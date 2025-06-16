using Godot;
using System;
using Interfaces;

namespace GameLogic
{
	
	public class MalevolentShrine : Passive //imeto e mnogo edgy(peak)
	{
		private int damage;
		
		public override AllEnemiesTarget GetTargetType(){
			return Target as AllEnemiesTarget;
		}
		
		public MalevolentShrine(EventManager eventManager, AllEnemiesTarget target){
			damage = 10;
			Connect(eventManager);
			AddTarget(target);
		}
		public MalevolentShrine(EventManager eventManager){
			damage = 10;
			Connect(eventManager);
			Target = new AllEnemiesTarget();
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
			Target.PopulateFromGrid();
			foreach(Enemy enemy in Target.TargetList){
				enemy.TakeDamage(damage);
			}
			Target.Reset();
		}
	}
		
}
