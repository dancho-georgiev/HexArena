using Godot;
using System;
using Interfaces;

//DOES WORk
namespace GameLogic{
	
	//BasicAttack
	public partial class SwordSlash : Active
	{
		public int Damage {get; protected set;}
		[Export] public Texture2D Icon { get; set; }
		
		public SwordSlash(EventManager _eventManager, SingleTarget _targeting){ 
			Damage = 2;
			Connect(_eventManager);
			AddTarget(_targeting);
		}
		
		public SwordSlash(ITile position)
		{
			Damage = 2;
			Target = new SingleTarget(position, 1);
		}
		
		public override SingleTarget GetTargetType(){
			return Target as SingleTarget;
		}
		
		public override void Connect(EventManager eventManager){
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager){
			eventManager.ActivateAbility1 -= Use;
		}
		public override void Use()
		{
			if (Target.IsReady())
			{
				Target.TargetList[0].TakeDamage(Damage);
			}
			else throw new Exception("not ready");
		}
	}
}
