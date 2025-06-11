using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	//attacks do more damage to targets with vulnerable
	//BUG or FEATURE: kogato ima 2 vulnerable na nqkoi toi stackva
	//Example base damage e 10->15 pri 1 vulnerable 50%
	//Example base damage e 10->23 pri 2 vulnerable 50%
	public class Vulnerable : StatusEffect, IModifyDamageTaken
{
	private float multiplier;
	private int duration;
	private EventManager eventManager;
	
	public Vulnerable(float damageMultiplier, int duration, EventManager eventManager, ITargetable target)
	{
		this.multiplier = damageMultiplier;
		this.duration = duration;
		this.eventManager = eventManager;
		AddTarget(new SelfTarget(target));
		Connect(eventManager);
	}

	public int ModifyDamage(int baseDamage)
	{
		return (int)Math.Ceiling(baseDamage * multiplier);
	}

	public override void AddTarget(ITarget target)
		{
			Targets.Add(target);
		}

		public override void Connect(EventManager eventManager)
		{
			eventManager.StartTurn += Use;
		}

		public override void Use()
		{
			if (duration <= 0)
			{
				Expire();
				return;
				
			}
			duration--;
			return;
			
		}
		public override void Expire()
		{
			foreach (ITarget target in Targets)
			{
				foreach (ITargetable targetable in target.TargetList)
				{
					if (targetable is ICharacter character)
					{
						character.StatusEffects.Remove(this); 
					}
				}
			}
			eventManager.StartTurn -= Use;
		}
	}
}
