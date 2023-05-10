#version 400 core
out vec4 FragColor;

in vec3 Normal;
in vec2 fUV;
in vec3 Pos;
in vec4 lightFragPos;
flat in int mat;

uniform vec3 camera_pos;

struct Light{
    float strength;
    vec3 color;
    vec3 position;
};


uniform Light ambient;
uniform Light lights[256];
uniform int lights_count;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};

uniform Material materials[8];

uniform sampler2D material_textures[8 * 4];

Material GetMaterial(int m){
    Material material = materials[m];
    return material;
}

int GetTextureID(int m, int t){
    return m * 16 + t;
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

uniform sampler2D material_shadowMap;

float ShadowCalculation(vec4 fragPosLightSpace)
{
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    projCoords = projCoords * 0.5 + 0.5;
    float closestDepth = texture(material_shadowMap, projCoords.xy).r;
    float currentDepth = projCoords.z;
    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0;
    return shadow;
}

void main()
{
    /////FragColor = vec4(1.0);
    Material material = GetMaterial(mat);

    Light ambientLight = ambient;
    Light currentLight = lights[0];


    vec3 col = texture2D(material_textures[0], fUV).rgb;

    vec3 amb = vec3(0, 0, 0);
    vec3 dif = vec3(0, 0, 0);
    vec3 spec = vec3(0, 0, 0);

    amb += CalculateAmbientLightning(ambientLight, material);

    for(int i = 0; i < lights_count; i++){
        dif += CalculateDiffuseLightning(lights[i], material);
        spec += CalculateSpecularLightning(lights[i], material, Normal);
    }

    float shadow = ShadowCalculation(lightFragPos);
    vec3 lighting = (amb + (1.0 - shadow) * (dif + spec)) * col;

    ////vec3 result = CombineLightning(amb, dif, spec, objectColor);
    FragColor = vec4(lighting, 1.0);
    
}