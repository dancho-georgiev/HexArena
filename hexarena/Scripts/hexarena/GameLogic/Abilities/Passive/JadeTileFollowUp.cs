using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class JadeTileFollowUp : Passive
	{
		private int damage;
		
		public override TargetBase GetTargetType(){
			return Target as TargetBase;
		}
		
		public JadeTileFollowUp(EventManager eventManager){
			damage = 10;
			this.eventManager = eventManager;
			eventManager.ActivatedAbility += Use;
			Target = new TargetBase();
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
				Target.AddTargetable(tile);
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
				Target.AddTargetable(character);
			}
		}
		
		public void Use(ICharacter sender, List<ITargetable> targets, string abilityName){
			if(abilityName!="JadeTileFollowUp"){
				foreach(ITargetable targetable in targets){
					if(targetable is ITile t){
						UseOnTile(t);
					}
					else if(targetable is ICharacter c){
						UseOnCharacter(c);
					}
				}
				ActivatedPassiveEffect?.Invoke(Target.TargetList, "JadeTileFollowUp");
				Target.Reset();
			}
			
		}
		
		public override void Use(){
			
		}
	}
}
