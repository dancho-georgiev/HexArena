using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	
	public abstract class Target : ITarget
{
	protected Grid grid;
	public abstract void PopulateFromGrid();
	public List<ITargetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
	public abstract void AddTargetable(ITargetable targetable);
}

	
}
