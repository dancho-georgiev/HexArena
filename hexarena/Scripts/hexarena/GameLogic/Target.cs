using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic
{
	
	public abstract class Target : ITarget
<<<<<<< HEAD
	{
		public abstract void InitializeList();
		public IEnumerable<Targetable> TargetList {get; set;}
		public uint TargetCount {get; set;}
	}
=======
{
	public abstract void InitializeList();
	public List<Targetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
}
>>>>>>> 0e8a97e2cd4e00e354dc37b125a5e8275c71ea24
	
}
