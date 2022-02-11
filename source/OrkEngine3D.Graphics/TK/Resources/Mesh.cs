using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using OrkEngine3D.Mathematics;
using OrkEngine3D.Components.Core;
using OrkEngine3D.Diagnostics.Logging;

namespace OrkEngine3D.Graphics.TK.Resources
{
    /// <summary>
    /// A mesh.
    /// </summary>
    public class Mesh : GLResource, IRenderable
    {
        int VBO;
        int VAO;
        int EBO;

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
        /// The mesh normals
        /// </summary>
        public Vector3[] normals = new Vector3[0];

        /// <summary>
        /// The triangles of the mesh
        /// </summary>
        public uint[] triangles = new uint[0];

        /// <summary>
        /// What shader should the mesh use?
        /// </summary>
        public ShaderProgram shader;

        public int[] materials;

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
                Logger.Get("ShaderLoader", "Graphics").Log(LogMessageType.FATAL, "Shader is null, make sure to set shader before updating data!");
            
            // Floats per vertex
            int floatsperv = 3 + 3 + 2 + 4 + 1; // Vec3 + Vec3 + Vec2 + Col4 + int
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
                bakedData[i * floatsperv + 9] = (normals.Length > 0 ? normals[i].X : 0);
                bakedData[i * floatsperv + 10] = (normals.Length > 0 ? normals[i].Y: 0);
                bakedData[i * floatsperv + 11] = (normals.Length > 0 ? normals[i].Z : 0);
                bakedData[i * floatsperv + 12] = (materials.Length > 0 ? materials[i] : 0);
            }

            GL.BindVertexArray(VAO); // Bind our vertex array

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangles.Length * sizeof(uint), triangles, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO); // Bind buffer

            GL.BufferData(BufferTarget.ArrayBuffer, bakedData.Length * sizeof(float), bakedData, BufferUsageHint.StaticDraw); // Set buffer data to baked vertex data

            int vpos = shader.GetAttribLocation("vert_position");
            int vuv = shader.GetAttribLocation("vert_uv");
            int vcol = shader.GetAttribLocation("vert_color");
            int vnorm = shader.GetAttribLocation("vert_normal");
            int vmat = shader.GetAttribLocation("vert_material");

            GL.VertexAttribPointer(vpos, 3, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 0 * sizeof(float));
            GL.EnableVertexAttribArray(vpos);

            GL.VertexAttribPointer(vuv, 2, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vuv);

            GL.VertexAttribPointer(vcol, 4, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(vcol);

            GL.VertexAttribPointer(vnorm, 3, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 9 * sizeof(float));
            GL.EnableVertexAttribArray(vnorm);

            GL.VertexAttribPointer(vmat, 1, VertexAttribPointerType.Float, false, floatsperv * sizeof(float), 12 * sizeof(float));
            GL.EnableVertexAttribArray(vmat);

        }

        /// <summary>
        /// Render the mesh.
        /// </summary>
        /// <param name="camera">The active camera object</param>
        /// <param name="t">The meshes transform</param>
        /// <param name="ctx">The current GraphicsContext</param>
        public void Render(){
            GL.BindVertexArray(VAO);
            shader.Use();
            shader.UniformMatrix("matx_view", Rendering.currentCamera.GetMatrix());
            shader.UniformMatrix("matx_model", Rendering.currentTransform.GetMatrix());
            shader.Uniform1("ambient.strength", Rendering.currentLightning.ambient.strength);
            shader.Uniform3("ambient.color", Rendering.currentLightning.ambient.color);
            shader.Uniform3("ambient.position", Rendering.currentLightning.ambient.position);

            shader.Uniform3("camera_pos", Rendering.currentCamera.transform.position);

            for (int i = 0; i < Rendering.currentLightning.lights.Length; i++)
            {
                Light light = Rendering.currentLightning.lights[i];
                shader.Uniform1("lights[0].strength", light.strength);
                shader.Uniform3("lights[0].color", light.color);
                shader.Uniform3("lights[0].position", light.position);
            }

            shader.Uniform1("lights_count", Rendering.currentLightning.lights.Length);

            for (int i = 0; i < Rendering.currentMaterials.Length; i++)
            {
                Material material = Rendering.currentMaterials[i];

                shader.Uniform3($"materials[{i}].ambient", material.ambient);
                shader.Uniform3($"materials[{i}].diffuse", material.diffuse);
                shader.Uniform3($"materials[{i}].specular", material.specular);

                shader.Uniform1($"materials[{i}].shininess", material.shininess);



                for (byte t = 0; t < material.textures.Length; t++)
                {
                    shader.Uniform1($"material_textures[{i * 16 + t}]" + t.ToString(), t);
                    Rendering.currentResourceManager.GetResource<Texture>(material.textures[t]).Use(t);
                }
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