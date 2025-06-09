using Godot;
using System;

namespace Interfaces{
	
	public interface IRangeRestrictedTarget : ITarget
	{
		public ITile Position {get; set;}
		public bool TargetInRange(ITargetable targetable);
	}
	
}
