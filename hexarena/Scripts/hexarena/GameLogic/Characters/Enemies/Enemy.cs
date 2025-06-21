using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	public abstract class Enemy : Character, IEnemy
	{		
		public Enemy(int health, int stepCost, EventManager eventManager) : base(health, stepCost, eventManager){
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>();
			PassiveAbilities = new List<IPassive>();
		}
		
	}
}
