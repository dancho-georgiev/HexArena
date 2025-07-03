shader_type canvas_item;

uniform float epsilon = 0.05;
uniform float frequency : hint_range(0.0, 100.0, 0.1);
uniform float amplitude : hint_range(0.0, 5.0, 0.1);
uniform float rotation : hint_range(0.0,6.28,0.01);
uniform vec2 center =vec2(0.5,0.5);

float wave(vec2 uv){
	return (sin(uv.x*frequency-0.5)*amplitude + uv.y-0.5);
}
vec2 offset(vec2 basisX, vec2 basisY){
	return center-vec2(center.x*basisX.x + center.y * basisY.x, center.x*basisX.y +center.y*basisY.y);
}
vec2 rotate(vec2 uv){
	float _cos =  cos(rotation);
	float _sin = sin(rotation);
	vec2 basisX = vec2(_cos, _sin);
	vec2 basisY =  vec2(-_sin, _cos);
	return vec2(uv.x*basisX.x + uv.y * basisY.x, uv.x*basisX.y + uv.y*basisY.y) + offset(basisX, basisY);
}

vec2 rotate(vec2 uv, float _rotation){
	float _cos =  cos(_rotation);
	float _sin = sin(_rotation);
	vec2 basisX = vec2(_cos, _sin);
	vec2 basisY =  vec2(-_sin, _cos);
	return vec2(uv.x*basisX.x + uv.y * basisY.x, uv.x*basisX.y + uv.y*basisY.y) + offset(basisX, basisY);
}

bool onLine(vec2 uv){
	vec2 uv1 = rotate(uv);
	vec2 uv2 = rotate(uv1, 2.0*PI/3.0);
	return wave(uv1) <= 0.0+epsilon && wave(uv1) >= 0.0-epsilon ||
	 wave(uv2) <= 0.0+epsilon && wave(uv2) >= 0.0-epsilon;
}
void fragment() {
	if(onLine(UV)) COLOR = vec4(0.0,0.73,0.46,0.7);
}
