using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class Slash : Passive
{
	private int damage;
	public Slash(EventManager eventManager){
		damage = 10;
		eventManager.StartTurn += Use2;
	}
	public override void AddTarget(ITarget target){
		if(target is AllEnemiesTarget){
			Targets.Add(target);
		}	
		else {} //throw nqkva greshka
	}
	public void Use2(){ //temporary(4am gaming)
		//GD.Print("slash used"); 
		foreach(Target t in Targets){
			foreach(Targetable targetable in t.TargetList){
				targetable.TakeDamage(damage);
			}
		}
	}
	
	public override void Use(object sender, EventArgs e){
		foreach(ITarget t in Targets){
			foreach(Enemy enemy in t.TargetList){
				enemy.TakeDamage(damage);
			}
		}
	}
}
	
}
