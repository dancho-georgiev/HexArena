using Godot;
using System;
using Interfaces;
using GameLogic;
using System.Collections.Generic;
using System.Linq;

namespace Utilities{
	
	public static class Utility
	{
		private static bool TileRangeBFS_Helper(ITile start, ITile target, int depth, HashSet<ITile> visited)
		{
			if(start.Position == target.Position) return true;
			if(depth == 0) return false;
			visited.Add(start);
			List<bool> isTrue = new List<bool>();
			foreach(ITile neighbour in start.Neighbours)
			{
				if(!visited.Contains(neighbour))
				{
					isTrue.Add(TileRangeBFS_Helper(neighbour, target, depth-1, visited));
				}
			}
			return isTrue.Contains(true); 
		}
	
		public static bool TileRangeBFS(ITile start, ITile target, int depth)
		{
			HashSet<ITile> visited = new HashSet<ITile>();
			return TileRangeBFS_Helper(start, target, depth, visited);
		}
		
		
		public static List<ITile> FindShortestPath(ITile startTile, ITile endTile)
		{
			Dictionary<ITile,ITile> cameFrom = new Dictionary<ITile,ITile>();
			List<ITile> visited = new List<ITile>();
			Queue<ITile> queue = new Queue<ITile>();
			
			 queue.Enqueue(startTile);
   			 visited.Add(startTile);

			while (queue.Count > 0)
			{
				ITile current = queue.Dequeue();
				if(current == endTile)
				{
					List<ITile> path = new List<ITile>();
					ITile tile = current;
			   			while (tile != startTile)
						{
							path.Add(tile);
							tile = cameFrom[tile];
						}
						path.Add(startTile);
						path.Reverse();
		   				return path;
				}
						foreach (ITile neighbor in current.Neighbours)
	   					{    
							
							if (neighbor.IsAvailable() && !visited.Contains(neighbor))
							{
								visited.Add(neighbor);
								cameFrom[neighbor] = current;
								queue.Enqueue(neighbor);
							}
						}
			}
			return null;
		}
	}
	
}
	
	
