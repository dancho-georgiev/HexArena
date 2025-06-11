using Godot;
using System;
using System.Collections.Generic;
using Interfaces;

namespace GameLogic{
	public partial class Peasant : Character, IPlayer 
	{	
		public Peasant(EventManager _eventManager) : base (100, 1)
		{
			InitializePassives();
			InitializeActives();
		}
		protected override void InitializePassives()
		{
			PassiveAbilities = new List<IPassive>();
		}
		protected override void InitializeActives()
		{
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.Add(new PitchforkPoke(Tile));
		}
	}
	
}
