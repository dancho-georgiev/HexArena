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
	
	[Export] public Texture2D CharacterSprite;
	private Sprite2D _sprite;
	public ITile CurrentTile { get; set; }
	public override void _Ready()
	{
		_sprite = new Sprite2D { Texture = CharacterSprite };
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
	public void MoveCharacter(ITile target)
	{

	}
}
