using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	public partial class PlaceholderEnemy : Enemy
	{
		public PlaceholderEnemy(int health, int stepCost) : base(health, stepCost){
			
		}
		protected override void InitializeActives()
		{
			
		}
		protected override void InitializePassives()
		{
			
		}
	}
}
