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
		List<Enemy> targets = new List<Enemy>();
		foreach(Enemy enemy in allEnemies){
			targets.Add(enemy);
		}
		TargetList = targets;
	}
	
	public override void InitializeList(){
		List<Character> targets = new List<Character>();
		foreach(List<ITile> col in Grid.TileGrid){
			foreach(ITile tile in col){
				if(tile.CharacterOnTile is Enemy){
					targets.Add(tile.CharacterOnTile);
				}
			}
		}
		TargetList = targets;
	}
	
}
	
}
