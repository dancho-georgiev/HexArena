using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public abstract class EnemiesOnlyTarget : Target, ITypeRestrictedTarget
	{
		public bool ValidType(ITargetable enemy){
			return enemy is IEnemy;
		}
		public override bool ValidTarget(ITargetable enemy){
			return ValidType(enemy);
		}
	}

	
}
