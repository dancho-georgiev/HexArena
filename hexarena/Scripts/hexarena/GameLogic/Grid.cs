using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
	public class Grid
	{
		public int lenght; 

		public int width;
	
		public List<List<ITile>> tileGrid;

		public List<IEnemy> enemies;

		public List<IPlayer> players;
		
		
	}
}
