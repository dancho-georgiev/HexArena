using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface ICharacter : ITargetable
	{
<<<<<<< Updated upstream
		int Health { get;  set; }
		
		double StepEnergyCost { get;  set; }
		
		ITile Tile { get;  set; }
=======
		public int Health { get; protected set; }
		public List<IStatusEffect> StatusEffects {get; protected set;}
		public ITile Tile { get; protected set; }
>>>>>>> Stashed changes
	}
}
