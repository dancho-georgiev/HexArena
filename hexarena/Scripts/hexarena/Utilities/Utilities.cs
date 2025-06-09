using Godot;
using System;
using Interfaces;
using GameLogic;
using System.Collections.Generic;
using System.Linq;

namespace Utilities{
	
	public static class Utility{
		private static bool TileRangeBFS_Helper(ITile start, ITile target, int depth, HashSet<ITile> visited){
			if(start.Position == target.Position) return true;
			if(depth == 0) return false;
			visited.Add(start);
			List<bool> isTrue = new List<bool>();
			foreach(ITile neighbour in start.Neighbours){
				if(!visited.Contains(neighbour)){
					isTrue.Add(TileRangeBFS_Helper(neighbour, target, depth-1, visited));
				}
			}
			return isTrue.Contains(true); 
		}
	
		public static bool TileRangeBFS(ITile start, ITile target, int depth){
			HashSet<ITile> visited = new HashSet<ITile>();
			return TileRangeBFS_Helper(start, target, depth, visited);
		}
	
	}
	
	
}
