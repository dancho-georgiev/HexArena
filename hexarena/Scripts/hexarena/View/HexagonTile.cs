using Godot;
using System;
using Interfaces;
using GameLogic;
using System.Collections.Generic;
using System.Linq;

namespace View
{
	public partial class HexagonTile : Node2D
	{
		private bool hovered;
		private bool selected = false;
		private bool validTarget = false;
		public ITile Tile;
		public Hexagon Hexagon;
		public List<HexagonTile> Neighbours;
		
		public Action<HexagonTile> TileClicked;
		public Action<HexagonTile> MouseEntered;
		public Action<HexagonTile> MouseExited;
		
		public bool Hovered {
			get{ return hovered;}
			set{
				hovered = value;
				if(hovered){
					if(!selected){
						Hexagon.polygon2D.Color = HoverColor;
					}
					
				}
				else{
					if(!selected){
						ResetColor();
					} 	
				}
		}
	}
	
	public bool ValidTarget {
			get{ return validTarget;}
			set{
				validTarget = value;
				if(validTarget){
						Hexagon.polygon2D.Color = TargetableColor;
						DefaultColor = TargetableColor;
						HoverColor = TargetedColor;
				}
				else{
					DefaultColor = new Color(1,1,1,1);
					HoverColor = new Color(1, 0, 1, 1);;
					ResetColor();} 	
				}
		}
	public bool Selected{
		get{return selected;}
		set{
			selected = value;
				if(selected){
					Hexagon.polygon2D.Color = SelectedColor;
					DefaultColor = SelectedColor;
					HoverColor = SelectedColor;
				}
				else{
					DefaultColor = new Color(1,1,1,1);
					HoverColor = new Color(1, 0, 1, 1);
					ResetColor();
				} 	
			}
		}
	
		[ExportGroup("Tile Colors")]
		[Export] public Color DefaultColor { get; set; } = new Color(1,1,1,1); //ZAHSTO NE E BQLO AAAAAAAAAA
		[Export] public Color SelectedColor { get; set; } = Colors.Blue;
		[Export] public Color MovableColor { get; set; } = Colors.Green;
		[Export] public Color HoverColor { get; set; } = new Color(1, 0, 1, 1); // Purple
		[Export] public Color TargetableColor { get; set; } = new Color(1, 0.5f, 0.5f, 1); // light Red
		[Export] public Color TargetedColor { get; set; } = new Color(1, 0f, 0f, 1); // Red
		
		

		public HexagonTile(Hexagon hexagon, ITile tile)
		{
			Hexagon = hexagon;
			Tile = tile;
			Neighbours = new List<HexagonTile>();
			AddChild(Hexagon);
			AddToGroup("HexTiles");
			Hexagon.area2D.MouseEntered += MouseEnter;
			Hexagon.area2D.MouseExited += MouseExit;
			Hexagon.area2D.InputEvent += HandleInput; 
			ResetColor();
		}
		
		public void HighlightAsSelected() => Hexagon.polygon2D.Color = SelectedColor;
		public void HighlightAsMovable() => Hexagon.polygon2D.Color = MovableColor;
		public void ResetColor() => Hexagon.polygon2D.Color = DefaultColor;
		
		public override void _Process(double delta){
		}
		
		public void MouseEnter()
		{
			Hovered = true;
			MouseEntered?.Invoke(this);
			
		}
		
		public void MouseExit()
		{
			Hovered = false;
			MouseExited?.Invoke(this);
		}
		
		private void OnTileClicked(){
				TileClicked?.Invoke(this);
				HighlightAsSelected();
				selected=!selected;
		}
		
		private void HandleInput(Node viewport, InputEvent @event, long shapeIdx)
		{
			if (@event is InputEventMouseButton mouseEvent)
			{
				if(mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left){
					OnTileClicked();
				}
				else if(!mouseEvent.Pressed){
					selected = false;
					MouseEnter();
				}		
			}
		}
		
	}
}
