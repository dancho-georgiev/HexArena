using Godot;
using System;
using GameLogic;
using System.Collections.Generic;
using Interfaces;
using System.Linq;

namespace View
{
	
	public partial class GridView : Node
	{
		public BattleField battleField;
		[Export]
		public int Width {get; set;}
		[Export]
		public int Length {get; set;}
		[Export]
		public PackedScene Hexagon {get; set;}
		[Export]
		public PackedScene PlayerSprite {get; set;}
		

		private float _hexSize;
	
		public List<List<HexagonTile>> Grid {get; set;}
		public List<GameCharacter> Characters{get; set;}
		
		public HexagonTile hoveredTile {get; set;}
		
		public List<HexagonTile> hexPath {get; set;}
		
		public bool selectingTarget {get; set;}
		public bool selectedCharacter {get; set;}
		public EventManager eventManager {get; set;}
		
		//veche grida e or hexagonTileove
		public override void _Ready(){
			Grid = new List<List<HexagonTile>>();
			Characters = new List<GameCharacter>();
			eventManager = new EventManager();
			battleField = new BattleField(eventManager, Width, Length);
			Hexagon = GD.Load<PackedScene>("res://Scenes/Hexagon.tscn");
			
			//gets hexagon size for TileToWorld
			Hexagon sampleHex = Hexagon.Instantiate<Hexagon>();
			_hexSize = sampleHex.Size;
			sampleHex.QueueFree();
			
			
			for(int i = 0; i < Width; i++){
				Grid.Add(new List<HexagonTile>());
				for(int j = 0; j < Length; j++){
					Hexagon inst = Hexagon.Instantiate<Hexagon>();
					inst.GlobalPosition = new Vector2(i * inst.Size + (j % 2 == 0 ? inst.Size/2 : 1), j * inst.Size);
					HexagonTile hexTile = new HexagonTile(inst, battleField.GetTile(i, j));
					hexTile.TileClicked += OnTileClicked;
					hexTile.MouseEntered += OnTileEntered;
					hexTile.MouseExited += OnTileExited;
					Grid[i].Add(hexTile);
					AddChild(hexTile);
				}
			}
			SetupNeighbours();
		} 
		
		private void SetupNeighbours(){
			foreach(List<HexagonTile> list in Grid){
				foreach(HexagonTile tile in list){
					foreach(ITile neighbour in tile.Tile.Neighbours){
						tile.Neighbours.Add(Grid[neighbour.Position.y][neighbour.Position.x]);
					}
				}
			}
		}
		
		private void MoveSelectedCharacter(HexagonTile tile)
		{
			
			if(battleField.SelectedCharacter == null)
			{
				GD.PrintErr("character is null");
				return;
			}
			 if (hoveredTile == null)
			{
				GD.PrintErr("hoveredTile is null");
				return;
			}
			
			if(!hoveredTile.Tile.IsAvailable())
			{
				GD.Print("hovered tile is not available ");
				return;
			}
			
			battleField.MoveSelectedCharacter(tile.Tile);
			
			if(battleField.SelectedCharacter.Tile == tile.Tile)
			{
				GD.Print("Logic character moved");
				if (Characters.Any(x => x.Character == battleField.SelectedCharacter))
				{
					GD.Print("visuals entered");
					GameCharacter visualCharacter = Characters.First(x=>x.Character==battleField.SelectedCharacter);
					visualCharacter.MoveVisualCharacter(tile);
				}
			}
				
		}
		
		
		public override void _Input(InputEvent @event)
		{
			if(@event is InputEventKey key)
			{
				if(key.Pressed && key.Keycode == Key.Q)
				{
					if(hoveredTile != null)
					{
						if(hoveredTile.Tile.CharacterOnTile == null){ // mnogo losho napraveno ne trqq da e taka
							IPlayer peasant = new Peasant(eventManager); 
							battleField.PlacePlayer(peasant, hoveredTile.Tile);
							GameCharacter player = PlayerSprite.Instantiate<GameCharacter>();
							player.GlobalPosition = hoveredTile.Hexagon.GlobalPosition;
							player.ZIndex = 2;
							player.Character = peasant;
							player.CurrentTile = hoveredTile;
							Characters.Add(player);
							hoveredTile.Hexagon.AddChild(player);
						}
					}
				}
			
				if(key.Pressed && key.Keycode == Key.E)
				{
					if(battleField.SelectedCharacter!=null)
					{
						battleField.SelectAbility(battleField.SelectedCharacter.ActiveAbilities[0]);
						selectingTarget = true;
						GD.Print($"using ability");
					}
				}

			}
			
		}
		public void PrintTilesWithCharacters()
		{
			GD.Print("===== Grid State =====");
			for (int y = 0; y < Length; y++)
			{
				string row = "";
				for (int x = 0; x < Width; x++)
				{
					var tile = battleField.GetTile(x, y);
					if (tile.CharacterOnTile != null) 
					{
						row += "[C]";  // Tile has a character
					}
					else if (tile.IsAvailable()) 
					{
						row += "[ ]";  // Available empty tile
					}
					else 
					{
						row += "[X]";  // Unavailable tile
					}
				}
				GD.Print(row);
			}
		}
		
		
		
		public void OnTileClicked(HexagonTile tile){
			GD.Print($"Clicked tile at {tile.Tile.Position.x}, {tile.Tile.Position.y }");
			if(!selectedCharacter){
				if(tile.Tile.CharacterOnTile!=null && tile.Tile.CharacterOnTile is IPlayer){
					battleField.SelectCharacter(tile.Tile.CharacterOnTile as IPlayer);
					GD.Print($"selected character");
					selectedCharacter = true;
				}
			}
			else if(selectingTarget){
				if(tile.Tile.CharacterOnTile!=null){
					battleField.AddTargetable(tile.Tile.CharacterOnTile);
					if(battleField.SelectedAbilityTarget.TargetList.Count>0)GD.Print($"added target");
					if(battleField.SelectedAbilityTarget.IsReady()){
						battleField.UseSelectedAbility();
						GD.Print($"{tile.Tile.CharacterOnTile.Health}");
						selectingTarget = false;
						selectedCharacter = false;
					}
					
				}
				
			}
			else if(selectedCharacter){
					MoveSelectedCharacter(tile);
					selectedCharacter = false;
					//PrintTilesWithCharacters();
				}
			
		}
		public void OnTileEntered(HexagonTile tile){
			hoveredTile = tile;
		}
		public void OnTileExited(HexagonTile tile){
			hoveredTile = null;
		}
		
		
		
		public Vector2 TileToWorld(int x, int y)
		{
			return new Vector2(
				x * _hexSize + (y % 2 == 1 ? _hexSize / 2 : 0),
				y * _hexSize * Mathf.Sqrt(3)/2
			);
		}
	}
	
}
