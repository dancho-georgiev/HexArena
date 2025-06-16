using Godot;
using System;
using GameLogic;

namespace Interfaces{
	
	public interface IGlobalTarget : ITarget
	{
		BattleField BattleField {get; set;}
		void SetBattleField(BattleField battleField);
	}
	
}
