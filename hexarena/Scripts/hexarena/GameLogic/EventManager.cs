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
		public delegate void StartTurnEventHandler();
		[Signal]
		public delegate void EndTurnEventHandler();
		[Signal]
		public delegate void HitEventHandler(); //trqq da sa slojat argumenti koi e kogo s kvo ili nqkvi podrobnosti
		[Signal]
		public delegate void TakeDamageEventHandler(); //sushtoto kato gornoto
		[Signal]
		public delegate void ActivateAbility1EventHandler(); //moje sushto da ima argument ama ne sum siguren tochno
															 //kvo iskame taka che za sq da sedi taka kogato stignem do tam she go opraim
		
		public void EmitOnStartTurn() => EmitSignal(SignalName.StartTurn);
		public void EmitOnEndTurn() => EmitSignal(SignalName.EndTurn);
		public void EmitOnHit() => EmitSignal(SignalName.Hit);
		public void EmitOnTakeDamage() => EmitSignal(SignalName.TakeDamage);
		public void EmitOnActivateAbility1() => EmitSignal(SignalName.ActivateAbility1);
	}
}
