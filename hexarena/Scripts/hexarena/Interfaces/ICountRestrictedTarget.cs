using Godot;
using System;

namespace Interfaces {
	
	public interface ICountRestrictedTarget : ITarget
	{
		uint TargetCount {get; set;}
		bool ValidTargetCount();
	}
	
}
