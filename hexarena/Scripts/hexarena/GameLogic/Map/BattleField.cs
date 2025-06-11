using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using Managers;

namespace GameLogic{
	
	public class BattleField
	{
		public const int GridSize = 6;
		
		private Grid grid;
		public List<IEnemy> Enemies;
		public List<IPlayer> Players;
		private StatusEffectFactory statusEffectFactory;
		private EventManager eventManager;
		public bool PlayerTurn = true;
		
		public IPlayer SelectedCharacter {get; set;}
		public IAbility SelectedAbility{get; set;}
		public ITarget SelectedAbilityTarget {get; set;}
		
		public BattleField(EventManager eventManager){
			this.eventManager = eventManager;
			grid = new Grid(6,6);
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			statusEffectFactory = new StatusEffectFactory(eventManager);
		}
		
		public ITile getTile(int x, int y){
			return grid.TileGrid[x][y];
		}
		
		public void AddEnemy(IEnemy enemy){
			Enemies.Add(enemy);
		}
		public void AddPlayer(IPlayer player){
			Players.Add(player);
		}
		
		public void MoveSelectedCharacter(ITile position){
			SelectedCharacter.MoveCharacter(position);
		}
		
		public void SelectCharacter(IPlayer selected){
			if(Players.Contains(selected)){
				SelectedCharacter = selected;
			}
		}
		
		public void SelectAbility(IActive selectedAbility){
			if(SelectedCharacter.ActiveAbilities.Contains(selectedAbility)){
				SelectedAbility = selectedAbility;
			}
		}
		
		//while targeta ne e ready se dobavqt targetable neshta
		public void AddTargetable(ITargetable targetable){
			SelectedAbilityTarget.AddTargetable(targetable);
		}
		
		//kogato e ready se puska abilityto
		public void UseSelectedAbility(){
			if(SelectedAbilityTarget.IsReady()) SelectedAbility.Use();
		}
		
		
	}
	
}
