using System;
using System.Collections.Generic;
namespace Interfaces
{
	public interface ITargetable
	{
		public abstract void TakeDamage(int damage);
		public abstract void TakeStatusEffect(IStatusEffect effect);

	}
}
