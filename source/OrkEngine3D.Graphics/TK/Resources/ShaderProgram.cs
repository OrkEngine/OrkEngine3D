using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public class ShaderProgram : GLResource
    {
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
                throw new ProgramException(log);
            }
            
            for (var i = 0; i < shaders.Length; i++)
            {
                GL.DetachShader(id, shaders[i].id);
            }

            GL.GetProgram(id, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            for (int i = 0; i < uniformCount; i++)
            {
                string name = GL.GetActiveUniform(id, i, out int size, out ActiveUniformType type);

                uniforms.Add(name, i);
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


        // UNIFORM SETTING
        public void Uniform1(string name, int value){
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, float value){
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, double value){
            GL.Uniform1(GetUniformLocation(name), value);
        }
        public void Uniform1(string name, byte value){
            GL.Uniform1(GetUniformLocation(name), value);
        }


        public void Uniform2(string name, int x, int y){
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, float x, float y){
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, double x, double y){
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, byte x, byte y){
            GL.Uniform2(GetUniformLocation(name), x, y);
        }
        public void Uniform2(string name, Vector2 value){
            GL.Uniform2(GetUniformLocation(name), value.X, value.Y);
        }

        public void Uniform3(string name, int x, int y, int z){
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, float x, float y, float z){
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, double x, double y, double z){
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, byte x, byte y, byte z){
            GL.Uniform3(GetUniformLocation(name), x, y, z);
        }
        public void Uniform3(string name, Vector3 value){
            GL.Uniform3(GetUniformLocation(name), value.X, value.Y, value.Z);
        }
        public void Uniform3(string name, Color3 value){
            GL.Uniform3(GetUniformLocation(name), value.Red, value.Green, value.Blue);
        }

        public void Uniform4(string name, int x, int y, int z, int w){
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, float x, float y, float z, float w){
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, double x, double y, double z, double w){
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, byte x, byte y, byte z, byte w){
            GL.Uniform4(GetUniformLocation(name), x, y, z, w);
        }
        public void Uniform4(string name, Vector4 value){
            GL.Uniform4(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
        }
        public void Uniform4(string name, Color4 value){
            GL.Uniform4(GetUniformLocation(name), value.Red, value.Green, value.Blue, value.Alpha);
        }
        public void UniformMatrix(string name, Matrix value){
            GL.UniformMatrix4(GetUniformLocation(name), 1, false, value.ToArray());
        }
    }
}