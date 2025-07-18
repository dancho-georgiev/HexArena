using System;
using System.Collections.Generic;
using GameLogic;
namespace Interfaces
{
	public interface  ITarget
	{
		public void PopulateFromGrid();
		public bool ValidTarget(ITargetable targetable);
		public void AddTargetable(ITargetable targetable);
		public List<ITargetable> TargetList { get; protected set; }
		public bool IsReady();
		public void Reset();
	}
}
