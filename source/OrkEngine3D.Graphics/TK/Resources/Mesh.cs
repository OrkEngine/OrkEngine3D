using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using OrkEngine3D.Mathematics;


namespace OrkEngine3D.Graphics.TK.Resources
{
    public class Mesh : GLResource
    {
        int VBO;
        int VAO;
        public Vector3[] verticies = new Vector3[0];
        public Vector2[] uv = new Vector2[0];
        public Color4[] colors = new Color4[0];
        public int[] triangles = new int[0];
        public ShaderProgram shader;
        public Mesh(GLResourceManager manager) : base(manager)
        {
            VBO = GL.GenBuffer();
            VAO = GL.GenVertexArray();
        }

        public void UpdateGLData(){

            if(shader == null)
                throw new NullReferenceException("Shader is null, make sure to set shader before updating data!");

            int floatsperv = 3 + 2 + 4; // Vec3 + Vec2 + Col4
            float[] bakedData = new float[verticies.Length * floatsperv];

            for (var i = 0; i < verticies.Length; i++)
            {
                bakedData[i * floatsperv + 0] = verticies[i].X;
                bakedData[i * floatsperv + 1] = verticies[i].Y;
                bakedData[i * floatsperv + 2] = verticies[i].Z;
                bakedData[i * floatsperv + 3] = (uv.Length > 0 ? uv[i].X : 0);
                bakedData[i * floatsperv + 4] = (uv.Length > 0 ? uv[i].Y : 0);
                bakedData[i * floatsperv + 5] = (colors.Length > 0 ? colors[i].Red : 0);
                bakedData[i * floatsperv + 6] = (colors.Length > 0 ? colors[i].Green : 0);
                bakedData[i * floatsperv + 7] = (colors.Length > 0 ? colors[i].Blue : 0);
                bakedData[i * floatsperv + 8] = (colors.Length > 0 ? colors[i].Alpha : 0);
            }

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.BufferData(BufferTarget.ArrayBuffer, bakedData.Length * sizeof(float), bakedData, BufferUsageHint.StaticDraw);

            int vpos = shader.GetAttribLocation("vPos");
            int vuv = shader.GetAttribLocation("vUv");
            int vcol = shader.GetAttribLocation("vCol");

            GL.VertexAttribPointer(vpos, 3, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 0 * sizeof(float));
            GL.EnableVertexAttribArray(vpos);

            GL.VertexAttribPointer(vuv, 2, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vuv);

            GL.VertexAttribPointer(vcol, 4, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(vcol);
        
        }

        public void Render(){
            GL.BindVertexArray(VAO);
            shader.Use();
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticies.Length);
        }

        public override void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VBO);
            GL.DeleteVertexArray(VAO);
        }
    }
}