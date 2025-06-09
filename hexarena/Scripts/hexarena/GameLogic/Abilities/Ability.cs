using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class Ability : IAbility
{
	public List<ITarget> Targets { get; set; }
	
	public Ability()
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
