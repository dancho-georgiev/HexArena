using Godot;
using System;
using Interfaces;
using System.Collections.Generic; 

namespace GameLogic
{
	public abstract class StatusEffectBase : IStatusEffect
	{
		public List<ITarget> Targets { get; set; }
		public StatusEffectBase()
		{
			Targets = new List<ITarget>();
		}
		public abstract void Use();
		public abstract void AddTarget(ITarget target);
		public abstract void Connect(EventManager eventManager);
		
	}
}
