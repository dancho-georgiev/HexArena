using Godot;
using System;
using Interfaces;

namespace View{
	
	public class UIActiveAbility
	{
		public IActive Ability { get; }
		public Texture2D Icon { get; }

		public UIActiveAbility(IActive ability, Texture2D icon)
		{
			Ability = ability;
			Icon = icon;
		}
	}
	
}
