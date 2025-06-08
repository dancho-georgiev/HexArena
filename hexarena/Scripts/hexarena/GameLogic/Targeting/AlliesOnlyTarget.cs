using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class AlliesOnlyTarget : Target, ITypeRestrictedTarget
{
		public bool ValidType(ITargetable ally){
			return ally is IPlayer;
		}
		public override bool ValidTarget(ITargetable ally){
			return ValidType(ally);
		}
		public override void AddTargetable(ITargetable ally){
			if(ValidTarget(ally)){
				TargetList.Add(ally);
			}
		}
}
	
}
