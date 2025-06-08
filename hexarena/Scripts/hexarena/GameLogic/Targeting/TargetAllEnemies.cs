using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
public class TargetAllEnemies : TargetBase
	{
		public TargetAllEnemies(Grid grid)
		{
			PopulateFromGrid(grid);
		}
		public override void PopulateFromGrid(Grid grid)
		{
			this.grid = grid;
			TargetList = new List<ITargetable>();

			 foreach (List<ITile> col in grid.TileGrid)
			{
				foreach (ITile tile in col)
				{
					if (tile.CharacterOnTile is IEnemy enemy)
					{
						 AddTargetable(enemy);
					}
				}
			}
		}
		
	}
}
	
}
	
}
