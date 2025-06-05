using System;
using System.Collections.Generic;
using GameLogic;
namespace Interfaces
{
	public interface  ITarget
	{
		public abstract void InitializeList();

		public List<Targetable> TargetList { get; protected set; }
		public uint TargetCount { get; set; }
		public void Initialize(uint targetsCount)
		{
			TargetCount = targetsCount;
			InitializeList();
		}


	}
}
