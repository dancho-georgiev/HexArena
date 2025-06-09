using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	public class SurroundSelfTarget : Target, IRangeRestrictedTarget
	{
		public ITile Position {get; set;}
		//public int TargetRange;
		
		public SurroundSelfTarget(ITile _position)
		{
			Position = _position;
			PopulateFromGrid();
		}
		
		public bool TargetInRange(ITargetable targetable)
		//Graph traversal algorithm needed BFS or DFS or any other   
		{
			if(targetable is ITile)
			{
				return Position.Neighbours.Contains(targetable as ITile);
			}
			else
			{
				return Position.Neighbours.Contains((targetable as ICharacter).Tile);
			}
		}
		public override void PopulateFromGrid()
		{
			TargetList = new List<ITargetable>();
			foreach(ITile tile in Position.Neighbours)
			{
				AddTargetable(tile);
			}
		}
		public override bool ValidTarget(ITargetable targetable)
		{
			return TargetInRange(targetable);
		}
	}
}
