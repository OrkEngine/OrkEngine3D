#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec2 fUV;
in vec3 Pos;

uniform sampler2D mat_texture0;
uniform sampler2D mat_texture1;
uniform vec3 camera_pos;

struct Light{
    float strength;
    vec3 color;
    vec3 position;
};

uniform Light ambient;
uniform Light light;

void main()
{   
    vec3 objectColor = texture(mat_texture0, fUV).rgb;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - Pos);

    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * light.color;

    vec3 ambient = ambient.strength * ambient.color;

    float specularStrength = 0.5;
    vec3 viewDir = normalize(camera_pos - Pos);
    vec3 reflectDir = reflect(-lightDir, norm);

    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 256);
    vec3 specular = specularStrength * spec * light.color;

    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}