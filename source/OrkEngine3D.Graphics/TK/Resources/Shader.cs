using OpenTK.Graphics.OpenGL4;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class Shader : GLResource
    {
        public int id;
        public Shader(GLResourceManager manager, string source, ShaderType type) : base(manager)
        {
            id = GL.CreateShader((OpenTK.Graphics.OpenGL4.ShaderType)type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);
            GL.GetShaderInfoLog(id, out string log);

            if(!string.IsNullOrEmpty(log)){
                throw new ShaderException(log);
            }
        }

        public override void Unload()
        {
            GL.DeleteShader(id);
        }
    }
}