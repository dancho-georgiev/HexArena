using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class EnemiesOnlyTarget : Target
	{
		public override sealed bool ValidTarget(ITargetable enemy){
			return enemy is IEnemy;
		}
		public override void AddTargetable(ITargetable enemy){
			if(ValidTarget(enemy)){
				TargetList.Add(enemy);
			}
		}
	}

	
}
