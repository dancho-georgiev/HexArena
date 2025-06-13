using Godot;
using System;
using GameLogic;
using System.Collections.Generic;

namespace View{
	
	public partial class GridView : Node
	{
		public BattleField battleField;
		[Export]
		public int Width {get; set;}
		[Export]
		public int Length {get; set;}
		[Export]
		public PackedScene Hexagon {get; set;}
		public List<List<HexagonTile>> Grid {get; set;}
		
		//veche grida e or hexagonTileove
		public override void _Ready(){
			Grid = new List<List<HexagonTile>>();
			EventManager eventManager = new EventManager();
			battleField = new BattleField(eventManager, Width, Length);
			for(int i = 0; i < Width; i++){
				Grid.Add(new List<HexagonTile>());
				for(int j = 0; j < Length; j++){
					Hexagon inst = Hexagon.Instantiate<Hexagon>();
					inst.Position = new Vector2(i *  inst.Size + (j % 2 == 1 ? inst.Size/2 : 0), j * inst.Size);
					var hexTile = new HexagonTile(inst, battleField.GetTile(i, j));
					hexTile.TileClicked += (tile) => GD.Print($"Clicked tile at {tile.Tile.Position.x}, {tile.Tile.Position.y }");
					
					Grid[i].Add(hexTile);
					AddChild(hexTile);
				}
			}
		} 
			public Vector2 TileToWorld(PointDouble gridPosition)
			{
				float hexSize = Hexagon.Instantiate<Hexagon>().Size;
				return new Vector2(
				(float)(gridPosition.x * hexSize + (gridPosition.y % 2 == 1 ? hexSize / 2 : 0)),
				(float)(gridPosition.y * hexSize * Mathf.Sqrt(3)/2) // Vertical stacking
				);
			}
	}
	
}
 	
