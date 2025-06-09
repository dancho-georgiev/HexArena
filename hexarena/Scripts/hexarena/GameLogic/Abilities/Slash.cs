using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class MalevolentShrine : Passive
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
	
	public override void AddTarget(ITarget target){
		if(target is AllEnemiesTarget){
			Targets.Add(target);
		}	
		else {throw new ArgumentException("Target not found");} 
	}

	
	public override void Use(){
		foreach(ITarget t in Targets){
			foreach(Enemy enemy in t.TargetList){
				enemy.TakeDamage(damage);
			}
		}
	}
}
	
}
