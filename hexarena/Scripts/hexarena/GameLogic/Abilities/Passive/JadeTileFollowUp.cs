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
			eventManager.ActivatedAbility.Connect(SetTargets);
			Target = new TargetBase();
		}
		public override void Connect(EventManager eventManager){
			//eventManager.StartTurn += Use;
		}
		public override void Disconnect(EventManager eventManager){
			//eventManager.StartTurn -= Use;
		}
		
		private void AddTile(ITile tile){
			if(tile is JadeTile && tile.CharacterOnTile!=null){
				
				Target.AddTargetable(tile);
			}
		}
		
		private void UseOnTile(ITile tile){
			if(tile.CharacterOnTile is IEnemy){
					tile.TakeDamage(damage);
			}
			else{
				//gain shield
			}
		}
		
		private void UseOnCharacter(ICharacter character){
			if(character is IEnemy){
				character.TakeDamage(damage);
			}
			else{
				//gain shield
			}
		}
		
		private void AddCharacter(ICharacter character){
			if(character.Tile is JadeTile){
				Target.AddTargetable(character);
			}
		}
		
		private void SetTargets(EventElement<ActivatedAbilityEventArgs> Event, ActivatedAbilityEventArgs args){
			ICharacter sender = args.Sender;
			List<ITargetable> targets = args.Targets;
			string abilityName = args.AbilityName;
			if(abilityName != "JadeTileFollowUp"){
				foreach(ITargetable targetable in targets){
					if(targetable is ITile t){
						AddTile(t);
					}
					else if(targetable is ICharacter c){
						AddCharacter(c);
					}
				}
				if(Target.TargetList.Count>0){
					Use();
				}
			}
			Event.FinishTask();
		}
		
		public override void Use(){
			foreach(ITargetable targetable in Target.TargetList){
				if(targetable is ITile t){
					UseOnTile(t);
				}
				else if(targetable is ICharacter c){
					UseOnCharacter(c);
				}
			}
			List<ITargetable> target = new List<ITargetable>(Target.TargetList);
			Target.Reset();
			GD.Print("Activated Passive");
			ActivatedPassiveEffect?.Invoke(target, "JadeTileFollowUp");
		}
	}
}
