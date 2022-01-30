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

Material GetMaterial(int m){
    Material material = Material(vec3(0), vec3(0), vec3(0), 0);

    if(m == 0){
        material.ambient = material0.ambient;
        material.diffuse = material0.diffuse;
        material.specular = material0.specular;
        material.shininess = material0.shininess;
    }
    if(m == 1){
        material.ambient = material1.ambient;
        material.diffuse = material1.diffuse;
        material.specular = material1.specular;
        material.shininess = material1.shininess;
    }
    if(m == 2){
        material.ambient = material2.ambient;
        material.diffuse = material2.diffuse;
        material.specular = material2.specular;
        material.shininess = material2.shininess;
    }

    return material;
}

vec3 CalculateAmbientLightning(Light l, Material material){
    return (l.color * l.strength) * material.ambient;
}

vec3 CalculateDiffuseLightning(Light l, Material material){
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(l.position - Pos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = (l.color * l.strength) * (diff * material.diffuse);
    return diffuse;
}

vec3 CalculateSpecularLightning(Light l, Material material, vec3 norm){
    vec3 lightDir = normalize(light.position - Pos);

    vec3 viewDir = normalize(camera_pos - Pos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = (l.color * l.strength) * (spec * material.specular);  
    return specular;
}

vec3 CombineLightning(vec3 ambient, vec3 diffuse, vec3 specular, vec3 objcolor){    
    return (ambient + diffuse + specular) * objcolor;
}

void main()
{
    Material material = GetMaterial(mat);

    Light currentLight = Light(light.strength, light.color, light.position);
    Light ambientLight = Light(ambient.strength, ambient.color, ambient.position);


    vec3 objectColor = vec3(1, 1, 1);//texture2D(material1_texture0, fUV).rgb;

    vec3 amb = CalculateAmbientLightning(ambientLight, material);
    vec3 dif = CalculateDiffuseLightning(currentLight, material);
    vec3 spec = CalculateSpecularLightning(currentLight, material, Normal);
    
    // specular
        
    vec3 result = CombineLightning(amb, dif, spec, objectColor);
    FragColor = vec4(result, 1.0);
}