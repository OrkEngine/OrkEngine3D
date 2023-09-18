using OpenTK.Graphics.OpenGL;
using OrkEngine3D.Graphics.TK.Resources;

namespace OrkEngine3D.Graphics.TK;

/* TODO: Needs to be tested */
/*
internal class Skybox
{
    private ShaderProgram skyboxShader;
    private Texture skyboxTexture;
    private int skyboxVAO;

    public Skybox(GLResourceManager manager, string skyboxImagePath)
    {
        skyboxShader = new ShaderProgram(manager, new Shader(manager, vertexShaderSource, Resources.ShaderType.VertexShader), new Shader(manager, fragmentShaderSource, Resources.ShaderType.FragmentShader));
        skyboxTexture = new Texture(manager, Texture.GetTextureDataFromFile(skyboxImagePath));
        skyboxVAO = GL.GenVertexArray();
        GL.BindVertexArray(skyboxVAO);
    }

    public void Render()
    {
        GL.DepthMask(false);
        skyboxShader.Use();
        GL.BindVertexArray(skyboxVAO);
        skyboxTexture.Use(0);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        GL.DepthMask(true);
    }

    private const string vertexShaderSource = "#version 330 core\nlayout (location = 0) in vec3 aPos;\nout vec3 TexCoords;\nuniform mat4 projection;\nuniform mat4 view;\nvoid main()\n{\n    TexCoords = aPos;\n    vec4 pos = projection * view * vec4(aPos, 1.0);\n    gl_Position = pos.xyww;\n}\n";

    private const string fragmentShaderSource = "#version 330 core\nout vec4 FragColor;\nin vec3 TexCoords;\nuniform sampler2D skybox;\nvoid main()\n{\n    FragColor = texture(skybox, TexCoords);\n}\n";
}
*/