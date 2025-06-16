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
	public class VulnerableEffect : StatusEffect, IModifyDamageTaken
	{
		private float bonusPercent;
		public int duration;
		private EventManager eventManager;
		
		public VulnerableEffect(float bonusPercent, int duration, EventManager eventManager, ICharacter target)
		{
			this.bonusPercent = bonusPercent; // e.g 0.4f for 40% 
			this.duration = duration;
			this.eventManager = eventManager;
			AddTarget(new SelfTarget(target));
			Connect(eventManager);
		}
		 //takes the set bonus percent from the constructor 
		//its used in character
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

		public override void Use() // decrements duration after a turn and calls expire
		{
			duration--;
			if (duration <= 0)
			{
				Expire();
				return;
			}
			return;
		}
			
		public override bool IsExpired(){ //idk
			return duration == 0;
		}
			
		public void Expire() //removes status efect after duration finishes
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
