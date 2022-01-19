using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;

namespace OrkEngine3D.Graphics.TK
{
    public class GraphicsContext
    {
        public GameWindow window;
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

            window.Run();
        }

        private void OnLoad(){
            
        }

        private void OnRender(FrameEventArgs e){

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            window.SwapBuffers();
        }

        private void OnUpdate(FrameEventArgs e){

        }
    }
}