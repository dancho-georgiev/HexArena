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
		
		public IPlayer SelectedCharacter {get; set;}
		public IAbility SelectedAbility{get; set;}
		public ITarget SelectedAbilityTarget {get; set;}
		
		// TUK NE TRQBVA LI VSICHKO DA NE E I(NESHTA)
		public BattleField(EventManager eventManager){
			this.eventManager = eventManager;
			grid = new Grid(6,6);
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			statusEffectFactory = new StatusEffectFactory(eventManager);
		}
		
		public BattleField(EventManager eventManager, int width, int length){
			this.eventManager = eventManager;
			grid = new Grid(width,length);
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			statusEffectFactory = new StatusEffectFactory(eventManager);
		}
		
		public ITile GetTile(int x, int y){
			return grid.TileGrid[x][y];
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
		public void InitializeRangeRestrictedTargets(ICharacter character, ITile tile){
			foreach(IRangeRestrictedTarget globalTarget in
			 character.ActiveAbilities.Where(x=> x.Target is IRangeRestrictedTarget).Select(x=>x.Target)){
				globalTarget.Position = tile;
			}
			foreach(IRangeRestrictedTarget globalTarget in
			 character.PassiveAbilities.Where(x=> x.Target is IRangeRestrictedTarget).Select(x=>x.Target)){
				globalTarget.Position = tile;
			}
		}
		
		public void PlacePlayer(IPlayer character, ITile tile){
			Players.Add(character);
			InitializeGlobalTargets(character);
			InitializeRangeRestrictedTargets(character, tile);
			character.Tile = tile;
			tile.IsAvailable = false;
			tile.CharacterOnTile = character;
			
		}
		
		public void PlaceEnemy(IEnemy character, ITile tile){
			Enemies.Add(character);
			InitializeGlobalTargets(character);
			InitializeRangeRestrictedTargets(character, tile);
			character.Tile = tile;
			tile.CharacterOnTile = character;
			tile.IsAvailable = false;
			
		}
		
		public ITile MoveSelectedCharacter(ITile position){
			SelectedCharacter.MoveCharacter(position);
			return position;
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
			SelectedAbilityTarget.AddTargetable(targetable);
		}
		
		//kogato e ready se puska abilityto
		public void UseSelectedAbility(){
			if(SelectedAbilityTarget.IsReady()) SelectedAbility.Use();
		}
		
		
	}
	
}
