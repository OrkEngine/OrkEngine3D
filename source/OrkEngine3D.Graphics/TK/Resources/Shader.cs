using OpenTK.Graphics.OpenGL4;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class Shader : GLResource
    {
        public int id;

        /// <summary>
        /// Generate an OpenGL shader
        /// </summary>
        /// <param name="manager">The GLResourceManager</param>
        /// <param name="source">The shader code</param>
        /// <param name="type">The shader type</param>
        /// <returns>The shader object</returns>
        public Shader(GLResourceManager manager, string source, ShaderType type) : base(manager)
        {
            id = GL.CreateShader((OpenTK.Graphics.OpenGL4.ShaderType)type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);
            GL.GetShaderInfoLog(id, out string log);

            if(!string.IsNullOrEmpty(log)){
                throw new ShaderException(type.ToString() + ": " + log);
            }
        }

        public override void Unload()
        {
            GL.DeleteShader(id);
        }
    }
}