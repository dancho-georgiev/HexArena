using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface ICharacter : ITargetable
	{

		int Health { get;  set; }
		double StepEnergyCost { get;  set; }
		ITile Tile { get;  set; }
		public List<IStatusEffect> StatusEffects {get; protected set;}
		

	}
}
