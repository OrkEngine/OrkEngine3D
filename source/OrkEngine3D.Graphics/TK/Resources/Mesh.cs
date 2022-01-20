using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using OrkEngine3D.Mathematics;
using OrkEngine3D.Components.Core;

namespace OrkEngine3D.Graphics.TK.Resources
{
    /// <summary>
    /// A mesh.
    /// </summary>
    public class Mesh : GLResource
    {
        int VBO;
        int VAO;
        int EBO;
        int m_view;
        int m_projection;
        int m_model;

        /// <summary>
        /// The verticies of the mesh
        /// </summary>
        public Vector3[] verticies = new Vector3[0];

        /// <summary>
        /// The UVs of the mesh
        /// </summary>
        public Vector2[] uv = new Vector2[0];

        /// <summary>
        /// The vertex colours of the mesh
        /// </summary>
        public Color4[] colors = new Color4[0];

        /// <summary>
        /// The triangles of the mesh
        /// </summary>
        public uint[] triangles = new uint[0];

        /// <summary>
        /// What shader should the mesh use?
        /// </summary>
        public ShaderProgram shader;

        /// <summary>
        /// The textures passed to the shader
        /// </summary>
        public Texture[] textures;

        /// <summary>
        /// Creates the mesh and allocates all resources
        /// </summary>
        /// <param name="manager"></param>
        /// <returns>The mesh</returns>
        public Mesh(GLResourceManager manager) : base(manager)
        {
            VBO = GL.GenBuffer();
            VAO = GL.GenVertexArray();
            EBO = GL.GenBuffer();
        }

        /// <summary>
        /// Updates the GPU end of the mesh, make sure the meshes shader is set, as it will try to locate shader variables
        /// </summary>

        public void UpdateGLData(){
            // We cannot locate variables in a non-existent 
            if(shader == null)
                throw new NullReferenceException("Shader is null, make sure to set shader before updating data!");
            
            // Floats per vertex
            int floatsperv = 3 + 2 + 4; // Vec3 + Vec2 + Col4
            float[] bakedData = new float[verticies.Length * floatsperv]; // The baked vertex data array

            for (var i = 0; i < verticies.Length; i++)
            {
                /* 
                Each vertex is stored in a piece of bakedData, in the order:
                pos.x
                pos.y
                pos.z
                uv.x
                uv.y
                color.r
                color.g
                color.b
                color.a
                */
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

            GL.BindVertexArray(VAO); // Bind our vertex array

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangles.Length * sizeof(uint), triangles, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO); // Bind buffer

            GL.BufferData(BufferTarget.ArrayBuffer, bakedData.Length * sizeof(float), bakedData, BufferUsageHint.StaticDraw); // Set buffer data to baked vertex data

            int vpos = shader.GetAttribLocation("vert_position");
            int vuv = shader.GetAttribLocation("vert_uv");
            int vcol = shader.GetAttribLocation("vert_color");

            GL.VertexAttribPointer(vpos, 3, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 0 * sizeof(float));
            GL.EnableVertexAttribArray(vpos);

            GL.VertexAttribPointer(vuv, 2, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vuv);

            GL.VertexAttribPointer(vcol, 4, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(vcol);

            m_view = shader.GetUniformLocation("matx_view");
            m_model = shader.GetUniformLocation("matx_model");
        
        }

        /// <summary>
        /// Render the mesh.
        /// </summary>
        /// <param name="camera">The active camera object</param>
        /// <param name="t">The meshes transform</param>
        /// <param name="ctx">The current GraphicsContext</param>
        public void Render(Camera camera, Transform t, GraphicsContext ctx){
            GL.BindVertexArray(VAO);
            shader.Use();
            GL.UniformMatrix4(m_view, 1, false, camera.GetMatrix(ctx).ToArray());
            GL.UniformMatrix4(m_model, 1, false, t.GetMatrix().ToArray());

            for (byte i = 0; i < textures.Length; i++)
            {
                GL.Uniform1(shader.GetUniformLocation("mat_texture" + i.ToString()), i);
                textures[i].Use(i);
            }

            GL.DrawElements(PrimitiveType.Triangles, triangles.Length, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// Unload the mesh
        /// </summary>
        public override void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VBO);
            GL.DeleteVertexArray(VAO);
        }
    }
}