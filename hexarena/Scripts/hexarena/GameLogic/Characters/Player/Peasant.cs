using Godot;
using System;
using System.Collections.Generic;
using Interfaces;

namespace GameLogic{             //Veche kinda susvam vsihcko da e v GameLogic namespace-a
	public partial class Peasant : Character
	{
		public short Health {get; set;}
		public double StepEnergyCost { get;  set; }
		public ITile CurrentPosition { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}  //5 Issues zashtoto hidevam inherited memberite tuk? idontget it
		public List<IActive> ActiveAbilities {get; set;} 
		public List<IPassive> CharacterPassives {get; set;} 
		
		public Peasant()
		{
			Health = 10;
			StepEnergyCost = 1;
			StatusEffects = new List<IStatusEffect>();
			EventManager eventManager = new EventManager();
			SingleTarget twoRangeSingleTarget = new SingleTarget(this.CurrentPosition, )
			//Abilities
			Active pitchforkPoke = new PitchforkPoke(eventManager, );
			ActiveAbilities = new List<Active>(pitchforkPoke);
			//Passives
			CharacterPassives = new List<Passive>();
		}
	}
}
