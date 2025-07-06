using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public abstract class Passive : Ability, IPassive
	{
		public Action<List<ITargetable>, string> ActivatedPassiveEffect {get; set;}
	}
}
