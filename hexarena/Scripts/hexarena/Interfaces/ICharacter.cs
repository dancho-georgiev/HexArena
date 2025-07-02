using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;

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
		public void SelectAbility(int index);
		public void UseSelectedAbility();
		public EventManager eventManager {get; set;}
		public List<IActive> ActiveAbilities { get; set; }
		public List<IPassive> PassiveAbilities { get; set; }
		public IAbility SelectedAbility {get; set;}
		
		public Action<string, List<ITargetable>> ActivatedAbility {get; set;}
		public Action<List<ITile>> HasMoved {get; set;}
	}
}
