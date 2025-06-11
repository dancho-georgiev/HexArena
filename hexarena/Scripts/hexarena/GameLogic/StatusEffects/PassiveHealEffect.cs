using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	//heal a small amount every turn
	public class PassiveHealEffect: StatusEffect
	{
<<<<<<< HEAD
		public int healAmount{get; set;}
=======
		public int HealAmount {get; protected set;}
>>>>>>> 06417b7e98f8746bc734717aac3efc4bacc07c1d
		private int duration;
		
		public PassiveHealEffect(int healAmount, int duration,
			 EventManager eventManager, ICharacter character)
		{
			this.HealAmount = healAmount;
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
