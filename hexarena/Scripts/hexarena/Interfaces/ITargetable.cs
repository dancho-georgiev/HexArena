using System;
using System.Collections.Generic;
namespace Interfaces
{
	public interface ITargetable
	{
		public abstract void TakeDamage(short damage);

		public abstract void TakeStatusEffect(IStatusEffect effect);

	}
}
