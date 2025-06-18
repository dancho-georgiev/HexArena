using Godot;
using System;
using Interfaces;

//DOES WORk
namespace GameLogic{
	
	//BasicAttack
	public partial class BasicAttack : Active
	{
		public int Damage {get; protected set;}
		
		public BasicAttack(EventManager _eventManager, SingleTarget _targeting){ 
			Damage = 2;
			Connect(_eventManager);
			AddTarget(_targeting);
		}
		public override GetTargetType(SingleTarget _targeting){
			AddTarget(_targeting);
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.ActivateAbility1 -= Use;
		}
		public override void Use()
		{
			Targets[0].TargetList[0].TakeDamage(Damage);
		}
	}
}
