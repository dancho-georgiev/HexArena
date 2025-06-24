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
				current = current.Neighbours.Where(x=>!path.Contains(x) && x.IsAvailable())
				.MinBy(x => Distance(Direction(current, x),(Direction(current,endTile))));
				path.Add(current);
			}
			return path;
		}
		
		public static List<HexagonTile> FindShortestPath2(HexagonTile startTile, HexagonTile endTile){
			List<HexagonTile> path = new List<HexagonTile>();
			if(startTile == endTile) return path;
			List<HexagonTile> rejected = new List<HexagonTile>();
			HexagonTile current = startTile;
			path.Add(current);
			while(current!=endTile){
				current = current.Neighbours.Where(x=>!path.Contains(x) && x.Tile.IsAvailable())
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
		
		public static float Distance(HexagonTile point1, HexagonTile point2){
			return Mathf.Sqrt(Mathf.Pow(point2.Hexagon.GlobalPosition.X - point1.Hexagon.GlobalPosition.X,2) + 
							Mathf.Pow(point2.Hexagon.GlobalPosition.Y - point1.Hexagon.GlobalPosition.Y,2));
		}
		
		public static Vector2 Direction(HexagonTile tile1, HexagonTile tile2){
			return (tile2.Hexagon.GlobalPosition-tile1.Hexagon.GlobalPosition).Normalized();
		}
		
		public static Vector2 Direction(Vector2 tile1, Vector2 tile2){
			return (tile2-tile1).Normalized();
		}
		
		public static Vector2 Direction(ITile tile1, ITile tile2){
			return (new Vector2(tile2.Position.x-tile1.Position.x, tile2.Position.y - tile1.Position.y)).Normalized();
		}
		
		private static Vector2 AverageDirection(List<HexagonTile> path){
			if(path.Count==0) return new Vector2(0,0);
			Vector2 result = Direction(path.First(), path.Last());
			return result.Normalized();
		}
		
		private static Vector2 AverageDirection(List<ITile> path){
			if(path.Count==0) return new Vector2(0,0);
			Vector2 result = Direction(path.First(), path.Last());
			return result.Normalized();
		}
		
		private static float CumulativeAverageDirection(List<HexagonTile> path){
			float result = 0;
			for(int i = 0; i < path.Count; i++){
				result += Distance(AverageDirection(path.GetRange(0,i+1)),AverageDirection(path));
			}
			return result;
		}
		
		private static float CumulativeAverageDirection(List<ITile> path){
			float result = 0;
			for(int i = 0; i < path.Count ; i++){
				result+=Distance(AverageDirection(path), AverageDirection(path.GetRange(0,i+1)));
			}
			return result;
		}
		
		private static List<HexagonTile> FindShortestPath3_Helper(HexagonTile startTile, HexagonTile endTile,
		 Dictionary<HexagonTile, int> distances, List<HexagonTile> visited, int depth, List<HexagonTile> path,
		 ref int count){
			if(!distances.Keys.Contains(startTile))distances.Add(startTile, int.MaxValue);
			if(distances[startTile]<depth || visited.Contains(startTile)) return null;
			if(startTile==endTile){
				path.Add(endTile);
				distances[endTile] = depth;
				++count;
				return path;
			}
			if(!endTile.Tile.IsAvailable()) return null;
			
			path.Add(startTile);
			visited.Add(startTile);
			distances[startTile] = depth;
			List<List<HexagonTile>> paths = new List<List<HexagonTile>>();
			
			foreach(HexagonTile neighbour in startTile.Neighbours){
				if(!distances.Keys.Contains(neighbour)){
					distances[neighbour] = depth + 1;
				}
				else if(distances[neighbour] > depth +1){
					distances[neighbour] = depth+1;
				}
			}
			
			foreach(HexagonTile neighbour in startTile.Neighbours){
				if(neighbour.Tile.IsAvailable()){
					List<HexagonTile> result = FindShortestPath3_Helper(neighbour, endTile,
					 distances, new List<HexagonTile>(visited), depth+1, new List<HexagonTile>(path), ref count);
					if(result!=null){
						paths.Add(result);
					}
				}
			}
			return paths.MinBy(path => path.Count + CumulativeAverageDirection(path));
		}
		
		public static List<HexagonTile> FindShortestPath3(HexagonTile startTile, HexagonTile endTile){
			Dictionary<HexagonTile, int> distances = new Dictionary<HexagonTile, int>();
			int count = 0;
			List<HexagonTile> result = FindShortestPath3_Helper(startTile, endTile, distances,new List<HexagonTile>(), 0, new List<HexagonTile>(),ref count);
			//GD.Print(count);
			return result == null ? new List<HexagonTile>() : result;
			
		}
		
		
		public static List<ITile> FindShortestPath(ITile startTile, ITile endTile)
		{
			Dictionary<ITile,ITile> cameFrom = new Dictionary<ITile,ITile>();
			List<ITile> visited = new List<ITile>();
			Queue<ITile> queue = new Queue<ITile>();
			List<List<ITile>> paths = new List<List<ITile>>();
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
		   				paths.Add(path);
						visited.Remove(current);
				}
						foreach (ITile neighbor in current.Neighbours)
	   					{    
							if (!visited.Contains(neighbor))
							{
								visited.Add(neighbor);
								cameFrom[neighbor] = current;
								queue.Enqueue(neighbor);
							}
						}
			}
			GD.Print("FindShortestPath exited with null");
			return paths.MinBy(path=>CumulativeAverageDirection(path));
		}
		
		
		public static List<HexagonTile> FindShortestPath(HexagonTile startTile, HexagonTile endTile)
		{
			Dictionary<HexagonTile,HexagonTile> cameFrom = new Dictionary<HexagonTile,HexagonTile>();
			Dictionary<HexagonTile, int> visited = new Dictionary<HexagonTile, int>();
			Queue<HexagonTile> queue = new Queue<HexagonTile>();
			List<List<HexagonTile>> paths = new List<List<HexagonTile>>();
			queue.Enqueue(startTile);
			visited.Add(startTile,0);

			while (queue.Count > 0)
			{
				HexagonTile current = queue.Dequeue();
				
				if(current == endTile)
				{
					List<HexagonTile> path = new List<HexagonTile>();
					HexagonTile tile = current;
			   			while (tile != startTile)
						{
							path.Add(tile);
							tile = cameFrom[tile];
						}
						path.Add(startTile);
						path.Reverse();
		   				paths.Add(path);
				}
						foreach (HexagonTile neighbour in current.Neighbours)
	   					{    
							if (!visited.Keys.Contains(neighbour))
							{
								visited.Add(neighbour, visited[current]+1);
								cameFrom[neighbour] = current;
								queue.Enqueue(neighbour);
							}
							else if(visited[neighbour] >= visited[current]+1){
								queue.Enqueue(neighbour);
								visited[neighbour] = visited[current]+1;
								cameFrom[neighbour] = current;
							}
						}
			}
			GD.Print(paths.Count);
			GD.Print("FindShortestPath exited with null");
			//return paths.MinBy(path=>CumulativeAverageDirection(path).Average());
			return paths.MaxBy(path=>path.Count);
		}
		
		public static Vector2 TransformBasis(Vector2 point, Vector2 basisX, Vector2 basisY, Vector2 offset){
			return new Vector2(point.X*basisX.X + point.Y * basisY.X, point.X*basisX.Y + point.Y*basisY.Y) + offset;
		}
		
		public static void ThreeDfy(List<List<HexagonTile>> grid){
			Vector2 e1 = new Vector2(1,0.01f).Normalized();
			Vector2 e2 = new Vector2(3,1).Normalized();
			Vector2 offset = new Vector2(-100,0);
			//Vector2 offset = new Vector2(-170, 0);
			foreach(List<HexagonTile> list in grid) {
				foreach(HexagonTile tile in list){
					List<Vector2> newPolygon = new List<Vector2>();
					foreach(Vector2 point in tile.Hexagon.polygon2D.Polygon){
						newPolygon.Add(TransformBasis(point, e1, e2, offset));
					}
					tile.Hexagon.GlobalPosition = TransformBasis(tile.Hexagon.GlobalPosition,e1, e2, offset); //+ offset;
					tile.Hexagon.polygon2D.SetPolygon(newPolygon.ToArray());
					tile.Hexagon.collisionPolygon2D.SetPolygon(newPolygon.ToArray());
					newPolygon.Clear();
					foreach(Vector2 point in tile.Hexagon.innerPolygon2D.Polygon){
						newPolygon.Add(TransformBasis(point, e1, e2, offset));
					}
					tile.Hexagon.innerPolygon2D.SetPolygon(newPolygon.ToArray());
				}
			}
		}
		
	}
	
}
	
	
