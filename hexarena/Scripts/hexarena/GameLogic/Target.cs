using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	
	public abstract class Target : ITarget
{
	protected BattleField battleField;
	public abstract bool IsReady();
	public abstract void PopulateFromGrid();
	public abstract bool ValidTarget(ITargetable targetable);
	public List<ITargetable> TargetList {get; set;}
	public virtual void AddTargetable(ITargetable targetable){
		if(ValidTarget(targetable)) TargetList.Add(targetable);
	}
	public virtual void Reset(){TargetList = new List<ITargetable>();}
}

	
}
