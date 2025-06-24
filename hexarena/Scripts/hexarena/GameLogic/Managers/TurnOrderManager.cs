using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace GameLogic{
	
	public class TurnOrderManager
	{
		private ICharacter characterOnTurn;
		private readonly List<IPlayer> players;
		private readonly List<IEnemy> enemies;
		
		private List<ICharacter> turnOrder;
		private List<ICharacter> havePlayed;
		public ICharacter CharacterOnTurn {get{return characterOnTurn;} protected set{characterOnTurn = value;}}
		
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
		
		public ICharacter NextTurn(){
			CharacterOnTurn = turnOrder.FirstOrDefault();
			if(CharacterOnTurn != null){
				havePlayed.Add(CharacterOnTurn);
				turnOrder.Remove(CharacterOnTurn);
			}
			
			if(turnOrder.Count==0 && havePlayed.Count > 0){
				turnOrder = new List<ICharacter>(havePlayed);
				UpdateOrder();
				havePlayed.Clear();
			}
			return CharacterOnTurn;
		}
	}
}
