using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class AlliesOnlyTarget : Target
{
		public override sealed bool ValidTargetType(ITargetable ally){
			return ally is IPlayer;
		}
		public override bool ValidTarget(ITargetable ally){
			return ValidTargetType(ally);
		}
		public override void AddTargetable(ITargetable ally){
			if(ValidTarget(ally)){
				TargetList.Add(ally);
			}
		}
}
	
}
