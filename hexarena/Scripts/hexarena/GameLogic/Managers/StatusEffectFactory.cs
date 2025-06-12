using Godot;
using System;
using Interfaces;
using GameLogic;

namespace Managers{
	
	public class StatusEffectFactory
	{
		private EventManager eventManager;
		public StatusEffectFactory(EventManager eventManager){
			this.eventManager = eventManager;
		}
		
		public void RemoveExpiredEffects(){
			
		}
		
		public IStatusEffect makePoison(ICharacter character, int duration, int damage){
			return new PoisonEffect(damage, duration, eventManager, character);
		}
		public IStatusEffect makeVulnerable(ICharacter character, float bonusPercent, int duration){
			return new VulnerableEffect(bonusPercent, duration, eventManager, character);
		}
		public IStatusEffect makePassiveHeal(ICharacter character,int duration, int healAmount){
			return new PassiveHealEffect(healAmount,duration,eventManager,character);
		}
		//todo: sichkite statusi
	}
	
}
