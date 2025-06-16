using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using GameLogic;
using View;
using Utilities;


public partial class GameCharacter : Node2D
{
	public ICharacter Character {get; set;}
	[Export] public Texture2D CharacterSpriteTexture;
	[Export] public float MoveSpeed = 100f;
	
	private Sprite2D _sprite;
	public HexagonTile CurrentTile { get; set; }
	private bool _isMoving = false;
	private HexagonTile _targetTile;
	private Queue<HexagonTile> _hexPath = new Queue<HexagonTile>();
	
	public override void _Ready()
	{
		_sprite = new Sprite2D { 
			Texture = CharacterSpriteTexture,
			Centered = true
		 };
		_sprite.Scale = new Vector2(0.1f,0.1f);
		AddChild(_sprite);
	}
	
	//public void setCharacter(ICharacter _character, Sprite2D _sprite){ //idontgetit
		//Character = _character;
		//
	//}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_isMoving)
		{
			HandleMovement((float)delta);
		}
	}
	public void MoveVisualCharacter(HexagonTile target)
	{
		 _hexPath = new Queue<HexagonTile>(Utility.FindShortestPath2(CurrentTile, target));
		
		GD.Print(_hexPath.Count);
		
		nextTarget();
		
		//walk animation
	}
	
	private void nextTarget(){
		_targetTile = _hexPath.Dequeue();
		_isMoving = true;
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
			FinishMovement();
		}
	}
	 private void FinishMovement()
	{
		_isMoving = false;
		GlobalPosition = GetTargetPosition(); // Snap to exact position
		Character.Tile = _targetTile.Tile;
		CurrentTile = _targetTile;
		if(_hexPath.Count>0) nextTarget();
		
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
