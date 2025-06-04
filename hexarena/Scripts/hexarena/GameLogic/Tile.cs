using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

public class Tile : ITile
{
	
		public Point position { get;set;}
		public List<ITile> neighbours { get; set;}
		public ICharacter characterOnTile { get; set;}
		public bool isAvailable { get; set;} = true;
	
	public Tile(Point position,ICharacter character)
	{
		this.position = position;
		this.characterOnTile = character;
		neighbours = new List<ITile>();
	}
	
	public void TakeDamage(int damage){
		if(characterOnTile == null){}
		else{
			characterOnTile.Health -= damage;
		}
	}
	
	public void TakeStatusEffect(IStatusEffect statusEffect){
		
	}
	
	
}
