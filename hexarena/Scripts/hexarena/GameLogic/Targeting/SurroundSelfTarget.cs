using Godot;
using System;
using Interfaces;
using Utilities;
using System.Collections.Generic;

namespace GameLogic{
	public class SurroundSelfTarget : Target, IRangeRestrictedTarget
	{
		public ITile Position {get; set;}
		public int TargetRange;
		
		public SurroundSelfTarget(ITile _position, int TargetRange)
		{
			Position = _position;
			this.TargetRange= TargetRange;
			PopulateFromGrid();
		}
		
		public bool TargetInRange(ITargetable targetable)
		//Graph traversal algorithm needed BFS or DFS or any other   
		{
			if(targetable is ITile){
				return Utility.TileRangeBFS(Position, targetable as ITile, TargetRange);
			}
			else{
				return Utility.TileRangeBFS(Position, (targetable as ICharacter).Tile, TargetRange);
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
