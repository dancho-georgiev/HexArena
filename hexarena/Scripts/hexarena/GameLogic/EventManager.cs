using System;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace GameLogic
{
	public partial class EventManager : Node
	{ //Ascend the event manager
		[Signal]
		public delegate void StartTurnEventHandler();
		[Signal]
		public delegate void EndTurnEventHandler();
		[Signal]
		public delegate void HitEventHandler(); //trqq da sa slojat argumenti koi e kogo s kvo ili nqkvi podrobnosti
		[Signal]
		public delegate void TakeDamageEventHandler(); //sushtoto kato gornoto
		[Signal]
		public delegate void ActivateAbility1EventHandler(); //moje sushto da ima argument ama ne sum siguren tochno<<<<<<< HEAD
															 //kvo iskame taka che za sq da sedi taka kogato stignem do tam she go opraim
		public Action<ICharacter,List<ITargetable>, string> ActivatedAbility;
		public Action<ITile> ChangedTile;
		public Action<IPlayer> OnCharacterSelected;

		
		public void EmitOnStartTurn() => EmitSignal(SignalName.StartTurn);
		public void EmitOnEndTurn() => EmitSignal(SignalName.EndTurn);
		public void EmitOnHit() => EmitSignal(SignalName.Hit);
		public void EmitOnTakeDamage() => EmitSignal(SignalName.TakeDamage);
		public void EmitOnActivateAbility1() => EmitSignal(SignalName.ActivateAbility1);
		public void EmitOnActivatedAbility(ICharacter sender, List<ITargetable> reciever, string abilityName)
		 => ActivatedAbility?.Invoke(sender, reciever.ToList(), abilityName);
		public void EmitOnChangedTile(ITile tile) => ChangedTile?.Invoke(tile);
		public void EmitOnCharacterSelected(IPlayer selectedCharacter) {
			OnCharacterSelected?.Invoke(selectedCharacter);
			GD.Print("EmitONCharacterSelected has emitted");
		} 
	}
}
