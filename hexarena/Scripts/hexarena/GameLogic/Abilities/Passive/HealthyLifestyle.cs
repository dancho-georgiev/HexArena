using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class HealthyLifestyle : Passive
	{
		
		public int Healing{get; protected set;}
	
		public HealthyLifestyle(SelfTarget _target)
		{
			Healing = 1;
			Name = "Healthy Lifestyle";
			EffectDescription = "+1 Health Regen at turn end.";  //Should probably rich text this up
			FlavorDescription = null;
			AddTarget(_target);
		}
		public override void Connect(EventManager eventManager){
			eventManager.EndTurn += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.EndTurn -= Use;
		}
		public override void AddTarget(ITarget target){ //kinda mi e sus tozi nachin na Use
				Target = target;
		}
		public override ITarget GetTargetType(){
			return Target as SelfTarget;
		}
		public override void Use(){
			(Target.TargetList[0] as ICharacter).Health += Healing;
		}
	}

}
