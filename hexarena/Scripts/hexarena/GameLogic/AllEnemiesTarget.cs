using Godot;
using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic{
	
	public class AllEnemiesTarget : Target 
	//nqma li da e po dobre da napraish target n enemies
	//i all enemies da e 
{
	
	public AllEnemiesTarget(Grid grid) {
		this.grid = grid;
		InitializeList();
	}
	public AllEnemiesTarget(List<Enemy> allEnemies) {
		TargetList = new List<ITargetable>();
		foreach(Enemy enemy in allEnemies){
			AddTargetable(enemy);
		}
	}
	
	public override void AddTargetable(ITargetable targetable){
		if(targetable is IEnemy){
			TargetList.Add(targetable);
		}
	}
	
	public override void InitializeList(){
		TargetList = new List<ITargetable>();
		foreach(List<ITile> col in grid.TileGrid){
			foreach(ITile tile in col){
				AddTargetable(tile.CharacterOnTile);
			}
		}
	}
	
}
	
}
