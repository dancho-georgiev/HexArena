using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class EnemiesOnlyTarget : Target
	{
		public override abstract void PopulateFromGrid();
		public override sealed void AddTargetable(ITargetable enemy){
			if(enemy is IEnemy){
				TargetList.Add(enemy);
			}
		}
	}

	
}
