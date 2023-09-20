using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Diagnostics.Logging;
using Vector2 = OrkEngine3D.Mathematics.Vector2;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace OrkEngine3D.Graphics.TK;

public class GContext
{
    public GameWindow window;
    public GLResourceManager glmanager;
    public GraphicsBehaviour handler;

    public GContext(string title, GraphicsBehaviour handler, bool msaa = true)
    {
        var nws = NativeWindowSettings.Default;
        var gws = GameWindowSettings.Default;

        nws.Title = title;
        nws.Size = new Vector2i(800, 600);
        nws.API = ContextAPI.OpenGL;
        nws.NumberOfSamples = 4;
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

        window.MouseWheel += MouseWheel;

        this.handler = handler;
        handler.context = this;
        glmanager = new GLResourceManager();
    }
    
    private void MouseWheel(MouseWheelEventArgs e)
    {
        handler.OnMouseWheel(e);
    }

    public GContext Run(){
        window.Run();

        return this;
    }

    private void OnLoad(){
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Multisample);
        GL.DebugMessageCallback(MessageCallback, IntPtr.Zero);

        handler.OnLoad();
    }

    public Vector2 mouseDelta = Vector2.Zero;
    private void OnRender(FrameEventArgs e){
        mouseDelta = Vector2.Zero;
        deltaTime = (float)e.Time;
        
        handler.OnRender(e);
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
        handler.OnUpdate(e);
    }

    public void ResetFrameBuffer()
    {
        GL.Viewport(0, 0, window.Size.X, window.Size.Y);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    private void Unload()
    {
        glmanager.Unload();
        handler.OnUnload();
    }

    private void OnResize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
        handler.OnResize(args);
    }

    void MessageCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
    {
        if(severity == DebugSeverity.DebugSeverityHigh){
				Logger.Get("GraphicsLogger", "OrkEngine3D.Graphics").Log(LogMessageType.ERROR,
				$"GL ERROR type = {type.ToString()}, severity = {severity.ToString()}, message = {Marshal.PtrToStringAuto(message)}");
        } else{
				Logger.Get("GraphicsLogger", "OrkEngine3D.Graphics").Log(LogMessageType.INFORMATION,
				$"GL CALLBACK: {(type == DebugType.DebugTypeError ? "**GL ERROR**" : "")} type = {type.ToString()}, severity = {severity.ToString()}, message = {Marshal.PtrToStringAuto(message)}");
        }
    }

    
}
public enum KeyEventType
{
    KeyDown,
    KeyUp,
}
public struct KeyEvent
{

    public KeyEventType eventType { get; set; }
    public Key Key { get; set; }

    public KeyEvent(KeyEventType eventType, Key key)
    {
        this.eventType = eventType;
        this.Key = key;
    }

    [Obsolete("Use GetKeyDown(Key key)")]
    public bool GetKeyDown()
    {
        if (eventType == KeyEventType.KeyDown) return true;

        return false;
    }

    [Obsolete("Use GetKeyUp(Key key)")]
    public bool GetKeyUp()
    {
        if (eventType == KeyEventType.KeyUp) return true;

        return false;
    }

    public bool GetKeyDown(Key key)
    {
        if (Key == key && eventType == KeyEventType.KeyDown) return true;

        return false;
    }

    public bool GetKeyUp(Key key)
    {
        if (Key == key && eventType == KeyEventType.KeyUp) return true;

        return false;
    }
}