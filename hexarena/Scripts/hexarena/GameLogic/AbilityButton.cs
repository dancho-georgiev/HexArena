using Godot;
using System;
using Interfaces;

namespace GameLogic{  //ne mislq che GameLogic e pravilnoto mqsto za tova ama idk
public partial class AbilityButton : Button
{
	public IActive StoredAbility {get; protected set;}
	public AbilityButton(IActive ability){
		StoredAbility = ability;
	}
}

}
