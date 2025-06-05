using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
	public class AllEnemiesTarget : ITarget
{
	public List<Targetable> TargetList {get; set;}
	public uint TargetCount {get; set;}
	private Grid Grid;
	
	public AllEnemiesTarget(Grid grid) {
		Grid = grid;
		InitializeList();
	}
	
	public void InitializeList(){
		TargetList = new List<Targetable>();
		foreach(List<ITile> col in Grid.TileGrid){
			foreach(ITile tile in col){
				if(tile.CharacterOnTile is Enemy){
					TargetList.Add(tile.CharacterOnTile);
				}
			}
		}
	}
	
}
	
}
