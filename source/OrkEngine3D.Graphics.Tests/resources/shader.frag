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

uniform Material materials[64];

uniform sampler2D material_textures[64 * 16];

Material GetMaterial(int m){
    Material material = materials[m];
    return material;
}

sampler2D GetTexture(int m, int t){
    return material_textures[m * 16 + t];
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
    vec3 lightDir = normalize(l.position - Pos);

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

    Light ambientLight = ambient;
    Light currentLight = light;

    vec3 hi = ambient.color;


    vec3 objectColor = vec3(1, 1, 1);//texture2D(material1_texture0, fUV).rgb;

    vec3 amb = CalculateAmbientLightning(ambientLight, material);
    vec3 dif = CalculateDiffuseLightning(currentLight, material);
    vec3 spec = CalculateSpecularLightning(currentLight, material, Normal);
    
    // specular
        
    vec3 result = CombineLightning(amb, dif, spec, objectColor);
    FragColor = vec4(result, 1.0);
}