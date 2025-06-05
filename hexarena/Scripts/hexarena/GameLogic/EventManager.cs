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
		[Signal]
		public delegate void HitEventHandler();
		[Signal]
		public delegate void TakeDamageEventHandler();
		
		public void EmitOnStartTurn() => EmitSignal(SignalName.StartTurn);
		public void EmitOnEndTurn() => EmitSignal(SignalName.EndTurn);
		public void EmitOnHit() => EmitSignal(SignalName.Hit);
		public void EmitOnTakeDamage() => EmitSignal(SignalName.TakeDamage);
	}
}
