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
		//Test(Test_BasicAttack, "Test_BasicAttack");
		Test(Test_PassiveHealEffect, "Test_PassiveHealEffect");
		Test(Test_PoisonedStrike,"Test_PoisonedStrike");
		Test(Test_VulnerableEffect,"Test_VulnerableEffect");
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
		Test(()=>{if(grid.TileGrid.Count==3)passedTest++;}, "grid.tileGrid.Count==width");
		Test(()=>{if(grid.TileGrid[1][2].Position == new Point(2,1))passedTest++;}, "correct coordinates");
		if(passed + 6 == passedTest) passedTest++;
	}
	//Peasant is abstract now GG
	private void Test_EnemyConstructorTile(){
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		PlaceholderEnemy enemy = new PlaceholderEnemy(100, 1);
		grid.PlaceEnemy(enemy, grid.GetTile(2,1));
		int passed = passedTest;
		Test(()=>{if(enemy.Health==100)passedTest++;}, "enemy.Health==100");
		Test(()=>{if(enemy.StepEnergyCost==1)passedTest++;}, "enemy.StepEnergyCost==1");
		Test(()=>{if(enemy.Tile.Position == new Point(1,2))passedTest++;}, "correct position");
		Test(()=>{if(enemy.StatusEffects.Count == 0)passedTest++;}, "StatusEffects is empty");
		if(passed + 4 == passedTest) passedTest++;
	}
	private void Test_GridAddEnemy(){
		EventManager ev = new EventManager();
		BattleField grid = new BattleField(ev);
		int passed = passedTest;
		PlaceholderEnemy enemy = new PlaceholderEnemy(100, 1);
		grid.PlaceEnemy(enemy, grid.GetTile(1,2));
		Test(()=>{if(grid.Enemies.Count==1)passedTest++;}, "added enemy");
		if(passed + 1 == passedTest) passedTest++;
	}
	private void Test_AllEnemiesTarget(){
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		PlaceholderEnemy enemy1 = new PlaceholderEnemy(100, 1);
		PlaceholderEnemy enemy2 = new PlaceholderEnemy(100, 1);
		grid.PlaceEnemy(enemy1, grid.GetTile(1,2));
		grid.PlaceEnemy(enemy2, grid.GetTile(2,1));
		
		AllEnemiesTarget target = new AllEnemiesTarget();
		target.SetBattleField(grid);
		Test(()=>{if(target.TargetList.Count() == 2)passedTest++;}, "targeted all enemies");
		Test(()=>{if(target.TargetList.Any(x=>(x as Enemy).Tile.Position == new Point(1,2)))passedTest++;}, "correct enemy");
		Peasant peasant = new Peasant(eventManager);
		MalevolentShrine slash = new MalevolentShrine(eventManager);
		peasant.PassiveAbilities.Add(slash);
		grid.PlacePlayer(peasant, grid.GetTile(0,0));
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
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		ITile start = grid.GetTile(0, 0);
		ITile end = grid.GetTile(3,3);
		
		Peasant character = new Peasant(eventManager);
		grid.PlacePlayer(character, start);
			ITile obstacle1 = grid.GetTile(2,0);
			obstacle1.IsAvailable = false;
		
			ITile obstacle3 = grid.GetTile(1,2);
			obstacle3.IsAvailable = false;
		  List<ITile> path = character.FindShortestPath(start, end);
		Test(() => { if (path != null) passedTest++; }, "path is not null");	
		Test(() => { if (path[0] == start) passedTest++; }, "path starts at start");
		Test(() => { if (path[path.Count - 1] == end) passedTest++; }, "path ends at end");
		Test(() => { if (path.Count >= 3) passedTest++; }, "path has reasonable length");
		 //GD.Print($"{path.Count} path count");
	}
	
	private void Test_MoveCharacter(){
		int passed = passedTest;
		
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		Peasant character = new Peasant(eventManager);
		grid.PlacePlayer(character, grid.GetTile(0,0));
		ITile TargetPosition = grid.GetTile(3,3);
		ITile obstacle1 = grid.GetTile(2,0);
		obstacle1.IsAvailable = false;
		ITile obstacle3 = grid.GetTile(1,2);
		obstacle3.IsAvailable = false;
		Test(() => { if (character.Tile == grid.GetTile(0,0)) passedTest++; }, "start position is not right");	
		character.MoveCharacter(TargetPosition);
		Test(() => { if (character.Tile == TargetPosition) passedTest++; }, "final position is not right");	
		if (passed + 2 == passedTest)
			passedTest++;
	}
	
	private void Test_SurroundSelfTarget()
	{
		int passed = passedTest;
		
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		Peasant character = new Peasant(eventManager);
		grid.PlacePlayer(character, grid.GetTile(3,3));
		Peasant character2 = new Peasant(eventManager);
		grid.PlacePlayer(character2, grid.GetTile(3,2));
		SurroundSelfTarget surroundTargeting = new SurroundSelfTarget(character.Tile, 1);
		SwordSpin spinSword = new SwordSpin(eventManager, surroundTargeting);
		
		List<int> oldHealth = new List<int>(){character.Health, character2.Health};
		eventManager.EmitOnActivateAbility1();
		Test(()=>{if(surroundTargeting.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health == oldHealth[0])passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health == oldHealth[1] - spinSword.Damage)passedTest++;}, "dealt damage to neighbouring character");
		if(passed+3==passedTest)passedTest++;	
	}
	
	private void Test_SweepFrontTarget()
	{
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		
		BattleField grid = new BattleField(eventManager);
		
		Peasant character = new Peasant(eventManager);
		grid.PlacePlayer(character, grid.GetTile(3,3));
		Peasant character2 = new Peasant(eventManager);
		grid.PlacePlayer(character2, grid.GetTile(3,2));
		Peasant character3 = new Peasant(eventManager);
		grid.PlacePlayer(character3, grid.GetTile(2,2));
		Peasant character4 = new Peasant(eventManager);
		grid.PlacePlayer(character4, grid.GetTile(2,4));
		SweepFrontTarget sweepTargeting = new SweepFrontTarget (character.Tile, character2.Tile);
		SwordSweep sweepSword = new SwordSweep(eventManager, sweepTargeting);
		
		
		List<int> oldCharacterHealth = new List<int>(){character.Health, character2.Health, character3.Health, character4.Health};
		
		eventManager.EmitOnActivateAbility1();
		Test(()=>{if(sweepTargeting.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health  == oldCharacterHealth[0])passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health == oldCharacterHealth[1]-sweepSword.Damage)passedTest++;}, "dealt damage to main target");
		Test(()=>{if(character3.Health == oldCharacterHealth[2]-sweepSword.Damage)passedTest++;}, "dealt damage adjacent to main target");
		Test(()=>{if(character4.Health == oldCharacterHealth[3])passedTest++;}, "did not deal damage behind itself");
		if(passed+5==passedTest)passedTest++;	
	}

	private void Test_PoisonStatusEffect(){
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Peasant character = new Peasant(eventManager);
		PoisonEffect poison = new PoisonEffect(10, 2, eventManager,character);
		
		int oldHealth = character.Health;
		character.TakeStatusEffect(poison);
		Test(()=>{if(character.StatusEffects.Count()==1)passedTest++;}, "added status effect");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth-(poison.Damage*1))passedTest++;}, "took damage 1");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth-(poison.Damage*2))passedTest++;}, "took damage 2");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth-(poison.Damage*2))passedTest++;}, "stopped taking damage");
		
		if(passed+4==passedTest)passedTest++;	
	}
	
		private void Test_PassiveHealEffect(){
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Peasant character = new Peasant(eventManager);
		//Character character = new Peasant(eventManager, new Tile(new Point(1,1)));
		PassiveHealEffect heal = new PassiveHealEffect(10, 2, eventManager,character);
		int oldHealth = character.Health;
		character.TakeStatusEffect(heal);
		Test(()=>{if(character.StatusEffects.Count()==1)passedTest++;}, "added status effect");

		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth+(heal.healAmount*1))passedTest++;}, "took damage 1");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth+(heal.healAmount*2))passedTest++;}, "took damage 2");
		eventManager.EmitOnStartTurn();
		Test(()=>{if(character.Health == oldHealth+(heal.healAmount*2))passedTest++;}, "stopped taking damage");
	}
	

	private void Test_BasicAttack()
	{
		
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		Peasant character = new Peasant(eventManager);
		grid.PlacePlayer(character, grid.GetTile(0,0));
		Peasant character2 = new Peasant(eventManager);
		grid.PlacePlayer(character2, grid.GetTile(0,1));
		SingleTarget targetSingle = new SingleTarget(character.Tile, character2.Tile, 1);
		SwordSlash slashSword = new SwordSlash(eventManager, targetSingle);
		
		List<int> oldHealth = new List<int>(){character.Health, character2.Health};
		eventManager.EmitOnActivateAbility1();
		
		Test(()=>{if(targetSingle.TargetInRange(character2))passedTest++;}, "comprehended character2 in range");
		Test(()=>{if(character.Health==oldHealth[0])passedTest++;}, "did not damage self");
		Test(()=>{if(character2.Health==oldHealth[1]-slashSword.Damage)passedTest++;}, "dealt damage to main target");
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
	
	//works but the effect is not saved i think
	private void Test_PoisonedStrike()
	{
		int passed = passedTest;
		
		EventManager eventManager = new EventManager();
		BattleField grid = new BattleField(eventManager);
		Peasant attacker = new Peasant(eventManager);
		grid.PlacePlayer(attacker, grid.GetTile(0,0));
		Peasant target = new Peasant(eventManager);
		grid.PlacePlayer(target, grid.GetTile(0,1));
		int targetInitialHealth = target.Health;
		SingleTarget targetSingle = new SingleTarget(attacker.Tile, target.Tile, 1);
		PoisonedStrike strike = new PoisonedStrike(eventManager, targetSingle);
		eventManager.EmitOnActivateAbility1();
		Test(() => {
			if (target.Health == targetInitialHealth - strike.Damage) passedTest++;
		}, "PoisonedStrike dealt direct damage");
		//Test(()=>{
			//if(target.StatusEffects.Count()== 1)passedTest++;
			//}, "added status effect");
		eventManager.EmitOnStartTurn();
		Test(() => {
			if (target.Health == targetInitialHealth - strike.Damage - strike.poisonDamage) passedTest++;
		}, "PoisonedStrike poison tick 1");
		eventManager.EmitOnStartTurn();
		Test(() => {
			if (target.Health == targetInitialHealth - strike.Damage - (strike.poisonDamage * 2)) passedTest++;
		}, "PoisonedStrike poison tick 2");
		eventManager.EmitOnStartTurn();
		Test(() => {
			if (target.Health == targetInitialHealth - strike.Damage - (strike.poisonDamage * 2)) passedTest++;
		}, "PoisonedStrike poison expired");

		if (passed + 4 == passedTest) passedTest++;
	}

	private void Test_VulnerableEffect()
	{
		int passed = passedTest;
		EventManager eventManager = new EventManager();
		Peasant character = new Peasant(eventManager);
		int baseDamage = 10;
		float multiplier = 0.5f; //this means +60%
		float multiplier2 = 1f; //this means +40%
		float total = multiplier +multiplier2;
		//total 100%
		int duration = 2;
		int duration2 =2;
		int initialHealth = character.Health;
		 //GD.Print($"Initial health {initialHealth}");

		Vulnerable vulnerable = new Vulnerable(multiplier, duration, eventManager, character);
		character.TakeStatusEffect(vulnerable);
		Vulnerable vulnerable2 = new Vulnerable(multiplier2, duration2, eventManager, character);
		character.TakeStatusEffect(vulnerable2);
		//GD.Print($" duration:  {vulnerable.duration}");
		//GD.Print($" duration2:  {vulnerable2.duration}");
		Test(() => {
			if (character.StatusEffects.Count == 2) passedTest++;
		}, "Vulnerable applied");

		// Apply first hit
		int expectedDamage1 = (int)Math.Ceiling(baseDamage * (1+total));
		character.TakeDamage(baseDamage);
		 //GD.Print($"Current health {character.Health}");
		Test(() => {
			if (character.Health == initialHealth - expectedDamage1) passedTest++;
		}, "First damage is multiplied");

		eventManager.EmitOnStartTurn(); // duration = 1
		//GD.Print($"Turn Started");
		//GD.Print($" duration:  {vulnerable.duration}");
		//GD.Print($" duration2:  {vulnerable2.duration}");
		int expectedDamage2 = (int)Math.Ceiling(baseDamage * (1+total));
		character.TakeDamage(baseDamage);
		//GD.Print($"Current health {character.Health}");
		Test(() => {
			if (character.Health == initialHealth - (expectedDamage1 + expectedDamage2)) passedTest++;
		}, "Second damage is multiplied");

		eventManager.EmitOnStartTurn(); // duration = 0, should expire
		//GD.Print($"Turn Started");
		//GD.Print($" duration:  {vulnerable.duration}");
		//GD.Print($" duration2:  {vulnerable2.duration}");
		character.TakeDamage(baseDamage);
		//GD.Print($"Current health {character.Health}");
		Test(() => {
			if (character.Health == initialHealth - (expectedDamage1 + expectedDamage2 + baseDamage)) passedTest++;
		}, "Third damage is NOT multiplied");
		Test(() => {
			if (!character.StatusEffects.Contains(vulnerable)) passedTest++;
		}, "Vulnerable removed after duration");
	}
	
	private void Test_BattleField(){
		EventManager eventManager = new EventManager();
		BattleField battleField = new BattleField(eventManager);
		Peasant peasant = new Peasant(eventManager);
		battleField.PlacePlayer(peasant, battleField.GetTile(1,1));
		PlaceholderEnemy enemy= new PlaceholderEnemy(100,1);
		battleField.PlaceEnemy(enemy, battleField.GetTile(1,2));

		battleField.SelectCharacter(peasant);
		battleField.SelectAbility(peasant.ActiveAbilities[0]);
		battleField.UseSelectedAbility(); // ne trqq da raboti
		battleField.AddTargetable(enemy);
		battleField.AddTargetable(enemy); //ne trqq da raboti
		battleField.UseSelectedAbility();
		//move character dali bachka
		//i moje bi oshte edin repeat na add targetable i useSelectedAbility
		
	}
}
