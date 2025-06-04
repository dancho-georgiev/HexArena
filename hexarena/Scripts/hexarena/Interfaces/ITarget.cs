using System;
using System.Collections.Generic;

namespace Interfaces
{
	public interface  ITarget
	{
		public abstract void InitializeList();

		public List<ITargetable> TargetList { get; protected set; }
		public uint TargetCount { get; set; }
		public void Initialize(uint targetsCount)
		{
			TargetCount = targetsCount;
			InitializeList();
		}
		public virtual void pushTargetable(ITargetable t)
		{
			TargetList.Add(t);
		}
		public void ClearList()
		{
			TargetList.Clear();
		}


	}
}
