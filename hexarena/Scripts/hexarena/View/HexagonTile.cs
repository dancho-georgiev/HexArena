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
		private bool selected = false;

		[ExportGroup("Tile Colors")]
		[Export] public Color DefaultColor { get; set; } = new Color(1,1,1,1); //ZAHSTO NE E BQLO AAAAAAAAAA
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
			if(!selected){
				Hexagon.polygon2D.Color = HoverColor;
			}
			
		}
		
		public void MouseExit()
		{
			if(!selected){
				ResetColor();
			} 
		}
		
		private void OnTileClicked(Node viewport, InputEvent @event, long shapeIdx)
		{
			if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed
				 && mouseEvent.ButtonIndex == MouseButton.Left)
			{
				TileClicked?.Invoke(this);
				HighlightAsSelected();
				selected=!selected;
			}
		}
		
	}
}
