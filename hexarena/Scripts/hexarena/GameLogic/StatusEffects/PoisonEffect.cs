using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	//take small amount of damage every turn
	
	public class PoisonEffect: StatusEffect
	//Ne znam dali vseki poison effect shte ima edin i sushti duration i damage
	{
		public int Damage {get; protected set;}
		private int duration;
		public PoisonEffect(int damage, int duration, EventManager eventManager, ITargetable character)
		{
			AddTarget(new SelfTarget(character));
			Connect(eventManager);
			Damage = damage;
			this.duration = duration;
		}
		
		public override void Connect(EventManager eventManager)
		{
			eventManager.StartTurn += Use;
		}
		
		public override void Use()
		{
			
			if(duration <= 0)
			{
				 
				return;
			}
			foreach(ITarget target in Targets)
			{
				foreach(ITargetable targetable in target.TargetList)
				{
					targetable.TakeDamage(Damage);
				}
			}
			duration--;
		}
		public override void AddTarget(ITarget target)
		{
			Targets.Add(target);
		}
		
	}
}
