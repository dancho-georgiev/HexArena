using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace GameLogic{
	
	
	public class EventElement<T> : IEventElement
	where T : EventArgs
	{
		private int remainingTasks;
		public Action HasFinished {get; set;}
		public delegate void EventHandler(EventElement<T> self, T param);
		private event EventHandler Action;
		private Dictionary<Action<EventElement<T>, T>, EventHandler> handlerMap;
		public int AllTasks{get; private set;}
		public int RemainingTasks{get{return remainingTasks;}
			set{
				remainingTasks = value;
				//GD.Print(remainingTasks);
			}
		}
		public bool Running{get; private set;}
		public bool Finished{
			get{
				bool finished = RemainingTasks==0 && (Children.Count == 0 || Children.All(x=>x.Finished));
				return finished;
			}
		}
		public List<IEventElement> Children {get; set;}
		
		public EventElement(){
			AllTasks = 0;
			RemainingTasks = 0;
			Children = new List<IEventElement>();
			handlerMap = new Dictionary<Action<EventElement<T>, T>, EventHandler>();
			Running = false;
		}
		
		public void Connect(Action<EventElement<T>,T> func){
				EventHandler handler = (s, e) => func(s,e);
				Action += handler;
				handlerMap[func] = handler;
				AllTasks++;
				RemainingTasks++;
		}
		
		public void Emit(EventArgs param){
			if(param is T){
				Action?.Invoke(this, param as T);
				Running = true;
			}
			else{
				throw new ArgumentException("Invalid argument type");
			}
			
		}
		
		public void FinishTask(){
			RemainingTasks--;
			if(Finished){
				RemainingTasks = AllTasks;
				Running = false;
				HasFinished?.Invoke();
				//GD.Print("Finished Event");
			} 
		}
		
		public void Disconnect(Action<EventElement<T>,T> func){
			if (handlerMap.TryGetValue(func, out EventHandler handler)){
				Action -= handler;
				handlerMap.Remove(func);
				AllTasks--;
				RemainingTasks--;
			}
		}
	}
}
