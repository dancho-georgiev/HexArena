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
		int Initiative {get; set;}
		public int Health { get; set; }
		public List<IStatusEffect> StatusEffects {get;  set;}
		public ITile Tile { get; set; }
		public void MoveCharacter(ITile target);
		public void MoveCharacter(List<ITile> path);
		
		public List<IActive> ActiveAbilities { get; set; }
		public List<IPassive> PassiveAbilities { get; set; }
	}
}
