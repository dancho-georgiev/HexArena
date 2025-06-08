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
		
		x
		public override void AddTargetable(ITargetable targetable)
		{
			 TargetList.Add(targetable); 
		}
		
		public void PopulateFromGrid(Grid grid)
		{
			 this.grid = grid;
			TargetList = new List<ITargetable>();
			foreach (List<ITile> col in grid.TileGrid)
			{
				foreach (ITile tile in col)
				{
					if(tile.CharacterOnTile is ITargetable targetable)
					{
						AddTargetable(targetable);
					}
				}
			}
		}
		//public List<T> SelectNOfType<T>(int n) where T : class, ITargetable
		
		
		
	}
}
