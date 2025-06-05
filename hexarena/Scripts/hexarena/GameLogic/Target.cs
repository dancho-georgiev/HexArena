using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	
	public abstract class Target : ITarget

{
	public abstract void InitializeList();
	public List<ITargetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
}

	
}
