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
		
		// ne znam tochno kak rabotat tezi grupi kusmet guys
		[ExportGroup("Tile Colors")]
		[Export] public Color DefaultColor { get; set; } = Colors.White; //ZAHSTO NE E BQLO AAAAAAAAAA
		//qvno tova e bqlo???????? 
		//mislq che ot nqkude drugade vzima cvqt i se dobavq otgore guys
		//ok opraih go veche e bqlo
		//Debug -> CollisionShapes off
		[Export] public Color SelectedColor { get; set; } = Colors.Blue;
		[Export] public Color MovableColor { get; set; } = Colors.Green;
		[Export] public Color HoverColor { get; set; } = new Color(1, 0, 1, 1); // Purple

		public HexagonTile(Hexagon hexagon, ITile tile)
		{
			Hexagon = hexagon;
			Tile = tile;
			
			AddChild(Hexagon);
			
			Hexagon.area2D.MouseEntered += MouseEnter;
			Hexagon.area2D.MouseExited += MouseExit;
			Hexagon.area2D.InputEvent += OnTileClicked; 
			ResetColor();
		}
		public void HighlightAsSelected() => Hexagon.polygon2D.Color = SelectedColor;
		public void HighlightAsMovable() => Hexagon.polygon2D.Color = MovableColor;
		public void ResetColor() => Hexagon.polygon2D.Color = DefaultColor;
		

		
		public void MouseEnter()
		{
			Hexagon.polygon2D.Color = HoverColor;
		}
		
		public void MouseExit()
		{
			 ResetColor();
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
