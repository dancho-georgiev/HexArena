using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using Godot;
namespace Interfaces
{
	public struct Point
		{
			public int x; public int y;
			public int X;  //vektorite iskat glavni bukvi womp womp
			public int Y; 
			public Point(int x, int y)
			{
				X = x;
				Y = y;
				this.x = x;
				this.y = y;
			}
			public static bool operator==(Point p1, Point p2){
				return p1.x==p2.x && p1.y==p2.y;
			}
			public static bool operator!=(Point p1, Point p2){
				return p1.x!=p2.x || p1.y!=p2.y;
			}
			public Vector2 ToVector2() => new Vector2(X, Y);
			public static Point FromVector2(Vector2 vector) => new Point((int)vector.X, (int)vector.Y);
		}

	public interface ITile : ITargetable
	{
		public Point Position { get;protected set;}
		public List<ITile> Neighbours { get;set;}
		public ICharacter CharacterOnTile { get; set;}
		public bool IsAvailable ();
	}	
}
