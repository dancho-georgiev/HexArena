shader_type canvas_item;

uniform vec3 rgb;
uniform float epsilon;
uniform sampler2D Texture;

void fragment() {
	vec2 pixel_size = 1.0 / vec2(textureSize(TEXTURE, 0));
    for(int y = -1; y <= 1; y++)
	{
	    for(int x = -1; x <= 1; x++)
	    {
	        vec2 pixel_off = vec2(float(x) * epsilon, float(y) * epsilon);
	        
	        if(texture(Texture, UV+pixel_off).a>0.0 && texture(Texture,UV).a==0.0)
	        {
	            COLOR = vec4(rgb, 1.0);
	            break;
	        }
		}
    }
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
