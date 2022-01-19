using OpenTK.Graphics.OpenGL4;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class ShaderProgram : GLResource
    {
        public int id;

        /// <summary>
        /// Create and compile the shader
        /// </summary>
        /// <param name="manager">The GLResourceManager</param>
        /// <param name="shaders">The shader objects</param>
        /// <returns>The shader program</returns>
        public ShaderProgram(GLResourceManager manager, params Shader[] shaders) : base(manager)
        {
            id = GL.CreateProgram();
            for (var i = 0; i < shaders.Length; i++)
            {
                GL.AttachShader(id, shaders[i].id);
            }
            GL.LinkProgram(id);
            GL.GetProgramInfoLog(id, out string log);

            if(!string.IsNullOrEmpty(log)){
                throw new ProgramException(log);
            }
            
            for (var i = 0; i < shaders.Length; i++)
            {
                GL.DetachShader(id, shaders[i].id);
            }

        }

        /// <summary>
        /// Get the location of a vertex attribute
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <returns>The OpenGL location</returns>
        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(id, name);
        }

        /// <summary>
        /// Get the location of a uniform
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <returns>The OpenGL location</returns>

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(id, name);
        }

        /// <summary>
        /// Use the shader
        /// </summary>
        public void Use(){
            GL.UseProgram(id);
        }

        /// <summary>
        /// Dispatch the compute shader
        /// </summary>
        public void DispatchCompute(int width, int height, int depth){
            Use();
            GL.DispatchCompute(width, height, depth);
            GL.Finish();
        }

        public override void Unload()
        {
            GL.DeleteProgram(id);
        }
    }
}