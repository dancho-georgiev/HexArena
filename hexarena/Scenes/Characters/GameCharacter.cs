using Godot;
using System;
using System.Collections.Generic;
using Interfaces;
using GameLogic;
using View;
using Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace View{
	public partial class GameCharacter : Node2D
	{
		public ICharacter Character {get; set;}
		[Export] public Texture2D CharacterSpriteTexture;
		[Export] public float MoveSpeed = 100f;
		
		protected Sprite2D _sprite;
		public HexagonTile CurrentTile { get; set; }
		public bool IsMoving = false;
		public bool StartAnimation = false;
		
		private string abilityName;
		private ITarget target;
		
		protected HexagonTile _targetTile;

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
			Character.HasMoved += MoveVisualCharacter;
			Character.ActivatedAbility += InstantiateAbilityAnimation;
		}
		
		
		public override void _PhysicsProcess(double delta)
		{
			if (IsMoving)
			{
				HandleMovement((float)delta);
			}
			else if(StartAnimation){
				InstantiateAbilityAnimation(abilityName, target);
			}
		}
		
		public void InstantiateAbilityAnimation(string abilityName, ITarget target){
			if(!IsMoving){
				PackedScene scene = GD.Load<PackedScene>($"res://Assets/AbilityAnimations/{abilityName}Animation.tscn");
				AnimatedSprite2D animation = scene.Instantiate<AnimatedSprite2D>();
				
				AddChild(animation);
			}
			else{
				StartAnimation = true;
				this.abilityName = abilityName;
				this.target = target;
			}
		}
		
		public virtual void MoveVisualCharacter(List<ITile> path){
			if (CurrentTile == null || path == null) return;
			List<ITile> pathTiles = path;
			_hexPath = new List<HexagonTile>();
			
			//convert logical tiles to visual hexagonTiles
			foreach (ITile tile in pathTiles)
			{
				HexagonTile hexTile = Utility.FindHexagonTileByITile(tile, GetTree());
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
					IsMoving = true;
				}
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
			IsMoving = false;
			GlobalPosition = GetTargetPosition(); // Snap to exact position
			Character.Tile = _targetTile.Tile;
			CurrentTile = _targetTile;
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
