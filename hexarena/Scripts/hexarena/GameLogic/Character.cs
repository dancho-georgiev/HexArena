using Godot;
using System;
using Interfaces;
using System.Collections.Generic;


namespace GameLogic{
	
	public class Character : Targetable, ICharacter
{
		public short Health { get;  set; }
		
		public double StepEnergyCost { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}
		public List<IActive> ActiveAbilities {get; protected set;} 
		public List<IPassive> CharacterPassives {get; protected set;}
		public ITile Tile { get;  set; }
		
		public Character(int health,double stepEnergyCost,ITile tile)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
			tile.CharacterOnTile = this;  //Temporary fix 
		}
		
		public Character(int health,double stepEnergyCost,ITile tile, List<IActive> _activeAbilities)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.AddRange(_activeAbilities);
			CharacterPassives = new List<IPassive>();  //idk dali e nujno tuk
			tile.CharacterOnTile = this;  //Temporary fix 
		}
		
		public Character(int health,double stepEnergyCost,ITile tile, List<IPassive> _charPassives)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>(); //Idk dali e nujno tuk
			CharacterPassives = new List<IPassive>();
			CharacterPassives.AddRange(_charPassives);
			tile.CharacterOnTile = this;  //Temporary fix 
		}
		
		public Character(int health,double stepEnergyCost,ITile tile, List<IActive> _activeAbilities, List<IPassive> _charPassives)
		{
			this.Health = health;
			this.StepEnergyCost = stepEnergyCost;
			this.Tile = tile;
			StatusEffects = new List<IStatusEffect>();
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.AddRange(_activeAbilities);
			CharacterPassives = new List<IPassive>();
			CharacterPassives.AddRange(_activeAbilities);
			tile.CharacterOnTile = this;  //Temporary fix 
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
