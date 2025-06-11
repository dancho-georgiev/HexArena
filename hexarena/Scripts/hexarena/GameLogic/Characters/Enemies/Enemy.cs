using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	public abstract class Enemy : Character, IEnemy
	{		
		public Enemy(int health, int stepCost) : base(health, stepCost){
			StatusEffects = new List<IStatusEffect>();
		}
		
	}
}
