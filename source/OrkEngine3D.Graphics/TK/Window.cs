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
            nws.WindowBorder = WindowBorder.Fixed;  
            nws.API = ContextAPI.OpenGL;
            nws.APIVersion = new Version(4, 0, 0);      

            window = new GameWindow(gws, nws);

            window.Load += OnLoad;
            window.RenderFrame += OnRender;
            window.UpdateFrame += OnUpdate;
            window.Unload += Unload;

            glmanager = new GLResourceManager();

            window.Run();
        }
        Shader fshader;
        Shader vshader;
        ShaderProgram program;
        private void OnLoad(){
            GL.DebugMessageCallback(MessageCallback, IntPtr.Zero);
            
            mesh = new Mesh(glmanager);
            fshader = new Shader(glmanager, fshadersource, ShaderType.FragmentShader);
            vshader = new Shader(glmanager, vshadersource, ShaderType.VertexShader);

            program = new ShaderProgram(glmanager, vshader, fshader);

            mesh.shader = program;
            mesh.verticies = new Vector3[] {
                new Vector3(-0.5f, -0.5f, 0),
                new Vector3( 0.0f,  0.5f, 0),
                new Vector3( 0.5f, -0.5f, 0),
            };

            mesh.colors = new Color4[] {
                new Color4(1.0f, 1.0f, 0.0f, 0.0f),
                new Color4(1.0f, 0.0f, 1.0f, 0.0f),
                new Color4(1.0f, 0.0f, 0.0f, 1.0f)
            };

            mesh.uv = new Vector2[]{
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            mesh.UpdateGLData();
            
        }

        Mesh mesh;

        private void OnRender(FrameEventArgs e){

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            mesh.Render();

            window.SwapBuffers();
        }

        private void OnUpdate(FrameEventArgs e){

        }

        private void Unload(){
            glmanager.Unload();
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
in vec3 vPos;
in vec4 vCol;
in vec2 vUv;

out vec4 fColor;
out vec3 fPos;

void main()
{
    gl_Position = vec4(vPos, 1.0);
    fColor = vCol;
    fPos = vPos;
}
        ";

        string fshadersource = @"
#version 330 core
out vec4 FragColor;

in vec4 fColor;
in vec3 fPos;

void main()
{
    FragColor = fColor;//vec4(1.0f, 0.5f, 0.2f, 1.0f);
}

        ";
    }
}