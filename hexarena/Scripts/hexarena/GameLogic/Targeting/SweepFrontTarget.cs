using Godot;
using System;
using Interfaces;

namespace GameLogic{
	public class SweepFrontTarget : Target, IRangeRestrictedTarget, ICountRestrictedTarget  //Name should be changed to something more comprehensive
	{
		public ITile Position {get; set;}
		public uint TargetCount {get; set;}
		
		public SweepFrontTarget(ITile _position, ITargetable targetable)
		{
			Position = _position;
			TargetCount = 3;
			if(TargetInRange(targetable))
			{
				TargetList = new List<ITargetable>();
				AddTargetable(targetable);
				PopulateFromGrid();
			}
			
		}
		
		public bool ValidTargetCount()
		{
			return TargetList.Count<=TargetCount;     // HMmm
		}
		
		public override bool ValidTarget(ITargetable targetable)
		{
			return TargetInRange(targetable);		//Only condition for the ability to work is for target to be in range
		}
		
		public bool TargetInRange(ITargetable targetable)
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
			List<ITargetable> targets = Position.Neighbours.Intersect(TargetList[0].Neighbours);
			targets.Add(TargetList.First());
			foreach(ITile tile in targets)
			{
				AddTargetable(tile);
			}
		}
	}
}
