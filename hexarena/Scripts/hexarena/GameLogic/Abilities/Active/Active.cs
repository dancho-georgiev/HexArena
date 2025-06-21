using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class Active : Ability, IActive
	{
		[Export] public Texture2D Icon { get; set; }
		
		
	}
	
}
