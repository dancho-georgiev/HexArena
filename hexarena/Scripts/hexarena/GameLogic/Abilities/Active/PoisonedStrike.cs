using Godot;
using System;
using Interfaces;

namespace GameLogic
{
	public class PoisonedStrike : Active
	{
		private EventManager eventManager;
		public int Damage {get; set;}
		public int poisonDamage {get; set;}
		public int poisonDuration {get; set;}
		// this currently works but it takes itargetable and aplies the
		// poison effect to the itargetable but it is not saved because the itargetable
		//doesnt have place to save status effects
		public PoisonedStrike(EventManager _eventManager, SingleTarget _targeting)
		{ 
			Damage = 2;
			poisonDamage = 3;
			poisonDuration = 2;
			eventManager = _eventManager;
			Connect(_eventManager);
			AddTarget(_targeting);
		}
		
		public PoisonedStrike(ITile position)
		{ 
			Damage = 2;
			poisonDamage = 3;
			poisonDuration = 2;
			Target = new SingleTarget(position, 1);
		}
		
		public override SingleTarget GetTargetType()
		{
			return Target as SingleTarget;
		}
		
		public override void Connect(EventManager eventManager)
		{
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager)
		{
			eventManager.ActivateAbility1 -= Use;
		}
		public override void Use()
		{
			if (Target.IsReady())
			{
				 ITargetable target = Target.TargetList[0];
				 target.TakeDamage(Damage);
				 PoisonEffect poison = new PoisonEffect(poisonDamage ,poisonDuration, eventManager,target);
				 target.TakeStatusEffect(poison);
			}
			else throw new Exception("not ready");
		}
	}
}
