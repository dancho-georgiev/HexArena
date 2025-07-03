using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class Ability : IAbility
	{
		public ITarget Target { get; set; }
		protected EventManager eventManager;
		public Ability()
		{
			
		}
		
		public virtual  void AddTarget(ITarget target)
		{
			Target = target;
		}
		
		public abstract void Use();
		public abstract ITarget GetTargetType();
		public abstract void Connect(EventManager eventManager);
		public abstract void Disconnect(EventManager eventManager);
	}
}
