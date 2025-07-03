using Godot;
using System;
using Interfaces;
namespace GameLogic{
	
	public class JadeTile : Tile
	{
		public JadeTile(Point position,Character character = null) : base(position, character){}
		public JadeTile(ITile other) : base(other) {}
	}
	
}
