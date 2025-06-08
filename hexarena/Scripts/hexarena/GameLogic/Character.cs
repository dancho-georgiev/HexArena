using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	
	public class Character : Targetable, ICharacter
{
		public int Health { get;  set; }
		
		public double StepEnergyCost { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}
		public ITile Tile { get;  set; }
		
		public Character(int health,double stepEnergyCost,ITile tile)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
		}
		
		public override void TakeDamage(int damage){
			Health-=damage;
		}
	
	public override void TakeStatusEffect(IStatusEffect statusEffect){
		StatusEffects.Add(statusEffect);
	}
	
	public void TeleportCharacter(ITile TargetPosition)
	{
		if(TargetPosition.IsAvailable == true)
		{
			this.Tile = TargetPosition;
		}
		
	}
	
	//Grisho:tva trqq da vleze samo na characterite koito she nqma da sa playable(na enemytata)
	// po nqkoe vreme go premesti
	// Dancho: Ne tova shte vleze na vseki 
	public void MoveCharacter(ITile TargetPosition)
	{
		if(TargetPosition.IsAvailable == true)
		{
			List<ITile> pathTiles = FindShortestPath(this.Tile, TargetPosition);
			foreach(ITile i in pathTiles){
				this.Tile = i;
				//GD.Print($"{this.Tile.Position.x}, {this.Tile.Position.y}");
			}
		}
	}
		//standart shortest path algo; can be improved
		public List<ITile> FindShortestPath(ITile startTile, ITile endTile)
		{
			var cameFrom = new Dictionary<ITile,ITile>();
			var visited = new List<ITile>();
			var queue = new Queue<ITile>();
			
			 queue.Enqueue(startTile);
   			 visited.Add(startTile);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if(current == endTile)
				{
					var path = new List<ITile>();
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
							
							if (neighbor.IsAvailable && !visited.Contains(neighbor))
							{
								visited.Add(neighbor);
								cameFrom[neighbor] = current;
								queue.Enqueue(neighbor);
							}
						}
			}
			return null;
		}
	}
}
