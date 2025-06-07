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
		public List<IEnemy> Enemies;
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
			Enemies = new List<IEnemy>();
			Players = new List<IPlayer>();
			
			SetupNeighbours();
		}
		
		public void AddEnemy(IEnemy enemy){
			Enemies.Add(enemy);
		}
		
		public void SetupNeighbours() //veche sa hexove
		{
		for (int x = 0; x <  this.Width; x++) //redove
			{
			for (int y = 0; y < this.Length; y++)//koloni
				{
				var tile = this.TileGrid[x][y];
				tile.Neighbours = new List<ITile>();

				bool even = y % 2 == 0; // gleda dali rowa e cheten ili ne i izpolzva razlichni posoki v zavisimost

				//? new (int dx, int dy)[] { // tuk sa za odd r nie izpolzvame even r
					//(+1,  0), ( 0, -1), (-1, -1), //chetni
					//(-1,  0), (-1, +1), ( 0, +1) 
				//}
				//: new (int dx, int dy)[] {
					//(+1,  0), (+1, -1), ( 0, -1), //nechetni
					//(-1,  0), ( 0, +1), (+1, +1)
				//};
					
				var directions = even
				? new (int dx, int dy)[] {//even r coordinati
					(+1,  0), (+1, -1), ( 0, -1), // even rows (offset right)
					(-1,  0), ( 0, +1), (+1, +1)
				}
				: new (int dx, int dy)[] {
					(+1,  0), ( 0, -1), (-1, -1), // odd rows (not offset)
					(-1,  0), (-1, +1), ( 0, +1)
				};
					foreach (var (dx, dy) in directions)
					{
						int nx = x + dx;
						int ny = y + dy;

						if (nx >= 0 && ny >= 0 && nx < this.Width && ny < this.Length)
						{
							tile.Neighbours.Add(this.TileGrid[nx][ny]);
						}
					}
				}
			}
		}
	}
}
