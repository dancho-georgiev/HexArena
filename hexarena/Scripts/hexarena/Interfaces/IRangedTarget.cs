using Godot;
using System;

namespace Interfaces{
	
	public interface IRangeRestrictedTarget : ITarget
	{
		Point Position {get; set;}
		public bool TargetInRange(ITargetable targetable);
	}
	
}
