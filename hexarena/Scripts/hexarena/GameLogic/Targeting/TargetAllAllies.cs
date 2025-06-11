using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
	public class AllAlliesTarget : AlliesOnlyTarget
	{
		AllAlliesTarget(BattleField battleField){
			this.battleField = battleField;
		}
		public override bool IsReady() {return true;}
		public override void PopulateFromGrid(){
			TargetList = new List<ITargetable>();
			TargetList = battleField.Players.Cast<ITargetable>().ToList();
		}
		
	}
	
}
