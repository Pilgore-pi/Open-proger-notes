pipeline
https://opentk.net/learn/chapter1/2-hello-triangle.html?tabs=onload-opentk4%2Conrender-opentk4%2Cresize-opentk4

Textures
https://moderngl.readthedocs.io/en/latest/reference/texture.html

moderngl vertex array
https://moderngl.readthedocs.io/en/4.1.1/VertexArrays.html

```glsl
#ifdef GL_FRAGMENT_PRECISION_HIGH 
precision highp float; 
#else 
precision mediump float; 
#endif 
 
vec2 rotate2D(vec2 uv, float a) { 
  float s = sin(a); 
  float c = cos(a); 
  return mat2(c, -s, s, c) * uv; 
} 
 
///////////////////////// 
///  Curve Functions  /// 
///////////////////////// 
 
vec2 Ellipse(float t, float width, float height){ 
 float x = height * cos(t); 
 float y = width * sin(t); 
 return vec2(x, y); 
} 
 
vec2 Cardioid(float t, float r){ 
 float x = 2. * r * cos(t) - r * cos(2. * t); 
 float y = 2. * r * sin(t) - r * sin(2. * t); 
 return vec2(x, y); 
} 
 
vec2 SteinerCurve(float t, float r){ 
 float x = 2. * r * cos(t) - r * cos(2. * t); 
 float y = 2. * r * sin(t) + r * sin(2. * t); 
 return vec2(x, y); 
} 
 
vec2 Astroid(float t, float R){ // it needs 80 POINTS 
 float x = R * cos(t/4.) * cos(t/4.) * cos(t/4.); 
 float y = R * sin(t/4.) * sin(t/4.) * sin(t/4.); 
 return vec2(x, y); 
} 
 
vec2 Epicycloid(float t, float a, float b){ 
 float x = (a+b)*cos(t) - b*cos((a/b+1.)*t); 
 float y = (a+b)*sin(t) - b*sin((a/b+1.)*t); 
 return vec2(x, y); 
} 
///////////////////////// 
///--Curve Functions--/// 
///////////////////////// 
 
 
uniform vec2 resolution; 
uniform float time; 
 
void main(void) { 
 vec2 uv = gl_FragCoord.xy / resolution.xy; 
 vec3 col = vec3(0.0); 
 
 uv = rotate2D(uv, 3.14 / 2.0)*3.0 +vec2(-1.5, 1.5); 
 uv.y /= 2.0; 
 
 const float r = 0.17; 
 const float POINTS = 15.0; // 60 default 
 for (float i=0.0; i < POINTS; i++) { //sin(time) * 0.5 
  float factor = (sin(time) * 0.5 + 0.5) + 0.3; 
  i += factor; 
 
  float t = i / 1.5; 
 
  vec2 point = SteinerCurve(t, 0.17); //clamp(cos(.0005*time)*.1, 0, 1) 
  float dx = point.x; 
  float dy = point.y; 
 
  //cardioid 
//  float a = i / 3; //originally was 2r cos(a) - r cos(2a) 
//  float dx = 2 * r * cos(a) - r * cos((2+.001*a) * a); 
//  float dy = 2 * r * sin(a) - r * sin((2+.001*a) * a); 
 
  col += 0.013 * factor / length(uv-vec2(dx + 0.1, dy) - 0.0); //was 0.01 * hash12(i) 
 } 
 // i tried to get rid of extra light 
 //vec3 changingColor = vec3(abs(sin(0.5*time)), 1 - abs(sin(.8*time)), abs(sin(.8*time))); // was vec3(0.2, 0.8, 0.9) * time 
 //col *= vec3(abs(sin(time)*0.5), 0.2, 1.0) * 1.3; //blue 
 col *= vec3(0.2, 0.8, 0.5);
 //col *= clamp(sin(changingColor) *  0.15 + 0.25, .25, -1); 
 
 gl_FragColor = vec4(col, 1.0); 
}
```

#Shaders