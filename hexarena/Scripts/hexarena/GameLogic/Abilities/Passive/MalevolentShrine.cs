using Godot;
using System;
using Interfaces;

namespace GameLogic
{
	
	public class MalevolentShrine : Passive //imeto e mnogo edgy
	{
		private int damage;
		public MalevolentShrine(EventManager eventManager, AllEnemiesTarget target){
			damage = 10;
			Connect(eventManager);
			AddTarget(target);
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.StartTurn += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.StartTurn -= Use;
		}
		
		public override void AddTarget(ITarget target){
			if(target is AllEnemiesTarget){
				Target = target;
			}	
			else {throw new ArgumentException();} 
		}

		public override void Use(){
			foreach(Enemy enemy in Target.TargetList){
				enemy.TakeDamage(damage);
			}
		}
	}
		
}
