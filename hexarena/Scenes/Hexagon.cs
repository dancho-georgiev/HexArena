using Godot;
using System;


public partial class Hexagon : Node2D
{
	[Export]
	public Polygon2D polygon2D;
	[Export]
	public Polygon2D innerPolygon2D;
	[Export]
	public CollisionPolygon2D collisionPolygon2D;
	[Export]
	public Area2D area2D;
	
	[Export]
	public float Size {get; set;} = 30;
	public static Vector2 pointy_hex_corner(Vector2 center, float size, int i){
		float angle_deg = 60 * i - 30;
		float angle_rad = Mathf.Pi / 180 * angle_deg;
		return new Vector2(center.X + size * Mathf.Cos(angle_rad), center.Y + size * Mathf.Sin(angle_rad));
	}
	
	public override void _Ready(){
		polygon2D = GetNode<Polygon2D>("polygon");
		innerPolygon2D =  GetNode<Polygon2D>("innerPolygon");
		area2D = GetNode<Area2D>("Area2D");
		collisionPolygon2D = area2D.GetNode<CollisionPolygon2D>("CollisionPolygon2D");
		
		//Vector2[] polygon = new Vector2[6];
		//Vector2[] innerPolygon = new Vector2[6];
		//for(int i = 0; i < 6; i++){
			//polygon[i] = pointy_hex_corner(new Vector2(Position.X,Position.Y),Size,i);
			//innerPolygon[i] = pointy_hex_corner(new Vector2(Position.X,Position.Y),Size-5,i);
		//}
//
		//polygon2D.SetPolygon(polygon);
		//innerPolygon2D.SetPolygon(innerPolygon);
		innerPolygon2D.ZIndex = 1;
		//collisionPolygon2D.SetPolygon(polygon);
		//area2D.AddChild(collisionPolygon2D);
		area2D.InputPickable = true;
		//AddChild(polygon2D);
		//AddChild(innerPolygon2D);
		//AddChild(area2D);
	} 
			

	
}
