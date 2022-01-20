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
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OrkEngine3D.Graphics.TK
{
    public class GraphicsContext
    {
        public GameWindow window;
        public GLResourceManager glmanager;
        public GraphicsHandler handler;
        public GraphicsContext(string title, GraphicsHandler handler){
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

            window.MouseMove += MouseMove;
            window.KeyDown += KeyDown;
            window.KeyUp += KeyUp;

            this.handler = handler;
            handler.context = this;
            glmanager = new GLResourceManager();
        }
        public void Run(){
            window.Run();
        }

        private void OnLoad(){
            GL.Enable(EnableCap.DepthTest);
            GL.DebugMessageCallback(MessageCallback, IntPtr.Zero);

            handler.Init();
            
        }

        public Vector2 mouseDelta = Vector2.Zero;
        private void OnRender(FrameEventArgs e){
            mouseDelta = Vector2.Zero;
            deltaTime = (float)e.Time;
            
            handler.Render();
        }

        public void SwapBuffers()
        {
            window.SwapBuffers();
        }


        public Queue<KeyEvent> nonQueriedKeys = new Queue<KeyEvent>();

        private void KeyDown(KeyboardKeyEventArgs e){
            nonQueriedKeys.Enqueue(new KeyEvent(KeyEventType.KeyDown, (Key)e.Key));
        }

        private void KeyUp(KeyboardKeyEventArgs e){
            nonQueriedKeys.Enqueue(new KeyEvent(KeyEventType.KeyUp, (Key)e.Key));
        }

        private void MouseMove(MouseMoveEventArgs e)
        {
            mouseDelta.X += e.DeltaX;
            mouseDelta.Y += e.DeltaY;
        }


        public float deltaTime = 0;
        private void OnUpdate(FrameEventArgs e){
            deltaTime = (float)e.Time;
            handler.Update();
        }

        public void ResetFrameBuffer()
        {
            GL.Viewport(0, 0, window.Size.X, window.Size.Y);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
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

        
    }
    public enum KeyEventType{
        KeyDown,
        KeyUp,
    }
    public struct KeyEvent{
        public KeyEventType eventType;
        public Key key;

        public KeyEvent(KeyEventType eventType, Key key)
        {
            this.eventType = eventType;
            this.key = key;
        }
    }
}