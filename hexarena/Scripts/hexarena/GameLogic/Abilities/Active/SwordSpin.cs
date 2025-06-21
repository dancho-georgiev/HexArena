using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SwordSpin : Active
	{
		public int Damage {get; protected set;}
		
		public SwordSpin(EventManager eventManager, SurroundSelfTarget targeting)
		{
			Damage = 2;
			Connect(eventManager);
			AddTarget(targeting);
		}
		
		public SwordSpin(ITile position)
		{
			Damage = 2;
			Target = new SurroundSelfTarget(position, 1);
		}
		
		public override SurroundSelfTarget GetTargetType(){
			return Target as SurroundSelfTarget;
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
