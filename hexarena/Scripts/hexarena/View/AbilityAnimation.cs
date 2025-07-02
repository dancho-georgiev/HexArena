using Godot;
using System;

public partial class AbilityAnimation : AnimatedSprite2D
{
	public override void _Ready(){
		ZIndex = 3;
		Play();
		AnimationFinished += QueueFree;
	}
}
