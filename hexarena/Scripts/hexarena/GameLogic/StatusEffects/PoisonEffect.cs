using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	public class PoisonEffect: StatusEffect
	//Ne znam dali vseki poison effect shte ima edin i sushti duration i damage
	{
		private short damage;
		private int duration;
		public PoisonEffect(short damage, int duration, EventManager eventManager, ICharacter character)
		{
			AddTarget(new SelfTarget(character));
			Connect(eventManager);
			this.damage = damage;
			this.duration = duration;
		}
		//tova ne znam dali pravi tova koeto si mislq che pravi
		//celta mi e da subscribena poisona s StartTurn
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
					targetable.TakeDamage(damage);
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
