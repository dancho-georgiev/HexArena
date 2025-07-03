using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class Impale : Active
	{
		public int Damage {get; protected set;}
		
		public Impale(ITile position, EventManager eventManager)
		{ 
			Damage = 3;
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
			if(Target.IsReady()){
				Target.TargetList[0].TakeDamage(Damage);
				if(Target.TargetList[0] is ITile) Target.TargetList[0] = new JadeTile(Target.TargetList[0] as ITile);
				else (Target.TargetList[0] as ICharacter).Tile = new JadeTile((Target.TargetList[0] as ICharacter).Tile);
				Target.Reset();
			}
			else throw new Exception("not ready");
		}
	}
	
}
