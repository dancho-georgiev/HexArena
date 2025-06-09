using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class AbilityBase : IAbility
{
	public List<ITarget> Targets { get; set; }
	
	public AbilityBase()
	{
		Targets = new List<ITarget>();
	}
	
	public virtual  void AddTarget(ITarget target)
	{
		Targets.Add(target);
	}
	
	public abstract void Use();
	public abstract void Connect(EventManager eventManager);
	

}
	
}
