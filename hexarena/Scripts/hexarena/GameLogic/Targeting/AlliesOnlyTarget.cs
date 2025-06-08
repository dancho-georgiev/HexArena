using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class AlliesOnlyTarget : Target
{
		public override abstract void PopulateFromGrid();
		public override sealed void AddTargetable(ITargetable ally){
			if(ally is IPlayer){
				TargetList.Add(ally);
			}
		}
}
	
}
