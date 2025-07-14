using Godot;
using System;
using GameLogic;
using Interfaces;

public partial class AbilitySlot : Button
{
	[Export]
	 public Texture ButtonIcon;
	 public event Action AbilityActivated;
	 private IActive  _currentAbility;
	
	public void Initialize()
	{
		Icon = ButtonIcon as Texture2D;
		//here add ability description for example
	}
}
