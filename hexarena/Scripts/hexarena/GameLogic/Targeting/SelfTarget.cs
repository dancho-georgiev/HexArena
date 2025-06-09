using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class SelfTarget : Target
	{
		public SelfTarget(ITargetable self){
			TargetList = new List<ITargetable>();
			TargetList.Add(self);
		}
		public override bool ValidTarget(ITargetable targetable){
			return true;
		}
		public override void PopulateFromGrid(){
			
		}
	}
	
}
