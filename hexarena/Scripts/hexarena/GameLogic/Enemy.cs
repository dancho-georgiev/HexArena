using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

public class Enemy : IEnemy
{
	public int Health {get; set;}
	public ITile Tile {get; set;}
	public List<IStatusEffect> StatusEffects {get; set;}
	public virtual void TakeDamage(int damage){
		Health-=damage;
	}
	public virtual void TakeStatusEffect(IStatusEffect status){
		StatusEffects.Add(status);
	}
}
