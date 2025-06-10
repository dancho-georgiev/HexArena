using Godot;
using System;
using Interfaces;

namespace GameLogic{

//BasicAttack
public partial class PitchforkPoke : Active
{
	public int Damage {get; protected set;}
	
	public PitchforkPoke(EventManager _eventManager, SingleTarget _targeting){ 
		Damage = 1;
		Connect(_eventManager);
		AddTarget(_targeting);
	}
	
	public override void Connect(EventManager eventManager){
		eventManager.ActivateAbility1 += Use;
	}
	public override void Use()
	{
		Targets[0].TargetList[0].TakeDamage(Damage);
	}
}}
