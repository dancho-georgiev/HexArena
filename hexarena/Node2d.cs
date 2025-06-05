using Godot;
using System;
using GameLogic;
using Interfaces;
using System.Linq;
public partial class Node2d : Node2D
{
	private EventManager eventManager;
	private int passedTest = 0;
	private int allTest = 0;
	private int passedTest1 = 0;
	
	public override void _Ready()
	{
		Test(Test_OnStartTurnEvent,"Test_OnStartTurnEvent");
		Test(Test_GridWidthLenghtConstructor, "GridWidthLengthConstructor");
		Test(Test_EnemyConstructorTile, "EnemyConstructorTile");
		Test(Test_GridAddEnemy, "GridAddEnemy");
		Test(Test_AllEnemiesTarget, "AllEnemiesTarget and Slash");
		GD.Print($"PASSED: {passedTest}");
		GD.Print($"FAILED: {allTest-passedTest}");
	}
	
	private void Printer()
	{
		passedTest += 1;
	}
	
	private void Test(Action func,string name){
		allTest+=1;
		passedTest1 = passedTest;
		func();
		if(passedTest1 == passedTest){
			GD.Print($"{name} has failed ");
		}
	}
	
	private void Test_OnStartTurnEvent(){
		// Instance and add to scene tree
		eventManager = new EventManager();
		AddChild(eventManager);
		// Connect signal to method
		eventManager.StartTurn += Printer;
		// Emit the signal
		eventManager.EmitOnStartTurn();
	}
	private void Test_GridWidthLenghtConstructor(){
		Grid grid = new Grid(3,5);
		int passed = passedTest;
		Test(()=>{if(grid.Length==3)passedTest++;}, "grid.length==3");
		Test(()=>{if(grid.Width==5)passedTest++;},  "grid.width==5");
		Test(()=>{if(grid.Enemies.Count==0)passedTest++;}, "grid.enemies.empty");
		Test(()=>{if(grid.Players.Count==0)passedTest++;}, "grid.players.empty");
		Test(()=>{if(grid.TileGrid.Count==5)passedTest++;}, "grid.tileGrid.Count==width");
		Test(()=>{if(grid.TileGrid[1][2].Position == new Point(2,1))passedTest++;}, "correct coordinates");
		if(passed + 6 == passedTest) passedTest++;
	}
	private void Test_EnemyConstructorTile(){
		Grid grid = new Grid(3,5);
		Enemy enemy = new Enemy(grid.TileGrid[1][2]);
		int passed = passedTest;
		Test(()=>{if(enemy.Health==100)passedTest++;}, "enemy.Health==100");
		Test(()=>{if(enemy.StepEnergyCost==1)passedTest++;}, "enemy.StepEnergyCost==1");
		Test(()=>{if(enemy.Tile.Position == new Point(2,1))passedTest++;}, "correct position");
		Test(()=>{if(enemy.StatusEffects.Count == 0)passedTest++;}, "StatusEffects is empty");
		if(passed + 4 == passedTest) passedTest++;
	}
	private void Test_GridAddEnemy(){
		Grid grid = new Grid(3,5);
		int passed = passedTest;
		Enemy enemy = new Enemy(grid.TileGrid[1][2]);
		grid.AddEnemy(enemy);
		Test(()=>{if(grid.Enemies.Count==1)passedTest++;}, "added enemy");
		if(passed + 1 == passedTest) passedTest++;
	}
	private void Test_AllEnemiesTarget(){
		EventManager eventManager = new EventManager();
		Grid grid = new Grid(3,5);
		Enemy enemy1 = new Enemy(grid.TileGrid[1][2]);
		Enemy enemy2 = new Enemy(grid.TileGrid[2][1]);
		AllEnemiesTarget target = new AllEnemiesTarget(grid);
		Test(()=>{if(target.TargetList.Count == 2)passedTest++;}, "targeted all enemies");
		Test(()=>{if(target.TargetList.Any(x=>(x as Enemy).Tile.Position == new Point(1,2)))passedTest++;}, "correct enemy");
		Slash slash = new Slash(eventManager);
		eventManager.EmitOnStartTurn();
		Test(()=>{if(enemy1.Health<100)passedTest++;}, "dealt damage to enemy1");
		Test(()=>{if(enemy2.Health<100)passedTest++;}, "dealt damage to enemy2");
		
	}
	
	
}
