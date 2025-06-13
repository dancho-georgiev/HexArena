using Godot;
using System;
using Interfaces;
using GameLogic;

namespace View
{
	public partial class HexagonTile : Node2D
	{
		public ITile Tile;
		public Hexagon Hexagon;
		public Action<HexagonTile> TileClicked;
		
		//[Export] public Color DefaultColor = Colors.White;
		//[Export] public Color SelectedColor = Colors.Blue;
		//[Export] public Color MovableColor = Colors.Green;
		//
		//public void HighlightAsSelected() => polygon2D.Color = SelectedColor;
		//public void HighlightAsMovable() => polygon2D.Color = MovableColor;
		//public void ResetColor() => polygon2D.Color = DefaultColor;
		
		public HexagonTile(Hexagon hexagon, ITile tile)
		{
			Hexagon = hexagon;
			Tile = tile;
			
			AddChild(Hexagon);
			
			Hexagon.area2D.MouseEntered += MouseEnter;
			Hexagon.area2D.MouseExited += MouseExit;
			 Hexagon.area2D.InputEvent += OnTileClicked; 
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
		
		private void OnTileClicked(Node viewport, InputEvent @event, long shapeIdx)
		{
			if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
			{
				TileClicked?.Invoke(this);
			}
		}
		
	}
}
