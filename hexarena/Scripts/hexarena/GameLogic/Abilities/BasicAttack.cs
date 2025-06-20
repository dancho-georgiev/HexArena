using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	//BasicAttack | should make it abstact later
	public class BasicAttack : Active
	{
		public int Damage {get; protected set;}
		
		//public BasicAttack(EventManager _eventManager, SingleTarget _targeting){ 
			//Damage = 2;
			//Connect(_eventManager);
			//AddTarget(_targeting);
		//}
		public BasicAttack(ITile position){
			Damage = 2;
			Target = new SingleTarget(position, 1);
		}
		public override SingleTarget GetTargetType(){
			return Target as SingleTarget;
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager){
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
