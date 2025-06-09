using Godot;
using System;
using Interfaces;

namespace GameLogic
{
	public class PoisonEffect: StatusEffectBase 
	//Ne znam dali vseki poison effect shte ima edin i sushti duration i damage
	{
		private int damage;
		private int duration;
		public PoisonEffect(int damage, int duration)
		{
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
			foreach(var target in Targets)
			{
				foreach(var targetable in target.TargetList)
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
