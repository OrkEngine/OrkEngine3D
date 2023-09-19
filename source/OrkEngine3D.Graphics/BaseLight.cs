using OrkEngine3D.Graphics.TK.Resources;

namespace OrkEngine3D.Graphics
{
    public class BaseLight
    {
        public static LightSettings LightSettings { get; set; } = new LightSettings();

        public static Camera Camera { get; set; }

        public static Shader LightShader { get; set; } = new Shader("resources/shaders/shader.vert", "resources/shaders/lighting.frag");

        public BaseLight(Camera camera, Shader lightShader)
        {
            lightShader.Use();

            Camera = camera;
            LightShader = lightShader;

            lightShader.SetMatrix4("view", camera.GetViewMatrix());
            lightShader.SetMatrix4("projection", camera.GetProjectionMatrix());

            lightShader.SetVector3("viewPos", camera.Position);

            lightShader.SetInt("material.diffuse", LightSettings.Diffuse);
            lightShader.SetInt("material.specular", LightSettings.Specular);
            lightShader.SetVector3("material.specular", LightSettings.VecSpecular);
            lightShader.SetFloat("material.shininess", LightSettings.Shininess);
        }
    }
}
