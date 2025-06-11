using Godot;
using System;
using Interfaces;

using System.Linq;
using System.Collections.Generic;


namespace GameLogic{
	public class SweepFrontTarget : Target, IRangeRestrictedTarget, ICountRestrictedTarget  //Name should be changed to something more comprehensive
	{
		public ITile Position {get; set;}
		public uint TargetCount {get; set;}
		
		public SweepFrontTarget(ITile _position, ITargetable targetable)
		{
			TargetList = new List<ITargetable>();
			Position = _position;
			TargetCount = 3;
			if(TargetInRange(targetable))
			{
				AddTargetable(targetable);
				PopulateFromGrid();
			}
			
		}
		public override bool IsReady(){return TargetCount == TargetList.Count;}
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
			List<ITargetable> targets;
			if(TargetList.First() is ITile)
			{
				targets = Position.Neighbours.Intersect((TargetList[0] as ITile).Neighbours).Select(x=>x as ITargetable).ToList();
			}
			else
			{
				targets = Position.Neighbours.Intersect((TargetList[0] as ICharacter).Tile.Neighbours).Select(x=>x as ITargetable).ToList();
			}
			foreach(ITile tile in targets)
			{
				AddTargetable(tile);
			}
		}
	}
}
