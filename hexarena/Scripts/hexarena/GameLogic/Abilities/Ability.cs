using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

public class Ability : IAbility
{
	public List<ITarget> Targets { get; set; } = new List<ITarget>();
	int Distance { get;  set; }
	int Damage { get;  set; }
	int Cost { get;  set; }
	int Cooldown { get;  set; }
	
	public void Use(object sender, EventArgs e)
	{
	  
	}

	public void Initialize(EventHandler handler)
	{
		handler += Use;
	}
	
	public Ability(int range,int damage, int cost, int cooldown)
	{
		this.Distance = range;
		this.Damage = damage;
		this.Cost = cost;
		this.Cooldown = cooldown;
	}
}
