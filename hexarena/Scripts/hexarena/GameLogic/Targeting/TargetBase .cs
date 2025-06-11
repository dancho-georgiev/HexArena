using Godot;
using System;
using System.Collections.Generic;
using Interfaces;

namespace GameLogic
{
	public class TargetBase : Target
	{
		//tova smqtam da e obssht klas koito vkluchva targeting mehanikata kum vseki character
		//nezavisimo dali e enemy ally ili neshto miscellanious - Dancho
		
		public TargetBase()
		{
			TargetList = new List<ITargetable>();
		}
		public override bool ValidTarget(ITargetable targetable) {return true;}
		public override bool IsReady(){return true;}
		public override void AddTargetable(ITargetable targetable)
		{
			 TargetList.Add(targetable); 
		}
		
		public override void PopulateFromGrid()
		{
		
		}
		//public List<T> SelectNOfType<T>(int n) where T : class, ITargetable
		
		
		
	}
}
