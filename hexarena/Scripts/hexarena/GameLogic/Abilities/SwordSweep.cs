using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SwordSweep : Active
	{
		private int damage = 2;
		public SwordSweep(EventManager _eventManager, SweepFrontTarget _targeting) //Could be made to work with other forms of sweeping targeting
		{
			Connect(_eventManager);
			AddTarget(_targeting);
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
