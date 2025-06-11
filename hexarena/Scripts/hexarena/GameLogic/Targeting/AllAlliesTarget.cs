using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
	public class AllAlliesTarget : AlliesOnlyTarget, IGlobalTarget
	{
		public BattleField BattleField {get; set;}
		public void SetBattleField(BattleField battleField){
			BattleField = battleField;
			PopulateFromGrid();
		}
		
		AllAlliesTarget(){
			TargetList = new List<ITargetable>();
		}
		public override bool IsReady() {return BattleField!=null;}
		public override void PopulateFromGrid(){
			TargetList = BattleField.Players.Cast<ITargetable>().ToList();
		}
		
	}
	
}
