using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class EnemiesOnlyTarget : Target
	{
		public override sealed bool ValidTargetType(ITargetable enemy){
			return enemy is IEnemy;
		}
		public override bool ValidTarget(ITargetable enemy){
			return ValidTargetType(enemy);
		}
		public override void AddTargetable(ITargetable enemy){
			if(ValidTarget(enemy)){
				TargetList.Add(enemy);
			}
		}
	}

	
}
