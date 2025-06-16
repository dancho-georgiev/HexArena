using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
public class AllEnemiesTarget : EnemiesOnlyTarget, IGlobalTarget
	{
		
		public BattleField BattleField{get; set;}
		public void SetBattleField(BattleField battleField){
			BattleField = battleField;
			PopulateFromGrid();
		}
		
		public AllEnemiesTarget(BattleField battleField)
		{
			TargetList = new List<ITargetable>();
			SetBattleField(battleField);
		}

		public AllEnemiesTarget()
		{
			TargetList = new List<ITargetable>();
		}

		public override bool IsReady() {return BattleField!=null;}
		
		public override void PopulateFromGrid()
		{
			TargetList = BattleField.Enemies.Cast<ITargetable>().ToList();
		}
		
	}
}
	

	
