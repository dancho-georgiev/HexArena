using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	
	public abstract class Character : Targetable, ICharacter
{
		public int Health { get;  set; }
		
		public double StepEnergyCost { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}
		public List<IActive> ActiveAbilities { get; set; }
		public List<IPassive> PassiveAbilities { get; set; }
		public ITile Tile { get;  set; }
		
		public Character(int health,double stepEnergyCost,ITile tile)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
			tile.CharacterOnTile = this;  //Temporary fix 
		}
		
		protected abstract void InitializeActives();
		protected abstract void InitializePassives();
		
		public virtual int ModifyDamageTaken(int damage)
		{
			GD.Print($"ModifyDamageTaken entered with {damage}");
			int modifiedDamage = damage;
			foreach (IStatusEffect effect in StatusEffects)
			{
				if (effect is IModifyDamageTaken modifier)
				{
					modifiedDamage = modifier.ModifyDamage(modifiedDamage);
				}
			}
			GD.Print($"ModifyDamageTaken exited with {modifiedDamage}");
			return modifiedDamage;
		}
		public override void TakeDamage(int damage){
			int modifiedDamage = ModifyDamageTaken(damage);
			Health -= modifiedDamage;
		}

	public override void TakeStatusEffect(IStatusEffect statusEffect){
		StatusEffects.Add(statusEffect);
	}
	
	public void TeleportCharacter(ITile TargetPosition)
	{
		if(TargetPosition.IsAvailable)
		{
			this.Tile = TargetPosition;
		}
		
	}
	
	//Grisho:tva trqq da vleze samo na characterite koito she nqma da sa playable(na enemytata)
	// po nqkoe vreme go premesti
	// Dancho: Ne tova shte vleze na vseki 
	public virtual void MoveCharacter(ITile TargetPosition)
	{
		if(TargetPosition.IsAvailable)
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
