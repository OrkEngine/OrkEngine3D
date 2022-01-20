using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using Vector3 = OrkEngine3D.Mathematics.Vector3;
using Color4 = OrkEngine3D.Mathematics.Color4;
using Vector2 = OrkEngine3D.Mathematics.Vector2;
using System.Runtime.InteropServices;
using OrkEngine3D.Components.Core;
using System.Collections.Generic;

namespace OrkEngine3D.Graphics.TK
{
    public class GraphicsContext
    {
        public GameWindow window;
        public GLResourceManager glmanager;
        public void Run(string title){
            var nws = NativeWindowSettings.Default;
            var gws = GameWindowSettings.Default;

            nws.Title = title;
            nws.Size = new Vector2i(800, 600);
            nws.API = ContextAPI.OpenGL;
            nws.APIVersion = new Version(4, 0, 0);      

            window = new GameWindow(gws, nws);

            window.Load += OnLoad;
            window.RenderFrame += OnRender;
            window.UpdateFrame += OnUpdate;
            window.Unload += Unload;
            window.Resize += OnResize;

            glmanager = new GLResourceManager();

            window.Run();
        }
        Shader fshader;
        Shader vshader;
        ShaderProgram program;
        Camera camera;
        private void OnLoad(){
            GL.Enable(EnableCap.DepthTest);
            GL.DebugMessageCallback(MessageCallback, IntPtr.Zero);
            
            mesh = new Mesh(glmanager);
            fshader = new Shader(glmanager, fshadersource, ShaderType.FragmentShader);
            vshader = new Shader(glmanager, vshadersource, ShaderType.VertexShader);

            program = new ShaderProgram(glmanager, vshader, fshader);

            mesh.shader = program;
            
            uint vertexIndex = 0;
            List<Vector3> vertices = new List<Vector3> ();
            List<uint> triangles = new List<uint> ();
            List<Vector2> uvs = new List<Vector2> ();

            for (int p = 0; p < 6; p++) { 
                for (int i = 0; i < 6; i++) {

                    int triangleIndex = VoxelData.voxelTris [p, i];
                    vertices.Add (VoxelData.voxelVerts [triangleIndex] - (Vector3.One * 0.5f));
                    triangles.Add (vertexIndex);

                    uvs.Add (VoxelData.voxelUvs [i]);

                    vertexIndex++;

                }
            }

            mesh.verticies = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();

            Texture testTexture = new Texture(glmanager, Texture.GetTextureDataFromFile("thevroom.png"));

            mesh.textures = new Texture[] {testTexture};

            mesh.UpdateGLData();

            camera = new Camera();
            camera.perspective = true;
            meshTransform = new Transform();
            
        }

        Mesh mesh;
        Transform meshTransform;

        private void OnRender(FrameEventArgs e){

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            mesh.Render(camera, meshTransform, this);

            window.SwapBuffers();
        }
        float t = 0;
        private void OnUpdate(FrameEventArgs e){
            t += (float)e.Time;

            meshTransform.position.Z = -2f;// + MathF.Sin(t);
            meshTransform.Rotate(-Vector3.One * (float)e.Time);
        }

        private void Unload(){
            glmanager.Unload();
        }

        private void OnResize(ResizeEventArgs args){
            GL.Viewport(0, 0, args.Width, args.Height);
        }

        void MessageCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            if(severity == DebugSeverity.DebugSeverityHigh){
                Console.Error.WriteLine($"GL ERROR type = {type.ToString()}, severity = {severity.ToString()}, message = {Marshal.PtrToStringAuto(message)}");
            } else{
                Console.WriteLine($"GL CALLBACK: {(type == DebugType.DebugTypeError ? "**GL ERROR**" : "")} type = {type.ToString()}, severity = {severity.ToString()}, message = {Marshal.PtrToStringAuto(message)}");
            }
        }

        string vshadersource = @"
#version 330 core
in vec3 vert_position;
in vec4 vert_color;
in vec2 vert_uv;

out vec4 fColor;
out vec3 fPos;
out vec2 fUV;

uniform mat4 matx_model;
uniform mat4 matx_view;

void main()
{
    gl_Position = matx_view * matx_model * vec4(vert_position, 1.0);
    fColor = vert_color;
    fUV = vert_uv;
    fPos = vert_position;
}
        ";

        string fshadersource = @"
#version 330 core
out vec4 FragColor;

in vec4 fColor;
in vec3 fPos;
in vec2 fUV;

uniform sampler2D mat_texture0;

void main()
{
    FragColor = texture(mat_texture0, fUV);
}

        ";
    }

    public static class VoxelData {


	public static readonly Vector3[] voxelVerts = new Vector3[8] {

		new Vector3(0.0f, 0.0f, 0.0f),
		new Vector3(1.0f, 0.0f, 0.0f),
		new Vector3(1.0f, 1.0f, 0.0f),
		new Vector3(0.0f, 1.0f, 0.0f),
		new Vector3(0.0f, 0.0f, 1.0f),
		new Vector3(1.0f, 0.0f, 1.0f),
		new Vector3(1.0f, 1.0f, 1.0f),
		new Vector3(0.0f, 1.0f, 1.0f),

	};

	public static readonly int[,] voxelTris = new int[6,6] {

		{0, 3, 1, 1, 3, 2}, // Back Face
		{5, 6, 4, 4, 6, 7}, // Front Face
		{3, 7, 2, 2, 7, 6}, // Top Face
		{1, 5, 0, 0, 5, 4}, // Bottom Face
		{4, 7, 0, 0, 7, 3}, // Left Face
		{1, 2, 5, 5, 2, 6} // Right Face

	};

	public static readonly Vector2[] voxelUvs = new Vector2[6] {

		new Vector2 (0.0f, 0.0f),
		new Vector2 (0.0f, 1.0f),
		new Vector2 (1.0f, 0.0f),
		new Vector2 (1.0f, 0.0f),
		new Vector2 (0.0f, 1.0f),
		new Vector2 (1.0f, 1.0f)

	};


}
}