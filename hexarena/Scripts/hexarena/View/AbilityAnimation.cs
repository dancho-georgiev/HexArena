using Godot;
using System;

public partial class AbilityAnimation : AnimatedSprite2D
{
	public override void _Ready(){
		Play();
		AnimationFinished += QueueFree;
	}
}
