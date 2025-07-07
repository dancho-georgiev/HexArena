using Godot;
using System;
using System.Collections.Generic;

public interface IEventElement
{
	Action HasFinished {get; set;}
	int RemainingTasks{get; set;}
	bool Finished{get;}
	bool Running{get;}
	void Emit(EventArgs args);
	List<IEventElement> Children {get; set;}
	void FinishTask();
}
