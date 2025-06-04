using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IClass
	{
		public List<IActive> ActiveAbilites { get; protected set; }
		public List<IPassive> PassiveAbilities { get;protected set; }

		public abstract void InitializeActives();
		public abstract void InitializePassives();


	}
}
