using Godot;
using System;
using Interfaces;

public abstract class Targetable : ITargetable
{
	public abstract void TakeDamage(int damage);
	public abstract void TakeStatusEffect(IStatusEffect status);
}
