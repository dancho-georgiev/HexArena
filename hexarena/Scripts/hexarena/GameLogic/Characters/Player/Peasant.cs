using Godot;
using System;

namespace GameLogic{
	public partial class Peasant : Character
	{
		public short Health {get; set;}
		public double StepEnergyCost { get;  set; }
		public ITile CurrentPosition { get;  set; }
		public List<IStatusEffect> StatusEffects {get; set;}
		public List<IActive> ActiveAbilities {get; protected set;} 
		public List<IPassive> CharacterPassives {get; protected set;} 
		
		
		
	}
}
