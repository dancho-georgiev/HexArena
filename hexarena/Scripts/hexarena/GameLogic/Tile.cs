using Godot;
using System;
using DataLogic;

public partial class Tile : ITile
{
	
	public Tile(Point position,ICharacter character)
	{
		this.position = position;
		this.character = character;
	}
	
	
	
}
