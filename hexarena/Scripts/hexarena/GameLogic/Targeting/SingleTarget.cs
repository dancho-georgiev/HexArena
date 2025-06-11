using Godot;
using System;
using Interfaces;
using Utilities;
using System.Collections.Generic;

namespace GameLogic{
	public partial class SingleTarget : Target, ICountRestrictedTarget, IRangeRestrictedTarget
	{
		public ITile Position {get; set;}
		public uint TargetCount {get; set;}
		public int TargetRange {get; set;}
		
		public SingleTarget(ITile _position, ITargetable _targetable, int _targetRange)
		{			
			Position = _position;
			TargetCount = 1;
			TargetRange = _targetRange;
			if(TargetInRange(_targetable))
			{
				TargetList = new List<ITargetable>();
				AddTargetable(_targetable);
			}
		}
		
		public SingleTarget(ITile _position, int _targetRange)
		{		
			TargetList = new List<ITargetable>();	
			Position = _position;
			TargetCount = 1;
			TargetRange = _targetRange;
		}
		
		public override bool IsReady(){return TargetCount == TargetList.Count;}
		
		public bool ValidTargetCount()
		{
			return TargetList.Count<=TargetCount;     // HMmm
		}
		public override bool ValidTarget(ITargetable targetable)
		{
			return TargetInRange(targetable) && !IsReady();
		}
		
		public bool TargetInRange(ITargetable targetable)
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

		}
	}
}
