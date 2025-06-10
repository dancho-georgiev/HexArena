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
		public short Health { get; set; }
		public List<IStatusEffect> StatusEffects {get; protected set;}
		public List<IActive> ActiveAbilities {get; protected set;} 
		public List<IPassive> CharacterPassives {get; protected set;}  //ne znam dali ni trqbva kato imame StatusEffects List already ama mi se struva pravilno
		public ITile Tile { get; protected set; }
	}
}
