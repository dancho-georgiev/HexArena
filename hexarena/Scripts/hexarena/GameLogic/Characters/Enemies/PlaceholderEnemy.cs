using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	public partial class PlaceholderEnemy : Enemy
	{
		public PlaceholderEnemy(int health, int stepCost, int initiative) : base(health, stepCost, initiative){
			
		}
		protected override void InitializeActives()
		{
			
		}
		protected override void InitializePassives()
		{
			
		}
	}
}
