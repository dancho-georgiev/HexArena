using Godot;
using System;


public struct PointDouble{
	public double x;
	public double y;
	public PointDouble(){
		x=0;
		y=0;
	}
	public PointDouble(double x, double y){
		this.x = x;
		this.y = y;
	}
	public static bool operator==(PointDouble p1, PointDouble p2){
				return p1.x==p2.x && p1.y==p2.y;
			}
	public static bool operator!=(PointDouble p1, PointDouble p2){
		return p1.x!=p2.x || p1.y!=p2.y;
	}
}

public partial class Hexagon : Node2D
{
	public Polygon2D polygon2D = new Polygon2D();
	public CollisionPolygon2D collisionPolygon2D = new CollisionPolygon2D();
	public Area2D area2D = new Area2D();
	[Export]
	public float Size {get; set;} = 30;
	public static Vector2 pointy_hex_corner(Vector2 center, float size, int i){
		float angle_deg = 60 * i - 30;
		float angle_rad = Mathf.Pi / 180 * angle_deg;
		return new Vector2(center.X + size * Mathf.Cos(angle_rad), center.Y + size * Mathf.Sin(angle_rad));
	}
	
	public override void _Ready(){
		Vector2[] polygon = new Vector2[6];
		for(int i = 0; i < 6; i++){
			polygon[i] = pointy_hex_corner(new Vector2(Position.X,Position.Y),Size,i);
		}
		polygon2D.SetPolygon(polygon);
		collisionPolygon2D.SetPolygon(polygon);
		area2D.AddChild(collisionPolygon2D);
		area2D.InputPickable = true;
		AddChild(polygon2D);
		AddChild(area2D);
	} 
	
}
