using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	public class Enemy : Character, IEnemy
	{		
		public Enemy(ITile position) : base(100, 1, position){
			Tile = position;
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>();
			Passives = new List<IPassive>();
			position.CharacterOnTile = this; //ne e hubavo po dobre v grid
		}
		
	}
}
