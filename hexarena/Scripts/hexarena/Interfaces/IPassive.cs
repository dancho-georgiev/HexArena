using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IPassive : IAbility
	{
		Action<List<ITargetable>, string> ActivatedPassiveEffect {get; set;}
	}
}
