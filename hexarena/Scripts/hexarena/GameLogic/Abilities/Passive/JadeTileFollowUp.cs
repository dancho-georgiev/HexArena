using Godot;
using System;

namespace GameLogic{
	
	public class JadeTileFollowUp : Passive
	{
		private int damage;
		
		public override AllEnemiesTarget GetTargetType(){
			return Target as AllEnemiesTarget;
		}
		
		public JadeTileFollowUp(EventManager eventManager, AllEnemiesTarget target){
			damage = 10;
			Connect(eventManager);
			AddTarget(target);
		}
		
		public JadeTileFollowUp(EventManager eventManager){
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
		
		public override void Use(){
			Target.PopulateFromGrid();
			foreach(Enemy enemy in Target.TargetList){
				enemy.TakeDamage(damage);
			}
			Target.Reset();
		}
	}
}
