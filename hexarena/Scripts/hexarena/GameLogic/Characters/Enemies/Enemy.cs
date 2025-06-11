using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	public abstract class Enemy : Character, IEnemy
	{		
		public Enemy(int health, int stepCost, ITile position) : base(health, stepCost, position){
			Tile = position;
			StatusEffects = new List<IStatusEffect>();
			position.CharacterOnTile = this; //ne e hubavo po dobre v grid
		}
		
	}
}
