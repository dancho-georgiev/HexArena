using Godot;
using System;
using System.Collections.Generic;
using GameLogic;
using Interfaces;

namespace View{
	
	public partial class AbilityBar : MarginContainer
	{
		private ICharacter currentCharacter;
		[Export] public PackedScene SlotScene { get; set; }
		[Export] public int MaxSlots { get; set; } = 8; // Safety limit
		private HBoxContainer _abilitiesContainer;
		//private List<AbilitySlot> _abilitySlots = new List<AbilitySlot>();
		public ICharacter CurrentCharacter {
			get{return currentCharacter;} 
			set{
				currentCharacter = value;
				DisplayAbilities();
			}
		}
		
		public override void _Ready()
		{
			_abilitiesContainer = GetNode<HBoxContainer>("AbilitiesContainer");
			if(_abilitiesContainer == null)
			{
				GD.Print("_abilitiesContainer is null");
			}
			// Dont create slots initially we ll create them dynamically since 
			//different characters will have different count of abilities
		}
		
		public void DisplayAbilities()
		{
			
			 int slotsNeeded = Mathf.Min(currentCharacter.ActiveAbilities.Count, MaxSlots);
			
			 for(int i = 0; i < slotsNeeded; i++)
			{
				IActive ability = currentCharacter.ActiveAbilities[i];
				
				UIActiveAbility uiAbility = new UIActiveAbility(ability,GetIconForAbility(ability));
				
				AbilitySlot slot = SlotScene.Instantiate<AbilitySlot>();
				_abilitiesContainer.AddChild(slot);
				
				slot.Initialize();
				slot.Pressed += () => ability.Use();
			}
		}
		private Texture2D GetIconForAbility(IActive ability)
		{
			return null;
		}
		
	}
}
