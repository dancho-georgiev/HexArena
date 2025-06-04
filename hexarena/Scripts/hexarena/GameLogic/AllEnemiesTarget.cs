using Godot;
using System;
using Interfaces;
using System.Collections.Generic;

public class AllEnemiesTarget : ITarget
{
	public List<ITargetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
	
	public void InitializeList(){
		TargetList = new List<Enemy>();
	}
	
}
