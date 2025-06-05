using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	public class Character : Targetable, ICharacter
	{
			public int Health { get;  set; }
			
			public double StepEnergyCost { get;  set; }
			
			public ITile Tile { get;  set; }
			
			public List<IStatusEffect> StatusEffects {get;  set;}
			public Character(int health,double stepEnergyCost,ITile tile)
			{
				this.Health = health;
				this.StepEnergyCost = stepEnergyCost;
				this.Tile = tile;
			}
			
			public override void TakeDamage(int damage){
				Health-=damage;
			}
		
		public override void TakeStatusEffect(IStatusEffect statusEffects)
		{
		StatusEffects.Add(statusEffects);
		}
		
		
		 
		public void MoveCharacter(Tile TargetPosition)
		{
			if(TargetPosition.isAvailable == true)
			{
				
			}
		}
		
		public List<Tile> FindShortestPath(Tile startTile, Tile endTile)
		{
			var cameFrom = new Dictionary<Tile,Tile>();
			var visited = new List<Tile>();
			var queue = new Queue<Tile>();
			
			 queue.Enqueue(startTile);
   			 visited.Add(startTile);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if(current == endTile)
				{
					var path = new List<Tile>();
					var tile = current;
			   			while (tile != startTile)
						{
							path.Add(tile);
							tile = cameFrom[tile];
						}
						path.Add(startTile);
						path.Reverse();
		   				return path;
				}
						foreach (var neighbor in current.Neighbours)
	   					{    
							Tile neighborTile = neighbor as Tile;
							if (neighborTile.isAvailable && !visited.Contains(neighborTile))
							{
								visited.Add(neighborTile);
								cameFrom[neighborTile] = current;
								queue.Enqueue(neighborTile);
							}
						}
					
				
			}
			return null;
		}
	}
}
