using Godot;
using System;

namespace Interfaces{
	
	public interface ITypeRestrictedTarget : ITarget
	{
		public bool ValidType(ITargetable targetable);
	}
	
}
