using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace GameLogic{
	
	public class TurnOrderManager
	{
		private readonly List<IPlayer> players;
		private readonly List<IEnemy> enemies;
		
		private List<ICharacter> turnOrder;
		private List<ICharacter> havePlayed;
		public TurnOrderManager(List<ICharacter> list){
			turnOrder = list;
			havePlayed = new List<ICharacter>();
			UpdateOrder();
		} 
		
		public TurnOrderManager(List<IPlayer> players, List<IEnemy> enemies){
			this.players = players;
			this.enemies = enemies;
			turnOrder = new List<ICharacter>();
			UpdateList();
			havePlayed = new List<ICharacter>();
		} 
		
		public void UpdateList(){
			turnOrder = Enumerable.Union
			(players.Select(x=>x as ICharacter),enemies.Select(x=>x as ICharacter)).ToList();
			UpdateOrder();
		}
		
		public void UpdateOrder(){
			turnOrder = turnOrder.OrderByDescending(x=>x.Initiative).ToList();
		}
		
		public ICharacter Peek(){
			return turnOrder.FirstOrDefault();
		}
		
		public ICharacter NextTurn(){
			ICharacter next = turnOrder.FirstOrDefault();
			if(next != null){
				havePlayed.Add(next);
				turnOrder.Remove(next);
			}
			if(turnOrder.Count==0 && havePlayed.Count > 0){
				turnOrder = new List<ICharacter>(havePlayed);
				UpdateOrder();
				havePlayed.Clear();
			}
			return next;
		}
	}
}
