using Godot;
using System;
using Interfaces;

namespace GameLogic{
	
	public class ChangedTileEventArgs : EventArgs
	{
		public ITile Tile {get; private set;}
		public ChangedTileEventArgs(ITile tile){
			Tile = tile;
		}
	}
	
}
