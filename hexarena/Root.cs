using Godot;
using System;
using GameLogic;

namespace View
{
	public partial class Root : Node2D
	{
		EventManager eventManager = new EventManager();
		[Export] Node UserInterface;
		[Export] Node BattleField;
		public override void _Ready(){
			if(UserInterface is AbilitiesDisplay abilityDisplay)
			abilityDisplay.eventManager = this.eventManager;
			
			if(BattleField is GridView battlefield)
			battlefield.eventManager = this.eventManager;
		}
	}
}
