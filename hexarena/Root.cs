using Godot;
using System;
using Interfaces;
using GameLogic;
using System.Collections.Generic;

namespace View{
	
	public partial class Root : Node2D
	{
		public EventManager eventManager;
		public GridView Battlefield;
		public List<Button> Buttons;
		
		
		public override void _Ready(){
			Battlefield = GetNode<GridView>("BattleField");
			Buttons = new List<Button>();
			eventManager = Battlefield.eventManager;
			for(int i = 0; i < 4; i++){
				Button button = new Button();
				button.Position += new Vector2(i*100f, 475f);
				int index = i;
				button.Pressed += () => {eventManager.EmitOnSelectedAbility(index);};
				button.Text = $"Ability{i+1}";
				AddChild(button);
				Buttons.Add(button);
			}
			Button endTurnButton = new Button();
			endTurnButton.Position += new Vector2(700f,300f);
			endTurnButton.Text = $"End Turn";
			//subscribe to end turn event
			AddChild(endTurnButton);
			Buttons.Add(endTurnButton);
		}
	}
	
}
