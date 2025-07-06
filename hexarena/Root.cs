using Godot;
using System;
using GameLogic;

namespace View
{
	public partial class Root : Node2D
	{
		EventManager eventManager = new EventManager();
		[Export] Control UserInterface;
		[Export] Node BattleField;
		public override void _Ready(){
			
			HBoxContainer abilitiesDisplay = GetNode<HBoxContainer>("UserInterface/MarginContainer/AbilitiesDisplay");
			if (abilitiesDisplay is AbilitiesDisplay scriptInstance)
			{
				GD.Print("Root tried to give its event manager to UserInterface");
				scriptInstance.eventManager = this.eventManager;
				scriptInstance.Connect(eventManager);
			}
		}
	}
}
