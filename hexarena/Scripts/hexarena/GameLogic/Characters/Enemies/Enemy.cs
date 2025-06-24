using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	public abstract class Enemy : Character, IEnemy
	{		
		public abstract void PlayTurn();
		public Enemy(EventManager eventManager,int health, int stepCost, int initiative) : base(eventManager,health, stepCost, initiative){
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>();
			PassiveAbilities = new List<IPassive>();
			Initiative = initiative;
		}
		
	}
}
