using Godot;
using System;
using System.Collections.Generic;
using GameLogic;
using Interfaces;
public partial class AbilityBar : MarginContainer
{
	[Export] public PackedScene SlotScene { get; set; }
	[Export] public int MaxSlots { get; set; } = 8; // Safety limit
	private HBoxContainer _abilitiesContainer;
	//private List<AbilitySlot> _abilitySlots = new List<AbilitySlot>();
	private ICharacter _currentCharacter;
	
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
	
	public void DisplayAbilities(ICharacter character)
	{
		_currentCharacter = character;
		
		 int slotsNeeded = Mathf.Min(character.ActiveAbilities.Count, MaxSlots);
		
		 for(int i = 0; i < slotsNeeded; i++)
		{
			IActive ability = character.ActiveAbilities[i];
			
			UIActiveAbility uiAbility = new UIActiveAbility(ability,GetIconForAbility(ability));
			
			AbilitySlot slot = SlotScene.Instantiate<AbilitySlot>();
			_abilitiesContainer.AddChild(slot);
			
			slot.Initialize(uiAbility);
			slot.Pressed += () => ability.Use();
		}
	}
	private Texture2D GetIconForAbility(IActive ability)
	{
		return null;
	}
	
}
