using Godot;
using System;

namespace View{
	
public partial class JadeCharacterView : GameCharacter
{
	public override void _Ready(){
		base._Ready();
		_sprite.Scale = new Vector2(1f,1f);
		_sprite.Material = Material.Duplicate(true) as ShaderMaterial;
		(_sprite.Material as ShaderMaterial).SetShaderParameter("rgb", new Vector3(0f,0.73f,0.46f));
		(_sprite.Material as ShaderMaterial).SetShaderParameter("Texture", _sprite.Texture);
	}
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
	}
}
	
}
