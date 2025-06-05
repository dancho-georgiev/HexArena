using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
		public abstract class Targetable : ITargetable
	{
		public abstract void TakeDamage(int damage);
		public abstract void TakeStatusEffect(IStatusEffect status);
	}	
}
