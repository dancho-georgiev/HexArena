using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using GameLogic;
using View;
using Utilities;
using System.Linq;

namespace View{
	public partial class GameCharacter : Node2D
	{
		public ICharacter Character {get; set;}
		[Export] public Texture2D CharacterSpriteTexture;
		[Export] public float MoveSpeed = 100f;
		
		private Sprite2D _sprite;
		public HexagonTile CurrentTile { get; set; }
		protected bool _isMoving = false;
		protected HexagonTile _targetTile;
	//<<<<<<< HEAD
		//private Queue<HexagonTile> _hexPath = new Queue<HexagonTile>();
	//=======
		protected List<HexagonTile> _hexPath = new List<HexagonTile>();
		protected int _currentPathIndex;
		
		public override void _Ready()
		{
			_sprite = new Sprite2D { 
				Texture = CharacterSpriteTexture,
				Centered = true
			 };
			_sprite.Scale = new Vector2(0.1f,0.1f);
			AddChild(_sprite);
		}
		
		
		public override void _PhysicsProcess(double delta)
		{
			if (_isMoving)
			{
				HandleMovement((float)delta);
			}
		}
		
		public void MoveVisualCharacter(List<HexagonTile> path){
			 if (CurrentTile == null || path == null) return;
			List<ITile> pathTiles = path.Select(x => x.Tile).ToList();
			_hexPath = new List<HexagonTile>();
			
			//convert logical tiles to visual hexagonTiles
			 foreach (ITile tile in pathTiles)
			{
				HexagonTile hexTile = FindHexagonTileByITile(tile);
				if (hexTile != null)
				{
					_hexPath.Add(hexTile);
				}
			}
			
			if (_hexPath.Count > 0)
				_currentPathIndex = 0; 
				if (_currentPathIndex < _hexPath.Count)
				{
					_targetTile = _hexPath[_currentPathIndex];
					_isMoving = true;
				}
		}
		 private HexagonTile FindHexagonTileByITile(ITile tile)
		{	
			// Search through all nodes in HexTiles group- Check HexTiles
			//I dont know if its a good practice
			 foreach (Node node in GetTree().GetNodesInGroup("HexTiles"))
			{
				if (node is HexagonTile hexTile && hexTile.Tile == tile)
				{
					//GD.Print($"HexTile Tile {hexTile.Tile.Position.x},{hexTile.Tile.Position.y}" );	
					return hexTile;
				}
			}
			//GD.PrintErr($"FindHexagonTileByITile returns null");
			return null;
		}
		
		private void HandleMovement(float delta)
		{
			if (_targetTile == null) return;
			
			Vector2 targetPosition = GetTargetPosition();
			Vector2 direction = (targetPosition - GlobalPosition).Normalized();
			Vector2 movement = direction * MoveSpeed * delta;
			
			// Move towards target
			GlobalPosition += movement;
			
			// Check if reached target
			if (GlobalPosition.DistanceTo(targetPosition) < 5f)
			{
				CurrentTile = _targetTile;
				
				// Move to next tile in path
				_currentPathIndex++;
				
				 // If more tiles remain
				 if (_currentPathIndex < _hexPath.Count)
				{
					// Set next target
					_targetTile = _hexPath[_currentPathIndex];
				}
				else
				{
					FinishMovement();
				}
			}
		}
		 private void FinishMovement()
		{
			_isMoving = false;
			GlobalPosition = GetTargetPosition(); // Snap to exact position
			Character.Tile = _targetTile.Tile;
			CurrentTile = _targetTile;
			//if(_hexPath.Count>0) nextTarget();
			
			//can start an idle animiation kato imame
		}
		
		private Vector2 GetTargetPosition()
		{
			if (_targetTile.Hexagon is Node2D)
			{
				return _targetTile.Hexagon.GlobalPosition*2;
			}
			return GlobalPosition;
		}
	}
	
}
