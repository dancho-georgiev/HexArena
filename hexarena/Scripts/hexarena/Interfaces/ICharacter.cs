using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface ICharacter : ITargetable
	{	
		double StepEnergyCost { get;  set; }
		public int Health { get; set; }
		public List<IStatusEffect> StatusEffects {get; protected set;}
		public ITile Tile { get; protected set; }
	}
}
