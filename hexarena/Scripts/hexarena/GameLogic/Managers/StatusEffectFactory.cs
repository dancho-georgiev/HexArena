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
		//todo: sichkite statusi
	}
	
}
