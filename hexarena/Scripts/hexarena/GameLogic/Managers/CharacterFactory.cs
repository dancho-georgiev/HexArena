using Godot;
using System;
using Interfaces;
using GameLogic;
using View;

namespace GameLogic
{
	public static class CharacterFactory
	{
		public static GameCharacter CreateCharacter(CharacterType type, HexagonTile spawnTile)
		{
			string scenePath;
			switch(type)
			{
				
			 case CharacterType.Player:
					scenePath = "res://Characters/Friendly/Warrior.tscn";
					break;
				 case CharacterType.Enemy:
					scenePath = "res://Characters/Enemies/Goblin.tscn";
					 break;
				case CharacterType.NPC:
					scenePath = "res://Characters/NPCs/Villager.tscn";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var scene = GD.Load<PackedScene>($"res://Characters/{type}Character.tscn");
			var character = scene.Instantiate<GameCharacter>();
			character.CurrentTile = spawnTile;
			return character;
		}
	}
}
