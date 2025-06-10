using System;
using System.Collections.Generic;

namespace Interfaces
{
	public interface IClass
	{
		public List<IActive> ActiveAbilites { get; set; }
		public List<IPassive> PassiveAbilities { get; set; }
	}
}
