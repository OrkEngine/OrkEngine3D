using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class ShaderProgram : GLResource
    {
        private static int lastBound = -1;
        public int id;

        /// <summary>
        /// The active uniforms in the shader
        /// </summary>
        /// <typeparam name="string">Uniform name</typeparam>
        /// <typeparam name="int">Uniform ID</typeparam>
        public Dictionary<string, int> uniforms = new Dictionary<string, int>();

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
                Logger.Get("ProgramLinker", "Graphics").Log(LogMessageType.ERROR, log);
            }
            
            for (var i = 0; i < shaders.Length; i++)
            {
                GL.DetachShader(id, shaders[i].id);
            }

            GL.GetProgram(id, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            for (int i = 0; i < uniformCount; i++)
            {
                string name = GL.GetActiveUniform(id, i, out int size, out ActiveUniformType type);

                uniforms.Add(name, GL.GetUniformLocation(id, name));
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
            return (uniforms.ContainsKey(name) ? uniforms[name] : -1);
        }

        /// <summary>
        /// Use the shader
        /// </summary>
        public void Use(){
            lastBound = id;
            GL.UseProgram(id);
        }

        /// <summary>
        /// Dispatch compute shader if the ShaderProgram contains one
        /// </summary>
        public void DispatchCompute(int width, int height, int depth){
            Use();
            GL.DispatchCompute(width, height, depth);
            GL.Finish();
        }

        /// <summary>
        /// Unload the ShaderProgram
        /// </summary>
        public override void Unload()
        {
            GL.DeleteProgram(id);
        }

        #region Uniforms
        // UNIFORM SETTING
        public void Uniform1(string name, int value){
            if(lastBound != id)
                Use();
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, float value){
            if(lastBound != id)
                Use();
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, double value){
            if(lastBound != id)
                Use();
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, byte value){
            if(lastBound != id)
                Use();
            GL.Uniform1(GetUniformLocation(name), value);
        }


        public void Uniform2(string name, int x, int y){
            if(lastBound != id)
                Use();
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, float x, float y){
            if(lastBound != id)
                Use();
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, double x, double y){
            if(lastBound != id)
                Use();
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, byte x, byte y){
            if(lastBound != id)
                Use();
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, Vector2 value){
            if(lastBound != id)
                Use();
            GL.Uniform2(GetUniformLocation(name), value.X, value.Y);
        }

        public void Uniform3(string name, int x, int y, int z){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, float x, float y, float z){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, double x, double y, double z){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, byte x, byte y, byte z){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, Vector3 value){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), value.X, value.Y, value.Z);
        }
        public void Uniform3(string name, Color3 value){
            if(lastBound != id)
                Use();
            GL.Uniform3(GetUniformLocation(name), value.Red, value.Green, value.Blue);
        }

        public void Uniform4(string name, int x, int y, int z, int w){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, float x, float y, float z, float w){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, double x, double y, double z, double w){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, byte x, byte y, byte z, byte w){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, Vector4 value){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
        }
        public void Uniform4(string name, Color4 value){
            if(lastBound != id)
                Use();
            GL.Uniform4(GetUniformLocation(name), value.Red, value.Green, value.Blue, value.Alpha);
        }
        public void UniformMatrix(string name, Matrix value){
            UniformMatrix(name, false, value);
        }
        public void UniformMatrix(string name, bool transpose, Matrix value){
            if(lastBound != id)
                Use();
            GL.UniformMatrix4(GetUniformLocation(name), 1, transpose, value.ToArray());
        }
        #endregion
    }
}