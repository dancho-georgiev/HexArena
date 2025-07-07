using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class HasMovedEventArgs : EventArgs
	{
		public ICharacter Character {get; private set;}
		public List<ITile> Path{get; private set;}
		public HasMovedEventArgs(ICharacter character, List<ITile> path){
			Character = character;
			Path = path;
		}
	}
	
}
