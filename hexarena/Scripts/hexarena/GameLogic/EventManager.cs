using System;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
	public partial class EventManager : Node
	{
		[Signal]
		public  delegate void StartTurnEventHandler();
		[Signal]
		public delegate void EndTurnEventHandler();
		public void EmitOnStartTurn()
		{
		
		  EmitSignal(SignalName.StartTurn);
		}
	}
}
