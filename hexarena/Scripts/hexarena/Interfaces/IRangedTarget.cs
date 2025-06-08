using Godot;
using System;

namespace Interfaces{
	
	public interface IRangedTarget : ITarget
	{
		Point Position {get; set;}
		public bool TargetInRange(ITargetable targetable);
	}
	
}
