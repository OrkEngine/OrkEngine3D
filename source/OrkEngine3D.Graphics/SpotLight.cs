using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics
{
    public class SpotLight : BaseLight
    {
        //change position to transform eventually
        public SpotLight(Vector3 position, Vector3 direction)
            : base(Camera, LightShader)
        {

            // Point light
            LightShader.SetVector3("spotLight.position", position); //Camera.Position
            LightShader.SetVector3("spotLight.direction", direction); //Camera.Front
            LightShader.SetVector3("spotLight.ambient", Ambient);
            LightShader.SetVector3("spotLight.diffuse", Diffuse);
            LightShader.SetVector3("spotLight.specular", Specular);
            LightShader.SetFloat("spotLight.constant", Constant);
            LightShader.SetFloat("spotLight.linear", Linear);
            LightShader.SetFloat("spotLight.quadratic", Quadratic);
            LightShader.SetFloat("spotLight.cutOff", CutOff);
            LightShader.SetFloat("spotLight.outerCutOff", OuterCutOff);

        }

        //public LightSettings LightSettings;

        public Vector3 Ambient { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);

        public Vector3 Diffuse { get; set; } = new Vector3(1.0f, 1.0f, 1.0f);

        public Vector3 Specular { get; set; } = new Vector3(1.0f, 1.0f, 1.0f);

        public float Constant { get; set; } = 1.0f;

        public float Linear { get; set; } = 0.09f;

        public float Quadratic { get; set; } = 0.032f;

        public float CutOff { get; set; } = Mathematics.MathF.Cos(MathHelper.DegreesToRadians(12.5f));

        public float OuterCutOff { get; set; } = Mathematics.MathF.Cos(MathHelper.DegreesToRadians(17.5f));
    }
}
