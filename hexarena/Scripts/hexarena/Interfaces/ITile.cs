using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
namespace Interfaces
{
	public struct Point
		{
			 public int x; public int y;
			//public Point(){x=0;y=0;}
			public Point(int x, int y){this.x = x; this.y = y;}
			public static bool operator==(Point p1, Point p2){
				return p1.x==p2.x && p1.y==p2.y;
			}
			public static bool operator!=(Point p1, Point p2){
				return p1.x!=p2.x || p1.y!=p2.y;
			}
		}

	public interface ITile : ITargetable
	{
		public Point Position { get;protected set;}
		public List<ITile> Neighbours { get;set;}
		public Character CharacterOnTile { get; set;}
		public bool IsAvailable { get; set;}
	}	
}
