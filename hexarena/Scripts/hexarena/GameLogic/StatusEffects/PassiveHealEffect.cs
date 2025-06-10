using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	public class PassiveHealEffect: StatusEffect
	{
		public int HealAmount {get; protected set;}
		private int duration;
		
		public PassiveHealEffect(int healAmount, int duration,
			 EventManager eventManager, ICharacter character)
		{
			HealAmount = healAmount;
			this.duration = duration;
			AddTarget(new SelfTarget(character));
			Connect(eventManager);      
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
					targetable.TakeDamage(-HealAmount); //This heals
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
