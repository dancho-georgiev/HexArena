using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

namespace GameLogic{
	
	public class AllAlliesTarget : AlliesOnlyTarget
	{
		AllAlliesTarget(Grid grid){
			this.grid = grid;
		}
		
		public override void PopulateFromGrid(){
			TargetList = new List<ITargetable>();
			foreach (List<ITile> col in grid.TileGrid)
			{
				foreach (ITile tile in col)
				{
					AddTargetable(tile.CharacterOnTile);
				}
			}
		}
		
	}
	
}
