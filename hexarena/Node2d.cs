using Godot;
using System;
using GameLogic;
using Interfaces;
using System.Linq;
using System.Collections.Generic;
using Utilities;

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
		Test(Test_Pathfinding,"Test_Pathfinding");
		Test(Test_SetupNeighbours,"Test_SetupNeighbours");
		Test(Test_MoveCharacter,"Test_MoveCharacter");
		Test(Test_SurroundSelfTarget, "Test_SurroundSelfTarget");
		Test(Test_TileRangeBFS, "Test_TileRangeBFS");
		Test(Test_PoisonStatusEffect, "Test_PoisonStatusEffect");
		Test(Test_SweepFrontTarget, "Test_SweepFrontTarget");
		Test(Test_BasicAttack, "Test_BasicAttack");
		GD.Print($"PASSED: {passedTest}");
		GD.Print($"FAILED: {allTest-passedTest}");
	}
	
	private void Printer(){
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
		Test(()=>{if(grid.Width==3)passedTest++;},  "grid.width==3");
		Test(()=>{if(grid.Length==5)passedTest++;}, "grid.length==5");
		Test(()=>{if(grid.Enemies.Count==0)passedTest++;}, "grid.enemies.empty");
		Test(()=>{if(grid.Players.Count==0)passedTest++;}, "grid.players.empty");
		Test(()=>{if(grid.TileGrid.Count==3)passedTest++;}, "grid.tileGrid.Count==width");
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
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Grid grid = new Grid(3,5);
		Enemy enemy1 = new Enemy(grid.TileGrid[1][2]);
		Enemy enemy2 = new Enemy(grid.TileGrid[2][1]);
		grid.AddEnemy(enemy1);
		grid.AddEnemy(enemy2);
		AllEnemiesTarget target = new AllEnemiesTarget(grid);
		Test(()=>{if(target.TargetList.Count() == 2)passedTest++;}, "targeted all enemies");
		Test(()=>{if(target.TargetList.Any(x=>(x as Enemy).Tile.Position == new Point(1,2)))passedTest++;}, "correct enemy");
		MalevolentShrine slash = new MalevolentShrine(eventManager, target);
		eventManager.EmitOnStartTurn();
		Test(()=>{if(enemy1.Health<100)passedTest++;}, "dealt damage to enemy1");
		Test(()=>{if(enemy2.Health<100)passedTest++;}, "dealt damage to enemy2");
		if(passed+4==passedTest)passedTest++;	
	}
	private void Test_SetupNeighbours()
	{
		int passed = passedTest;

		Grid grid = new Grid(4, 4);
		grid.SetupNeighbours(); 

		// centura trqbva da ima 6
		ITile center = grid.TileGrid[2][2];
		Test(() => { if (center.Neighbours.Count == 6) passedTest++; }, "center tile has 6 neighbors");

		// uglite trqbva da imat 2 ili 3 saseda
		ITile corner = grid.TileGrid[0][0];
		Test(() => { if (corner.Neighbours.Count >= 2 && corner.Neighbours.Count <= 3) passedTest++; }, "corner tile has correct neighbor count");

		//TO DO CHECK IF A SPECIFIC tile Is a neighbour

		if (passed + 2 == passedTest)
			passedTest++;
	}
	
	private void Test_Pathfinding(){ //HOLY SHIT CODA MI RABOTI
		Grid grid = new Grid(4,4);
		ITile start = grid.TileGrid[0][0];
		ITile end = grid.TileGrid[3][3];
		
		grid.SetupNeighbours();
		
		Character character = new Character(100, 1, start);
			
			ITile obstacle1 = grid.TileGrid[2][0];
			obstacle1.IsAvailable = false;
		
			ITile obstacle3 = grid.TileGrid[1][2];
			obstacle3.IsAvailable = false;
		  List<ITile> path = character.FindShortestPath(start, end);
		Test(() => { if (path != null) passedTest++; }, "path is not null");	
		Test(() => { if (path[0] == start) passedTest++; }, "path starts at start");
		Test(() => { if (path[path.Count - 1] == end) passedTest++; }, "path ends at end");
		Test(() => { if (path.Count >= 3) passedTest++; }, "path has reasonable length");
		GD.Print($"{path.Count} path count");
	}
	
	private void Test_MoveCharacter(){
		int passed = passedTest;
		Grid grid = new Grid(4, 4);
		Character character = new Character(100, 1, grid.TileGrid[0][0]);
		ITile TargetPosition = grid.TileGrid[3][3];
		ITile obstacle1 = grid.TileGrid[2][0];
			obstacle1.IsAvailable = false;
			ITile obstacle3 = grid.TileGrid[1][2];
			obstacle3.IsAvailable = false;
		Test(() => { if (character.Tile == grid.TileGrid[0][0] ) passedTest++; }, "start position is not right");	
		character.MoveCharacter(TargetPosition);
		Test(() => { if (character.Tile == TargetPosition) passedTest++; }, "final position is not right");	
		if (passed + 2 == passedTest)
			passedTest++;
	}
	
	private void Test_SurroundSelfTarget()
	{
		int passed = passedTest;
		Grid grid = new Grid(6, 6);
		EventManager eventManager = new EventManager();
		Character character = new Character(100, 1, grid.TileGrid[3][3]);
		Character character2 = new Character(100, 1, grid.TileGrid[3][2]);
		SurroundSelfTarget surroundTargeting = new SurroundSelfTarget(character.Tile, 1);
		SwordSpin spinSword = new SwordSpin(eventManager, surroundTargeting);
		eventManager.EmitOnActivateAbility1();
		Test(()=>{if(surroundTargeting.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health==100)passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health<100)passedTest++;}, "dealt damage to neighbouring character");
		if(passed+3==passedTest)passedTest++;	
	}
	
	private void Test_SweepFrontTarget()
	{
		int passed = passedTest;
		Grid grid = new Grid(5, 5);
		EventManager eventManager = new EventManager();
		Character character = new Character(100, 1, grid.TileGrid[3][3]);
		Character character2 = new Character(100, 1, grid.TileGrid[3][2]);
		Character character3 = new Character(100, 1, grid.TileGrid[2][2]);
		Character character4 = new Character(100, 1, grid.TileGrid[2][4]);
		SweepFrontTarget sweepTargeting = new SweepFrontTarget (character.Tile, character2.Tile);
		SwordSweep sweepSword = new SwordSweep(eventManager, sweepTargeting);
		
		eventManager.EmitOnActivateAbility1();
		Test(()=>{if(sweepTargeting.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health==100)passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health<100)passedTest++;}, "dealt damage to main target");
		Test(()=>{if(character3.Health<100)passedTest++;}, "dealt damage adjacent to main target");
		Test(()=>{if(character4.Health==100)passedTest++;}, "did not deal damage behind itself");
		if(passed+5==passedTest)passedTest++;	
	}

	private void Test_PoisonStatusEffect(){
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Character character = new Character(100, 1, new Tile(new Point(1,1)));
		PoisonEffect poison = new PoisonEffect(10, 2, eventManager,character);
		character.TakeStatusEffect(poison);
		Test(()=>{if(character.StatusEffects.Count()==1)passedTest++;}, "added status effect");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == 90)passedTest++;}, "took damage 1");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == 80)passedTest++;}, "took damage 2");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == 80)passedTest++;}, "stopped taking damage");
		
		if(passed+4==passedTest)passedTest++;	
	}
	
	private void Test_BasicAttack()
	{
		Grid grid = new Grid(1, 2);
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Character character = new Character(100, 1, grid.TileGrid[0][0]);
		Character character2 = new Character(100, 1, grid.TileGrid[0][1]);
		
		SingleTarget targetSingle = new SingleTarget (character.Tile, character2.Tile, 1);
		SwordSlash slashSword = new SwordSlash(eventManager, targetSingle);
		eventManager.EmitOnActivateAbility1();
		
		Test(()=>{if(targetSingle.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health==100)passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health<100)passedTest++;}, "dealt damage to main target");
		if(passed + 3 == passedTest)passedTest++;
	}
	
	private void Test_TileRangeBFS(){
		Grid grid = new Grid(6,6);
		int passed = passedTest;
		Test(()=>{if(Utility.TileRangeBFS(grid.TileGrid[0][0], grid.TileGrid[1][1], 1))passedTest++;}, "find target 1");
		Test(()=>{if(Utility.TileRangeBFS(grid.TileGrid[0][0], grid.TileGrid[1][2], 2))passedTest++;}, "find target 2");
		Test(()=>{if(Utility.TileRangeBFS(grid.TileGrid[0][0], grid.TileGrid[2][2], 3))passedTest++;}, "find target 3");
		if(passed + 3 == passedTest)passedTest++;
	}
}
