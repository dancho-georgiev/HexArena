using Godot;
using System;
using Interfaces;
using GameLogic;
using View;

namespace GameLogic
{
	
		public enum CharacterType 
		{
			Peasant,
			PlaceholderEnemy,
			NPC
		}
	
	public class CharacterFactory
	{
		private EventManager eventManager;
		private BattleField battleField;
		
		public CharacterFactory(EventManager eventManager, BattleField battleField){
			this.eventManager = eventManager;
			this.battleField = battleField;
		}
		
		public GameCharacter SpawnCharacter(CharacterType type, HexagonTile spawnTile)
		{
			string scenePath;
			GameCharacter character = new GameCharacter();
			switch(type)
			{
			 	case CharacterType.Peasant:
					PackedScene scene = GD.Load<PackedScene>("res://Scenes/Characters/Friendly/Peasant.tscn");
				 	character = scene.Instantiate<PeasantView>();
					character.Character = new Peasant(eventManager);
					battleField.PlacePlayer(character.Character as IPlayer, spawnTile.Tile);
					break;
				 case CharacterType.PlaceholderEnemy:
					scenePath = "res://Characters/Enemies/Goblin.tscn";
					 break;
				case CharacterType.NPC:
					scenePath = "res://Characters/NPCs/Villager.tscn";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			character.ZIndex = 2;
			character.CurrentTile = spawnTile;
			character.GlobalPosition = spawnTile.Hexagon.GlobalPosition;
			spawnTile.Hexagon.AddChild(character);
			return character;
		}
	}
}
