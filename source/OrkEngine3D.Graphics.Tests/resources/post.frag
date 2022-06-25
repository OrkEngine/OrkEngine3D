#version 400

uniform sampler2D frame;

in vec2 uv;

void main(){
    vec4 col = texture2D(frame, uv);
    col.a = 1;
    gl_FragColor = col;
}