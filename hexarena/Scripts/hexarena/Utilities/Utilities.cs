using Godot;
using System;
using Interfaces;
using GameLogic;
using System.Collections.Generic;
using System.Linq;
using View;

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
		

		
		public static List<ITile> FindShortestPath2(ITile startTile, ITile endTile){
			List<ITile> path = new List<ITile>();
			ITile current = startTile;
			path.Add(current);
			while(current!=endTile){
				current = current.Neighbours.Where(x=>!path.Contains(x))
				.MinBy(x => Distance(Direction(current, x),(Direction(current,endTile))));
				path.Add(current);
			}
			return path;
		}
		
		public static List<HexagonTile> FindShortestPath2(HexagonTile startTile, HexagonTile endTile){
			List<HexagonTile> path = new List<HexagonTile>();
			HexagonTile current = startTile;
			path.Add(current);
			while(current!=endTile){
				current = current.Neighbours.Where(x=>!path.Contains(x))
				.MinBy(x => Distance(Direction(current, x),(Direction(current,endTile))));
				path.Add(current);
			}
			return path;
		}
		
		public static float Distance(ITile tile1, ITile tile2){
			return Mathf.Sqrt(Mathf.Pow(tile2.Position.x - tile1.Position.x,2) + 
							Mathf.Pow(tile2.Position.y - tile1.Position.y,2));
		}
		
		public static float Distance(Vector2 point1, Vector2 point2){
			return Mathf.Sqrt(Mathf.Pow(point2.X - point1.X,2) + 
							Mathf.Pow(point2.Y - point1.Y,2));
		}
		
		public static Vector2 Direction(HexagonTile tile1, HexagonTile tile2){
			return (tile2.Hexagon.GlobalPosition-tile1.Hexagon.GlobalPosition).Normalized();
		}
		
		public static Vector2 Direction(ITile tile1, ITile tile2){
			return (new Vector2(tile2.Position.x-tile1.Position.x, tile2.Position.y - tile1.Position.y)).Normalized();
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
							if ( !visited.Contains(neighbor))
							{
								visited.Add(neighbor);
								cameFrom[neighbor] = current;
								queue.Enqueue(neighbor);
							}
						}
			}
			GD.Print("FindShortestPath exited with null");
			return null;
		}
	}
	
}
	
	
