using Godot;
using System;

namespace View{
	
public partial class JadeCharacter : GameCharacter
{
	public override void _Ready(){
		base._Ready();
		_sprite.Scale = new Vector2(1f,1f);
		_sprite.Material = Material;
		(Material as ShaderMaterial).SetShaderParameter("rgb", new Vector3(0f,0.73f,0.46f));
		(Material as ShaderMaterial).SetShaderParameter("Texture", _sprite.Texture);
	}
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
	}
}
	
}
