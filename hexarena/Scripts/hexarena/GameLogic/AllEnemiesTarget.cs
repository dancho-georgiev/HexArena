using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

public class AllEnemiesTarget : ITarget
{
	public List<ITargetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
	
	public void InitializeList(){
		List<Enemy> enemies = new List<Enemy>();
		TargetList = new List<ITargetable>(enemies);
	}
	
}
