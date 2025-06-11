using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class Active : Ability, IActive
	{
	public abstract ITarget GetTargetType();
	}
	
}
