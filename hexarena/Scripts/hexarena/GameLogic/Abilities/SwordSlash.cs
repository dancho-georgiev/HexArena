using Godot;
using System;
using Interfaces;

//DOES WORk
namespace GameLogic{
	
	//BasicAttack
	public partial class SwordSlash : Active
	{
		private short damage = 2;
		
		public SwordSlash(EventManager _eventManager, SingleTarget _targeting){ 
			Connect(_eventManager);
			AddTarget(_targeting);
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Use()
		{
			Targets[0].TargetList[0].TakeDamage(damage);
		}
	}
}
