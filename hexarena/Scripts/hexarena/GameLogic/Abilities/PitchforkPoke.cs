using Godot;
using System;
using Interfaces;

namespace GameLogic{

//BasicAttack
public partial class PitchforkPoke : Active
{
	private int damage = 1;
	
	public PitchforkPoke(EventManager _eventManager, SingleTarget _targeting){ 
		Connect(_eventManager);
		AddTarget(_targeting);
	}
	
	public override void Connect(EventManager eventManager){
		eventManager.ActivateAbility1 += Use;
	}
	public override void Use()
	{
		Targets[0].TargetList[0].TakeDamage(damage);
	}
}}
