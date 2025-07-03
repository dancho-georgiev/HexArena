using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class JadeTileFollowUp : Passive
	{
		private int damage;
		
		public override AllEnemiesTarget GetTargetType(){
			return Target as AllEnemiesTarget;
		}
		
		public JadeTileFollowUp(EventManager eventManager){
			damage = 10;
			this.eventManager = eventManager;
			eventManager.ActivatedAbility += Use;
		}
		public override void Connect(EventManager eventManager){
			eventManager.StartTurn += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.StartTurn -= Use;
		}
		
		private void UseOnTile(ITile tile){
			if(tile is JadeTile && tile.CharacterOnTile!=null){
				if(tile.CharacterOnTile is IEnemy){
					tile.TakeDamage(damage);
				}
				else{
					//gain shield
				}
			}
		}
		
		private void UseOnCharacter(ICharacter character){
			if(character.Tile is JadeTile){
				if(character is IEnemy){
					character.TakeDamage(damage);
				}
				else{
					//gain shield
				}
			}
		}
		
		public void Use(ICharacter sender, List<ITargetable> targets, string abilityName){
			foreach(ITargetable targetable in targets){
				if(targetable is ITile t){
					UseOnTile(t);
				}
				else if(targetable is ICharacter c){
					UseOnCharacter(c);
				}
			}
		}
		
		public override void Use(){
			
		}
	}
}
