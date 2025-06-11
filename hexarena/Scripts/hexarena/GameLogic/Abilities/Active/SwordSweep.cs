using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SwordSweep : Active
	{
		public int Damage {get; protected set;}
		public SwordSweep(EventManager _eventManager, SweepFrontTarget _targeting) //Could be made to work with other forms of sweeping targeting
		{
			Damage = 2;
			Connect(_eventManager);
			AddTarget(_targeting);
		}
		
		public SwordSweep(ITile position)
		{
			Damage = 2;
			Target = new SweepFrontTarget(position);
		}
		
		public override SweepFrontTarget GetTargetType(){
		return Target as SweepFrontTarget;
	}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.ActivateAbility1 -= Use;
		}
		public override void Use(){
			foreach(ITargetable target in Target.TargetList){
				target.TakeDamage(Damage);
			}
		}
	}
}
