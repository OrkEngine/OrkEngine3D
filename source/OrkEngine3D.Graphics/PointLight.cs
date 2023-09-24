using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics
{
    public class PointLight : BaseLight
    {
        //change position to transform eventually
        public PointLight(Vector3 position)
            : base(Camera, LightShader)
        {
            if (position.Length == 1)
            {
                // Point light
                LightShader.SetVector3($"pointLights[0].position", position);
                LightShader.SetVector3($"pointLights[0].ambient", Ambient);
                LightShader.SetVector3($"pointLights[0].diffuse", Diffuse);
                LightShader.SetVector3($"pointLights[0].specular", Specular);
                LightShader.SetFloat($"pointLights[0].constant", Constant);
                LightShader.SetFloat($"pointLights[0].linear", Linear);
                LightShader.SetFloat($"pointLights[0].quadratic", Quadratic);
            }
        }

        public PointLight(Vector3[] positions)
            : base(Camera, LightShader)
        {
            if (positions.Length > 1)
            {
                // Point lights
                for (int i = 0; i < positions.Length; i++)
                {
                    LightShader.SetVector3($"pointLights[{i}].position", positions[i]);
                    LightShader.SetVector3($"pointLights[{i}].ambient", Ambient);
                    LightShader.SetVector3($"pointLights[{i}].diffuse", Diffuse);
                    LightShader.SetVector3($"pointLights[{i}].specular", Specular);
                    LightShader.SetFloat($"pointLights[{i}].constant", Constant);
                    LightShader.SetFloat($"pointLights[{i}].linear", Linear);
                    LightShader.SetFloat($"pointLights[{i}].quadratic", Quadratic);
                }
            }
        }

        //public LightSettings LightSettings;

        public Vector3 Ambient { get; set; } = new Vector3(0.05f, 0.05f, 0.05f);

        public Vector3 Diffuse { get; set; } = new Vector3(0.8f, 0.8f, 0.8f);

        public Vector3 Specular { get; set; } = new Vector3(1.0f, 1.0f, 1.0f);

        public float Constant { get; set; } = 1.0f;

        public float Linear { get; set; } = 0.09f;

        public float Quadratic { get; set; } = 0.032f;
    }
}
