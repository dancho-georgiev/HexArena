using Godot;
using System;
using Interfaces;
using System.Collections.Generic; 

namespace GameLogic
{
	public abstract class StatusEffect : IStatusEffect
	{
		public Action<List<ITargetable>, string> ActivatedPassiveEffect {get; set;}
		public abstract bool IsExpired();
		public abstract void Disconnect(EventManager eventManager);
		public ITarget Target { get; set; }
		public StatusEffect()
		{
			
		}
		public abstract void Use();
		public abstract void AddTarget(ITarget target);
		public abstract void Connect(EventManager eventManager);
	}
}
