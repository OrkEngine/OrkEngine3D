using OpenTK.Mathematics;

namespace OrkEngine3D.Graphics
{
    public class LightSettings
    {
        public int Diffuse { get; set; } = 0;

        public int Specular { get; set; } = 1;

        public Vector3 VecSpecular { get; set; } = new Vector3(0.5f, 0.5f, 0.5f);

        public float Shininess { get; set; } = 32.0f;
    }
}
