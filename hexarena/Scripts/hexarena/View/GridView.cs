using Godot;
using System;
using GameLogic;

namespace View{
	
	public partial class GridView : Node
	{
		public BattleField battleField;
		[Export]
		public int Width {get; set;}
		[Export]
		public int Length {get; set;}
		[Export]
		public PackedScene Hexagon {get; set;}
		
		public override void _Ready(){
			EventManager eventManager = new EventManager();
			battleField = new BattleField(eventManager, Width, Length);
			for(int i = 0; i <= Width; i++){
				for(int j = 0; j <= Length; j++){
					Hexagon inst = Hexagon.Instantiate<Hexagon>();
					inst.Position = new Vector2(i *  inst.Size + (j % 2 == 1 ? inst.Size/2 : 0), j * inst.Size);
					AddChild(inst);
				}
			}
		} 
	}
	
}
