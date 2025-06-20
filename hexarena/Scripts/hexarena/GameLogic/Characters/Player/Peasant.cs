using Godot;
using System;
using System.Collections.Generic;
using Interfaces;

namespace GameLogic{
	public partial class Peasant : Character, IPlayer 
	{	
		public Peasant(EventManager _eventManager) : base (100, 1)
		{
			InitializePassives(_eventManager);
			InitializeActives();
		}
		protected override void InitializePassives(EventManager _eventManager)
		{
			SelfTarget self = new SelfTarget(this);
			PassiveAbilities = new List<IPassive>();
			PassiveAbilities.Add(new HealthyLifestyle(_eventManager, self));
		}
		protected override void InitializeActives()
		{
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.Add(new PitchforkPoke(Tile));
		}
	}
	
}
