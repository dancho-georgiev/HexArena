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
			JadeCharacter,
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
			PackedScene scene;
			switch(type)
			{
			 	case CharacterType.Peasant:
					scene = GD.Load<PackedScene>("res://Scenes/Characters/Friendly/Peasant.tscn");
				 	character = scene.Instantiate<PeasantView>();
					character.Character = new Peasant(eventManager);
					battleField.PlacePlayer(character.Character as IPlayer, spawnTile.Tile);
					break;
				 case CharacterType.PlaceholderEnemy:
					scene = GD.Load<PackedScene>("res://Scenes/Characters/Enemy/PlaceholderEnemy.tscn");
				 	character = scene.Instantiate<EnemyCharacter>();
					character.Character = new PlaceholderEnemy(eventManager, 100, 1, 1);
					battleField.PlaceEnemy(character.Character as IEnemy, spawnTile.Tile);
					 break;
				case CharacterType.JadeCharacter:
					scene = GD.Load<PackedScene>("res://Scenes/Characters/Friendly/JadeCharacter.tscn");
				 	character = scene.Instantiate<JadeCharacterView>();
					character.Character = new JadeCharacter(eventManager);
					battleField.PlacePlayer(character.Character as IPlayer, spawnTile.Tile);
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
