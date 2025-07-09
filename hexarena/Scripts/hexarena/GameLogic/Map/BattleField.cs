using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using System.Linq;

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
		public TurnOrderManager turnOrderManager {get; set;}
		public ICharacter SelectedCharacter {get; set;}
		public IAbility SelectedAbility{get{return SelectedCharacter.SelectedAbility;} 
										set{SelectedCharacter.SelectedAbility = value;}}
		public ITarget SelectedAbilityTarget {get; set;}
		
		public bool GameStarted {get; set;} = false;
		// TUK NE TRQBVA LI VSICHKO DA NE E I(NESHTA)
		public BattleField(EventManager eventManager){
			this.eventManager = eventManager;
			grid = new Grid(6,6);
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			statusEffectFactory = new StatusEffectFactory(eventManager);
			turnOrderManager = new TurnOrderManager(Players, Enemies);
			eventManager.ChangedTile.Connect(OnChangedTile);
		}
		
		public BattleField(EventManager eventManager, int width, int length){
			this.eventManager = eventManager;
			grid = new Grid(width,length);
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			statusEffectFactory = new StatusEffectFactory(eventManager);
			turnOrderManager = new TurnOrderManager(Players, Enemies);
			eventManager.ChangedTile.Connect(OnChangedTile);
		}
		
		public ITile GetTile(int row, int col){
			return grid.TileGrid[row][col];
		}
		
		public void OnChangedTile(IEventElement Event, ChangedTileEventArgs args){
			ITile tile = args.Tile;
			foreach(ITile t in tile.Neighbours){
				t.Neighbours.Remove(tile);
				t.Neighbours.Add(tile);
			}
			Event.FinishTask();
		}
		
		public void InitializeGlobalTargets(ICharacter character){
			foreach(IGlobalTarget globalTarget in
			 character.ActiveAbilities.Where(x=> x.Target is IGlobalTarget).Select(x=>x.Target)){
				globalTarget.SetBattleField(this);
			}
			foreach(IGlobalTarget globalTarget in
			 character.PassiveAbilities.Where(x=> x.Target is IGlobalTarget).Select(x=>x.Target)){
				globalTarget.SetBattleField(this);
			}
		}
		
		public void StartGame(){
			GameStarted = true;
			SelectedCharacter = turnOrderManager.NextTurn();
		}
		
		public void PlacePlayer(IPlayer character, ITile tile){
			Players.Add(character);
			InitializeGlobalTargets(character);
			character.Tile = tile;
			turnOrderManager.UpdateList();
		}
		
		public void PlaceEnemy(IEnemy character, ITile tile){
			Enemies.Add(character);
			InitializeGlobalTargets(character);
			character.Tile = tile;
			turnOrderManager.UpdateList();
		}
		
		public ITile MoveSelectedCharacter(ITile position){
			SelectedCharacter.MoveCharacter(position);
			return position;
		}
		
		public ITile MoveSelectedCharacter(List<ITile> path){
			SelectedCharacter.MoveCharacter(path);
			return SelectedCharacter.Tile;
		}
		
		public void SelectCharacter(IPlayer selected){
			if(Players.Contains(selected)){
				SelectedCharacter = selected;
			}
		}
		
		public void SelectAbility(IActive selectedAbility){
			if(SelectedCharacter.ActiveAbilities.Contains(selectedAbility)){
				SelectedAbility = selectedAbility;
				SelectedAbilityTarget = selectedAbility.GetTargetType();
			}
		}
		
		//while targeta ne e ready se dobavqt targetable neshta
		public void AddTargetable(ITargetable targetable){
			if(SelectedAbility!=null){
				SelectedAbilityTarget.AddTargetable(targetable);
			}
		}
		
		//kogato e ready se puska abilityto
		public void UseSelectedAbility(){
			SelectedCharacter.UseSelectedAbility();
		}
		
		public void StartTurn(){
			eventManager.EmitOnStartTurn();
			if(SelectedCharacter is IEnemy enemy) enemy.PlayTurn();
		}
		
		public void EndTurn(){
			eventManager.EmitOnEndTurn();
			SelectedCharacter = turnOrderManager.NextTurn();
		}
		
	}
	
}
