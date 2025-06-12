using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	//attacks do more damage to targets with vulnerable
	//BUG or FEATURE: kogato ima 2 vulnerable na nqkoi toi stackva : BUG
	//Example base damage e 10->15 pri 1 vulnerable 50%
	//Example base damage e 10->23 pri 2 vulnerable 50%
	
	
	//now its linear 2 vulnerable 50 give 100% instead of 125% 
	public class Vulnerable : StatusEffect, IModifyDamageTaken
	{
		private float bonusPercent;
		public int duration;
		private EventManager eventManager;
		
		public Vulnerable(float bonusPercent, int duration, EventManager eventManager, ITargetable target)
		{
			this.bonusPercent = bonusPercent; // e.g 0.4f for 40% 
			this.duration = duration;
			this.eventManager = eventManager;
			AddTarget(new SelfTarget(target));
			Connect(eventManager);
		}

		 public float GetBonusPercent()
		   {
			   return bonusPercent;
		   }

		public override void AddTarget(ITarget target)
		{
			Target = target;
		}

		public override void Connect(EventManager eventManager)
		{
			eventManager.StartTurn += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.StartTurn -= Use;
		}

		public override void Use()
		{
			duration--;
			if (duration <= 0)
			{
				Expire();
				return;
				
			}
			return;
		}
			
		public override bool IsExpired(){
			return duration == 0;
		}
			
		public void Expire()
		{
			foreach (ITargetable targetable in Target.TargetList)
			{
				if (targetable is ICharacter character)
				{
					character.StatusEffects.Remove(this); 
					//GD.Print("Status Effect has been removed");
				}
			}
			eventManager.StartTurn -= Use;
		}
	}
}
