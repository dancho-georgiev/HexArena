using Godot;
using System;
using Interfaces;


public class Character : Targetable, ICharacter
{
		public int Health { get;  set; }
		
		public double StepEnergyCost { get;  set; }
		
		public ITile Tile { get;  set; }
		
		public Character(int health,double stepEnergyCost,ITile tile)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
		}
		
		public override void TakeDamage(int damage){
			Health-=damage;
		}
	
	public override void TakeStatusEffect(IStatusEffect statusEffect){
		
	}
	
	public void MoveCharacter(Tile TargetPosition)
	{
		if(TargetPosition.isAvailable == true)
		{
			
		}
	}
}
