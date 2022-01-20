#version 400 core
in vec3 vert_position;
in vec4 vert_color;
in vec2 vert_uv;
in vec3 vert_normal;
in float vert_material;

out vec2 fUV;
out vec3 Normal;
out vec3 Pos;
flat out int mat;

uniform mat4 matx_model;
uniform mat4 matx_view;

void main()
{
    gl_Position = matx_view * matx_model * vec4(vert_position, 1.0);
    Normal = vert_normal * mat3(inverse(matx_model));
    fUV = vert_uv;
    Pos = vec3(matx_model * vec4(vert_position, 1.0));
    mat = int(floor(vert_material));
}