using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class ActivatedAbilityEventArgs : EventArgs
	{
		public ICharacter Sender {get; private set;}
		public List<ITargetable> Targets {get; private set;}
		public string AbilityName {get; private set;}
		public ActivatedAbilityEventArgs(ICharacter sender, List<ITargetable> reciever, string abilityName){
			Sender = sender;
			Targets = reciever;
			AbilityName = abilityName;
		}
	}
	
}
