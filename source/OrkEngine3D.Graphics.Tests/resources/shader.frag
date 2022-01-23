#version 400 core
out vec4 FragColor;

in vec3 Normal;
in vec2 fUV;
in vec3 Pos;
flat in int mat;

uniform vec3 camera_pos;

struct Light{
    float strength;
    vec3 color;
    vec3 position;
};


uniform Light ambient;
uniform Light light;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};

uniform Material material0;
uniform Material material1;
uniform Material material2;

uniform sampler2D material1_texture0;

void main()
{
    Material material = Material(vec3(0), vec3(0), vec3(0), 0);
    vec3 objectColor = vec3(0, 0, 0);
    if(mat == 1){
        material.ambient = material1.ambient;
        material.diffuse = material1.diffuse;
        material.specular = material1.specular;
        material.shininess = material1.shininess;
        objectColor = material1.ambient;
    }
    if(mat == 2){
        material.ambient = material2.ambient;
        material.diffuse = material2.diffuse;
        material.specular = material2.specular;
        material.shininess = material2.shininess;
        objectColor = material2.ambient;
    }

    vec3 ambient = (ambient.color * ambient.strength) * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - Pos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = (light.color * light.strength) * (diff * material.diffuse);
    
    // specular
    vec3 viewDir = normalize(camera_pos - Pos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = (light.color * light.strength) * (spec * material.specular);  
        
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(1.0, 1.0, 1.0, 0.0);
}