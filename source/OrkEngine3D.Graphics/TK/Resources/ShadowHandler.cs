using System;
using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Diagnostics.Logging;

namespace OrkEngine3D.Graphics.TK.Resources;

public class ShadowHandler : GLResource, IRenderTarget
{
    private int depthMapFBO;
    private int depthMap;
    const int ShadowWidth = 1024, ShadowHeight = 1024;
    public ShadowHandler(GLResourceManager manager) : base(manager)
    {
        depthMapFBO = GL.GenFramebuffer();

        depthMap = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, depthMap);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, ShadowWidth, 
            ShadowHeight, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
        
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthMapFBO);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthMap, 0);
        GL.DrawBuffer(DrawBufferMode.None);
        GL.ReadBuffer(ReadBufferMode.None);
        
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public override void Unload()
    {
        
    }

    public void BindTarget()
    {
        GL.Viewport(0, 0, ShadowWidth, ShadowHeight);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthMapFBO);
        GL.Clear(ClearBufferMask.DepthBufferBit);
    }

    public void Clear()
    {
        Logger.Get("ShadowHandler", "Graphics").Log(LogMessageType.ERROR, "Cannot clear shadow handler!");
    }
}