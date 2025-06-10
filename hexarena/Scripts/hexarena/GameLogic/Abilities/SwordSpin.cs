using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SwordSpin : Active
	{
		private int damage = 2;
		public SwordSpin(EventManager eventManager, SurroundSelfTarget targeting)
		{
			Connect(eventManager);
			AddTarget(targeting);
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Use(){
			foreach(ITarget t in Targets){
				foreach(ITargetable target in t.TargetList){
					target.TakeDamage(damage);
				}
			}
		}
	}
}
