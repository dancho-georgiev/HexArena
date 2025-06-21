using Godot;
using System;
using System.Collections.Generic;
using Interfaces;

namespace GameLogic{
	public partial class Peasant : Character, IPlayer 
	{	
		public Peasant(EventManager eventManager) : base (100, 1, eventManager)
		{
			InitializePassives();
			InitializeActives();
		}
		protected override void InitializePassives()
		{
			SelfTarget self = new SelfTarget(this);
			PassiveAbilities = new List<IPassive>();
			HealthyLifestyle healthy = new HealthyLifestyle(self);
			PassiveAbilities.Add(healthy);
			healthy.Connect(eventManager);
		}
		protected override void InitializeActives()
		{
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.Add(new PitchforkPoke(Tile));
			ActiveAbilities.Add(new PitchforkPoke(Tile)); //For testing purposes only
		}
	}
	
}
