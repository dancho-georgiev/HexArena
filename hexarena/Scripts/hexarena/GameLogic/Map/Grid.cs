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
		public int Length; 
		public int Width;
		public List<List<ITile>> TileGrid;
		public List<Enemy> Enemies;
		public List<IPlayer> Players;
		
		public Grid(int length, int width){
			this.Length = length;
			this.Width = width;
			TileGrid = new List<List<ITile>>(Width);
			for(int i = 0; i < Width ; i++){
				TileGrid.Add(new List<ITile>(Length));
				for(int j = 0; j < Length; j++){
					TileGrid[i].Add(new Tile(new Point(j,i)));
				}
			}
			Enemies = new List<Enemy>();
			Players = new List<IPlayer>();
		}
		
		public void AddEnemy(Enemy enemy){
			Enemies.Add(enemy);
		}
	}
}
