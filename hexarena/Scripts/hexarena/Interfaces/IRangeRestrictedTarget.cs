using Godot;
using System;

namespace Interfaces{
	
	public interface IRangeRestrictedTarget : ITarget
	{
		ITile Position {get; set;}
		public bool TargetInRange(ITargetable targetable);
	}
	
}
