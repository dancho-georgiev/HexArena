using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
	public class AllEnemiesTarget : Target
{
	private Grid Grid;
	
	public AllEnemiesTarget(Grid grid) {
		Grid = grid;
		InitializeList();
	}
	public AllEnemiesTarget(List<Enemy> allEnemies) {
		TargetList = new List<Targetable>();
		foreach(Enemy enemy in allEnemies){
			TargetList.Add(enemy);
		}
	}
	
	public override void InitializeList(){
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
