using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class Impale : Active
	{
		public int Damage {get; protected set;}
		
		public Impale(ITile position, EventManager eventManager)
		{ 
			Damage = 3;
			Target = new SingleTarget(position, 1);	
			this.eventManager = eventManager;
		}
		
		public override SingleTarget GetTargetType()
		{
			return Target as SingleTarget;
		}
		
		public override void Connect(EventManager eventManager)
		{
			eventManager.ActivateAbility1 += Use;
		}
		public override void Disconnect(EventManager eventManager)
		{
				eventManager.ActivateAbility1 -= Use;
		}
		public override void Use()
		{
			if(Target.IsReady()){
				Target.TargetList[0].TakeDamage(Damage);
				ITile tile;
				if(Target.TargetList[0] is ITile) tile = Target.TargetList[0] as ITile;
				else tile = (Target.TargetList[0] as ICharacter).Tile;
				if(!(tile is JadeTile)){
					tile = new JadeTile(tile);
					eventManager.EmitOnChangedTile(tile);
				}
				Target.Reset();
			}
			else throw new Exception("not ready");
		}
	}
	
}
