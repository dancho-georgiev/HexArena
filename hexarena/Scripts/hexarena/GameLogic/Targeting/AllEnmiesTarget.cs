using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
public class AllEnemiesTarget : EnemiesOnlyTarget
	{
		public AllEnemiesTarget(Grid grid)
		{
			this.grid = grid;
			PopulateFromGrid();
		}

		
		public override void PopulateFromGrid()
		{
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
	

	
