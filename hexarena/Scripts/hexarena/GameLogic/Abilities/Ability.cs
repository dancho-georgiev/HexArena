using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class Ability : IAbility
{
	public List<ITarget> Targets { get; set; } = new List<ITarget>();
	//public int Distance { get;  set; }
	//public int Damage { get;  set; }
	//public int Cost { get;  set; }
	//public int Cooldown { get;  set; }
	
	public abstract void Use();
	public virtual void AddTarget(ITarget target){
		Targets.Add(target);
	}
	public abstract void Connect(EventManager eventManager);
	
	//public Ability(int range,int damage, int cost, int cooldown)
	//{
		//this.Distance = range;
		//this.Damage = damage;
		//this.Cost = cost;
		//this.Cooldown = cooldown;
	//}
}
	
}
