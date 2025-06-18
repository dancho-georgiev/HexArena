using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class Ability : IAbility
	{
		public string Name {get; set;} = "Unnamed Ability";
		public string Description {get; set;} = "Description";
		public ITarget Target { get; set; }
		
		public Ability()
		{
			
		}
		
		public virtual  void AddTarget(ITarget target)
		{
			Target = target;
		}
		
		public abstract void Use();
		public abstract ITarget GetTargetType();
		public abstract void Connect(EventManager eventManager); //Connects the ability to the given eventManager
		public abstract void Disconnect(EventManager eventManager); //Disconnects the Ability from the given eventManager
	}
}
