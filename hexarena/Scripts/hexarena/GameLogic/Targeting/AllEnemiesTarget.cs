using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
public class AllEnemiesTarget : EnemiesOnlyTarget
	{
		public AllEnemiesTarget(BattleField battleField)
		{
			this.battleField = battleField;
			PopulateFromGrid();
		}

		public override bool IsReady() {return true;}
		
		public override void PopulateFromGrid()
		{
			TargetList = new List<ITargetable>();
			TargetList = battleField.Enemies.Cast<ITargetable>().ToList();
		}
		
	}
}
	

	
