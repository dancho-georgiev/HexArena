using System;
using System.Collections.Generic;
using GameLogic;
namespace Interfaces
{
	public interface  ITarget
	{
		public void PopulateFromGrid();

		public List<ITargetable> TargetList { get; protected set; }
		public uint TargetCount { get; set; }
	}
}
