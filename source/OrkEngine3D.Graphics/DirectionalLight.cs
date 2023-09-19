using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics
{
    public class DirectionalLight : BaseLight
    {
        //change position to transform eventually

        public DirectionalLight(Vector3 direction)
            : base(Camera, LightShader)
        {
            Direction = direction;

            LightShader.SetVector3("dirLight.direction", Direction);
            LightShader.SetVector3("dirLight.ambient", Ambient);
            LightShader.SetVector3("dirLight.diffuse", Diffuse);
            LightShader.SetVector3("dirLight.specular", Specular);
        }

        //Default value
        public Vector3 Direction { get; set; } = new Vector3(-0.2f, -1.0f, -0.3f);

        public Vector3 Ambient { get; set; } = new Vector3(0.05f, 0.05f, 0.05f);

        public Vector3 Diffuse { get; set; } = new Vector3(0.4f, 0.4f, 0.4f);

        public Vector3 Specular { get; set; } = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
