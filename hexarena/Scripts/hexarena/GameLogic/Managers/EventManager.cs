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
	{
		private List<(IEventElement, EventArgs)> eventQueue;
		private IEventElement currentEvent;
		public EventManager(){
			HasMoved = new EventElement<HasMovedEventArgs>();
			ActivatedAbility = new EventElement<ActivatedAbilityEventArgs>();
			ChangedTile = new EventElement<ChangedTileEventArgs>();
			eventQueue = new List<(IEventElement,EventArgs)>();
		}
		private void AddToQueue(IEventElement Event, EventArgs args){
			eventQueue.Add((Event, args));
			if(eventQueue.Count==1){
				currentEvent = eventQueue.First().Item1;
				currentEvent.HasFinished += NextEvent;
				currentEvent.Emit(eventQueue.First().Item2);
			}
		}
		private void NextEvent(){
			if(eventQueue.Count>0){
				currentEvent.HasFinished -= NextEvent;
				//GD.Print(currentEvent.GetType());
				eventQueue.RemoveAt(0);
				if(eventQueue.Count>0){
					currentEvent = eventQueue.First().Item1;
					currentEvent.HasFinished += NextEvent;
					currentEvent.Emit(eventQueue.First().Item2);
				}
				//GD.Print(eventQueue.Count);
			}
		}
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
		public EventElement<ActivatedAbilityEventArgs> ActivatedAbility;
		public EventElement<ChangedTileEventArgs> ChangedTile;
		public EventElement<HasMovedEventArgs> HasMoved;
		
		public void EmitOnStartTurn() => EmitSignal(SignalName.StartTurn);
		public void EmitOnEndTurn() => EmitSignal(SignalName.EndTurn);
		public void EmitOnHit() => EmitSignal(SignalName.Hit);
		public void EmitOnTakeDamage() => EmitSignal(SignalName.TakeDamage);
		public void EmitOnActivateAbility1() => EmitSignal(SignalName.ActivateAbility1);
		
		public void EmitOnActivatedAbility(ICharacter sender, List<ITargetable> reciever, string abilityName){
			AddToQueue(ActivatedAbility, new ActivatedAbilityEventArgs(sender, reciever.ToList(), abilityName));
			//GD.Print($"Added {abilityName}");
		}
		public void EmitOnChangedTile(ITile tile){
			AddToQueue(ChangedTile, new ChangedTileEventArgs(tile));
		}
		public void EmitOnHasMoved(ICharacter character, List<ITile> path){
			AddToQueue(HasMoved, new HasMovedEventArgs(character, path));
		}
	}
}
