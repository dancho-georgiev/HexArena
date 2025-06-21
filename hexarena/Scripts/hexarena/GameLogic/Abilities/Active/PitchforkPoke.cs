using Godot;
using System;
using Interfaces;

namespace GameLogic{
//BasicAttack
	public class PitchforkPoke : Active
	{
		public int Damage {get; protected set;}
		public PitchforkPoke(ITile position)
		{ 
			Damage = 1;
			Target = new SingleTarget(position, 2);
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
			if(Target.IsReady()){
				Target.TargetList[0].TakeDamage(Damage);
				Target.Reset();
			}
			else throw new Exception("not ready");
		}
	}
}
