using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public struct Point
		{ public int x; public int y; }

	public interface ITile : ITargetable
	{
		public Point position { get;protected set;}
		public List<ITile> Neighbours { get; protected set;}
		public ICharacter characterOnTile { get; set;}

		public bool isAvailable { get; set;}
	}		
}
