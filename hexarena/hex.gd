extends Node2D

# coords
var q: int     #columms
var r: int     #rows

signal tile_clicked(q, r)

func _input_event(_viewport, event, _shape_idx):
	if event is InputEventMouseButton and event.pressed:
		emit_signal("tile_clicked",q,r)
	
