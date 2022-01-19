using OpenTK.Graphics.OpenGL4;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class ShaderProgram : GLResource
    {
        public int id;

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


        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(id, name);
        }

        public void Use(){
            GL.UseProgram(id);
        }

        public override void Unload()
        {
            GL.DeleteProgram(id);
        }
    }
}