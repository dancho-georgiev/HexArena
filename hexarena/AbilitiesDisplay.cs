using Godot;
using System;
using GameLogic;
using Interfaces;
using System.Collections.Generic;

namespace View
{
	public partial class AbilitiesDisplay : HBoxContainer
	{ 
		
		//Upon receiving a signal from the event manager that a unit has been clicked
		//Receive a List of all active and passive abilities that the character has
		//Create a button for each member of the Active List
		//Create 1 button that opens up a conainer that lists the effect of every passive in a Richtextbox
		// (with keywords that show a text of their effect on hover)
		
		//Variant 1: Create all UI elements upon receiving the signal (LAME)
		//Variant 2: Have pre-made UI elements for each ammount of abilities. (maybe)
		//Variant 3: At the start of the stage create a hidden UI for each character
		// and only show it when needed. (Also create one for every possible summon). (pro)
		
		public EventManager eventManager;
		private Node ButtonsContainer;
		private List<AbilityButton> DisplayedButtons = new List<AbilityButton>();
		
		public override void _Ready(){
			
		}
		public void Connect(EventManager _eventManager){
			eventManager.OnCharacterSelected += onCharacterSelect;
		}
		public void Disconnect(EventManager _eventManager){
			eventManager.OnCharacterSelected -= onCharacterSelect;
		}
		private void onCharacterSelect(IPlayer character){
			
			ButtonsContainer.Free();
			ButtonsContainer = new Node();
			
				foreach(IActive activeAbility in character.ActiveAbilities){
					Button abilityButton = new AbilityButton(activeAbility);
					abilityButton.CustomMinimumSize = new Vector2(100, 100);
					abilityButton.ExpandIcon = true;
					abilityButton.Icon = GD.Load<Texture2D>("res://Assets/AbilityIcons/PitchforkPoke.png");
					abilityButton.Pressed += AbilityClicked;
					ButtonsContainer.AddChild(abilityButton);
				}
			this.AddChild(ButtonsContainer);
				
				//In the future, also display the Name, EffectDescription and FlavorDescription of the Ability.
				//Variant 1 add an on-hover event. The event should display a floating textbox with the description
				//of the ability which follows the mouse position
				//Variant 2 display the description of the ability in a pre-defined empty text box,
				//which is attached on one of the sides of the AbilitiesDisplay
				
		}
		private void AbilityClicked()
		{
			
		}
	}
}
