using Godot;
using System;
using Interfaces;

namespace GameLogic{

//BasicAttack
public class PitchforkPoke : Active
{
	public int Damage {get; protected set;}
	public SingleTarget target;
	
	public PitchforkPoke(ITile position){ 
		Damage = 1;
		target = new SingleTarget(position, 2);
	}
	
	public override SingleTarget GetTargetType(){
		return target;
	}
	
	public override void Connect(EventManager eventManager){
		eventManager.ActivateAbility1 += Use;
	}
	public override void Disconnect(EventManager eventManager){
			eventManager.ActivateAbility1 -= Use;
		}
	public override void Use()
	{
		if(target.IsReady()){
			target.TargetList[0].TakeDamage(Damage);
			target.Reset();
		}
		else throw new Exception("not ready");
	}
}}
