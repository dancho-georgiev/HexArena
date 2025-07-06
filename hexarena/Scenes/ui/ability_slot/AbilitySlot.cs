using Godot;
using System;
using GameLogic;
using Interfaces;

public partial class AbilitySlot : Button
{
	 [Export] public TextureRect Icon { get; set; }
	 public event Action AbilityActivated;
	 private IActive  _currentAbility;
	
	public void Initialize(UIActiveAbility  uiAbility)
	{
		 _currentAbility = uiAbility.Ability;
		Icon.Texture = GD.Load<Texture2D>("res://Assets/Characters/Friendly/peasant_old.png");
		//here add ability description for example
	}
}
