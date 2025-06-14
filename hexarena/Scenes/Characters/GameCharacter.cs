using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using GameLogic;

public partial class GameCharacter : CharacterBody2D, ICharacter
{
	public double StepEnergyCost { get;  set; }
	public int Health { get; set; }
	public List<IStatusEffect> StatusEffects {get;  set;}= new();
	public ITile Tile { get; set; }
	public List<IActive> ActiveAbilities { get; set; } = new();
	public List<IPassive> PassiveAbilities { get; set; } = new();
	
	[Export] public Texture2D CharacterSpriteTexture;
	[Export] public float MoveSpeed = 100f;
	
	private Sprite2D _sprite;
	public ITile CurrentTile { get; set; }
	private bool _isMoving = false;
	private ITile _targetTile;
	
	public override void _Ready()
	{
		_sprite = new Sprite2D { 
			Texture = CharacterSpriteTexture,
			Centered = true
		 };
		AddChild(_sprite);
	}
	
	public void TakeDamage(int damage)
	{
		Health -= damage;
		 // To do: Add damage animation
	}
	public void TakeStatusEffect(IStatusEffect statusEffect)
	{
		StatusEffects.Add(statusEffect);
		// Optional: Add visual effect
	}
	
	 public override void _PhysicsProcess(double delta)
	{
		if (_isMoving)
		{
			HandleMovement((float)delta);
		}
	}
	public void MoveCharacter(ITile target)
	{
		if (Tile == target) return;
		_targetTile = target;
		_isMoving = true;
		//walk animation
	}
	private void HandleMovement(float delta)
	{
		if (_targetTile == null) return;
		
		var targetPosition = GetTargetPosition();
		var direction = (targetPosition - GlobalPosition).Normalized();
		var movement = direction * MoveSpeed * delta;
		
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
		Tile = _targetTile;
		//can start an idle animiation kato imame
	}
	
	private Vector2 GetTargetPosition()
	{
		if (_targetTile is Node2D tileNode)
		{
			return tileNode.GlobalPosition;
		}
		return GlobalPosition;
	}
}
