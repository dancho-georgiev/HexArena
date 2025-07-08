using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
namespace GameLogic{
	
	public partial class JadeCharacter : Character, IPlayer
	{
		public JadeCharacter(EventManager eventManager) : base (eventManager,100, 1, 1)
		{
			SelectedAbility = ActiveAbilities[0];
		}
		protected override void InitializePassives()
		{
			PassiveAbilities = new List<IPassive>();
			PassiveAbilities.Add(new JadeTileFollowUp(eventManager));
			foreach(IPassive passive in PassiveAbilities){
				passive.ActivatedPassiveEffect += (List<ITargetable> t, string s)=>{eventManager.EmitOnActivatedAbility(this,t,s);};
			}
		}
		protected override void InitializeActives()
		{
			ActiveAbilities = new List<IActive>();
			ActiveAbilities.Add(new Impale(Tile, eventManager));
		}
	}
	
}
