namespace OrkEngine3D.Graphics.TK.Resources
{
    using OpenTK.Graphics.OpenGL4;

    public class PostProcessingEffect : GLResource
    {
        public ID Id;
        private readonly ID shaderId;

        private readonly ID vertexShader = Rendering.CreateShader(@"
#version 400 core
const vec4 data[6] = vec4[]
(
    vec4( -1.0,  1.0,  0.0, 1.0 ),
    vec4( -1.0, -1.0,  0.0, 0.0 ),
    vec4(  1.0, -1.0,  1.0, 0.0 ),
    vec4( -1.0,  1.0,  0.0, 1.0 ),
    vec4(  1.0, -1.0,  1.0, 0.0 ),
    vec4(  1.0,  1.0,  1.0, 1.0 )
);

out vec2 uv;

void main()
{
    vec4 vertex = data[gl_VertexID];
    gl_Position = vec4(vertex.xy, 0.0, 1.0);
    uv = vertex.zw;
}
        ", ShaderType.VertexShader);

        private readonly ID buffer;
        
        public PostProcessingEffect(GLResourceManager manager, string shader, int width, int height) : base(manager) {
            ID fragShader = Rendering.CreateShader(shader, ShaderType.FragmentShader);
            shaderId = Rendering.CreateShaderProgram(vertexShader, fragShader);
            buffer = Rendering.CreateRenderBuffer(width, height);
        }
        public override void Unload()
        {
            throw new System.NotImplementedException();
        }

        public void PreRender(){
            Rendering.BindTarget(buffer);
        }

        public void Render()
        {
            Rendering.currentResourceManager.GetResource<ShaderProgram>(shaderId).Use();
            Rendering.currentResourceManager.GetResource<RenderBuffer>(buffer).target.Use(0);
            Rendering.currentResourceManager.GetResource<ShaderProgram>(shaderId).Uniform1("frame", 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

    }
}