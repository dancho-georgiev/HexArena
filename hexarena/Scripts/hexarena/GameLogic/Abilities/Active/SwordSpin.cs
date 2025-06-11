using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SwordSpin : Active
	{
		public int Damage {get; protected set;}
		public SurroundSelfTarget target;
		public SwordSpin(EventManager eventManager, SurroundSelfTarget targeting)
		{
			Damage = 2;
			Connect(eventManager);
			AddTarget(targeting);
		}
		
		public override SurroundSelfTarget GetTargetType(){
			return target;
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.ActivateAbility1 -= Use;
		}
		public override void Use(){
			foreach(ITarget t in Targets){
				foreach(ITargetable target in t.TargetList){
					target.TakeDamage(Damage);
				}
			}
		}
	}
}
