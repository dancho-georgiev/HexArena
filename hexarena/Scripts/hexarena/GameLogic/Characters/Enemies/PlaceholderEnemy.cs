using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public partial class PlaceholderEnemy : Enemy
	{
		public PlaceholderEnemy(EventManager eventManager, int health, int stepCost, int initiative) : base(eventManager, health, stepCost, initiative){
			InitializeActives();
		}
		private ITile FindClosestPlayer(){
			HashSet<ITile> visited = new HashSet<ITile>();
			Queue<ITile> queue = new Queue<ITile>();
			queue.Enqueue(Tile);
			while(queue.Count > 0){
				ITile current = queue.Dequeue();
				visited.Add(current);
				foreach(ITile neighbour in current.Neighbours){
					if(!visited.Contains(neighbour)){
						if(neighbour.IsAvailable()){
							queue.Enqueue(neighbour);
						}
						if(neighbour.CharacterOnTile != null && neighbour.CharacterOnTile is IPlayer){
							return current;
						}
					}
				}
			}
			return null;
		}
		
		private IPlayer FindPlayerInRange(){
			HashSet<ITile> visited = new HashSet<ITile>();
			Queue<ITile> queue = new Queue<ITile>();
			queue.Enqueue(Tile);
			while(queue.Count > 0){
				ITile current = queue.Dequeue();
				visited.Add(current);
				if(current.CharacterOnTile is IPlayer) return current.CharacterOnTile as IPlayer;
				foreach(ITile neighbour in current.Neighbours){
					if(!visited.Contains(neighbour) &&
					 (ActiveAbilities[0].Target as IRangeRestrictedTarget).TargetInRange(neighbour)){
						queue.Enqueue(neighbour);
					}
				}
			}
			return null;
		}
		
		public override void PlayTurn(){
			ITile target = FindClosestPlayer();
			if(target==null) return;
			MoveCharacter(target);
			IPlayer player = FindPlayerInRange();
			ActiveAbilities[0].Target.AddTargetable(player);
			ActiveAbilities[0].Use();
		}
		
		protected override void InitializeActives()
		{
			ActiveAbilities.Add(new PitchforkPoke(Tile));
		}
		protected override void InitializePassives()
		{
			
		}
	}
}
