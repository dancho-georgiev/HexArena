using Godot;
using System;

namespace View{
	
	public partial class PeasantView : GameCharacter
	{
		public override void _Ready(){
			base._Ready();
			_sprite.Material = Material.Duplicate(true) as ShaderMaterial;
			(_sprite.Material as ShaderMaterial).SetShaderParameter("rgb", new Vector3(0f,0.0f,1f));
			(_sprite.Material as ShaderMaterial).SetShaderParameter("Texture", _sprite.Texture);
		}
		
		public override void _PhysicsProcess(double delta){
			base._PhysicsProcess(delta);
		}
	}

}
