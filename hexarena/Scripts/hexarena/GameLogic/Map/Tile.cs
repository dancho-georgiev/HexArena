using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class Tile : Targetable, ITile
	{
		
			public Point Position { get;set;}
			public List<ITile> Neighbours { get; set;}
			public Character CharacterOnTile { get; set;}
			public bool IsAvailable { get; set;} = true;
		
		public Tile(Point position,Character character = null)
		{
			this.Position = position;
			this.CharacterOnTile = character;
			Neighbours = new List<ITile>();
		}
		
		public override void TakeDamage(int damage){
			if(CharacterOnTile == null){}
			else{
				CharacterOnTile.Health -= damage;
			}
		}
		
		
		public override void TakeStatusEffect(IStatusEffect statusEffect){
			
		}
	}
}
