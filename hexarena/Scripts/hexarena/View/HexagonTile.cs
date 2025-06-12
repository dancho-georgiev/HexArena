using Godot;
using System;
using Interfaces;
using GameLogic;

namespace View
{
	public partial class HexagonTile
	{
		public ITile Tile;
		public Hexagon Hexagon;
		
		public HexagonTile(Hexagon hexagon, ITile tile)
		{
			Hexagon = hexagon;
			Tile = tile;
			Hexagon.area2D.MouseEntered += MouseEnter;
			Hexagon.area2D.MouseExited += MouseExit;
		}
		
		public void ChangeColor(Polygon2D pol, Color color)
		{
			pol.Color = color;
		}
		
		public void MouseEnter()
		{
			ChangeColor(Hexagon.polygon2D, new Color(1, 0, 1, 1));
		}
		
		public void MouseExit()
		{
			ChangeColor(Hexagon.polygon2D, new Color(1, 1, 1, 1));
		}
	}
}
