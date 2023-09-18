using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OrkEngine3D.Graphics.TK.Resources;

namespace OrkEngine3D.Graphics.TK;

public abstract class GraphicsBehaviour
{
    public GContext context;

    public CursorState CursorState
    {
        get => context.window.CursorState;
        set => context.window.CursorState = value;
    }

    public bool IsFocused => context.window.IsFocused;

    public MouseState MouseState => context.window.MouseState;

    public KeyboardState KeyboardState => context.window.KeyboardState;

    public Vector2i Size => context.window.Size;

    public GLResourceManager resourceManager { get { return context.glmanager; } }

    /// <summary>
    /// On screen resize event
    /// </summary>
    public abstract void OnResize(ResizeEventArgs args);

    /// <summary>
    /// Unload references, same as Dispose()
    /// </summary>
    public abstract void OnUnload();

    /// <summary>
    /// On application render event
    /// </summary>
    public abstract void OnRender(FrameEventArgs e);

    /// <summary>
    /// On application update event
    /// </summary>
    public abstract void OnUpdate(FrameEventArgs e);

    //TODO: add more call events
    public abstract void OnMouseWheel(MouseWheelEventArgs e);

    /// <summary>
    /// On application load event
    /// </summary>
    public abstract void OnLoad();

    public virtual void Close() { context.window.Close(); }

    public virtual void SwapBuffers() { context.window.SwapBuffers(); }
}