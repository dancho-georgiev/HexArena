using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using System.Threading.Tasks;

namespace GameLogic{
	public partial class Peasant : Character, IPlayer 
	{	
		public Peasant(EventManager eventManager) : base (eventManager,100, 1, 1)
		{
			InitializePassives();
			InitializeActives();
			SelectedAbility = ActiveAbilities[0];
		}
		protected override void InitializePassives()
		{
			PassiveAbilities = new List<IPassive>();
		}
		protected override void InitializeActives()
		{
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.Add(new PitchforkPoke(Tile, eventManager));
		}
	}
	
}
