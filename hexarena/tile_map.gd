extends Node2D

@export var hex_scene: PackedScene      #tva shte go navurja kum hexa
@export var grid_height:= 5  #rows
@export var grid_width:= 8  #cols
@export var hex_size := 32.0

# tova e dictionary ot vsichki tileove pod formata <Vector2i, Node> kluchut e Vector2i 
var tiles = {} 

func _ready():
	generate_rect_tile_map()
	$Polygon2D.polygon = get_hex_points(hex_size)

func get_hex_points(size: float) -> PackedVector2Array:
	var points = []
	for i in 6:
		var angle = deg_to_rad(60 * i - 30)  # -30° for pointy-top
		points.append(Vector2(cos(angle), sin(angle)) * size)
	return PackedVector2Array(points)

#In the  orientation, the horizontal distance between adjacent
#hexagon centers is horiz = width == sqrt(3) * size == 2 * inradius. The
#vertical distance is vert = 3/4 * height == 3/2 * size.
func next_hex_center(q: int, r: int) -> Vector2:
	var x  = 1.5 * q * hex_size
	var y = sqrt(3.00) * (r * q * 0.5) * hex_size
	return Vector2(x,y) # returnva posokata i distanciqta kum sledvashtiq centur



#suzdavam tileove i gi dobavqm kum dictionaryto tiles
func generate_rect_tile_map():
	for q in range(0, grid_width):
		for r in range(0, grid_height):
			var tile = hex_scene.instantiate()
			tile.q = q
			tile.r = r
			tile.position = next_hex_center(q,r)
			add_child(tile)
			tiles[Vector2i(q,r)] = tile
