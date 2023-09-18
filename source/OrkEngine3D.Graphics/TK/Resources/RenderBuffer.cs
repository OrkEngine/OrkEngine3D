﻿using OpenTK.Graphics.OpenGL4;
using System;

namespace OrkEngine3D.Graphics.TK.Resources;

/*
public class RenderBuffer : GLResource, IRenderTarget
{
    public int id;
    public int depthBuffer;
    public readonly Texture target;

    public RenderBuffer(GLResourceManager manager, int width, int height) : base(manager)
    {
        id = GL.GenFramebuffer();

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);

        int renderedTexture;
        renderedTexture = GL.GenTexture();

        // "Bind" the newly created texture : all future texture functions will modify this texture
        GL.BindTexture(TextureTarget.Texture2D, renderedTexture);

        // Give an empty image to OpenGL ( the last "0" )
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 1024, 768, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

        // Poor filtering. Needed !
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

        target = new Texture(manager, renderedTexture, width, height);


        depthBuffer = GL.GenRenderbuffer();
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthBuffer);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, width, height);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBuffer);

        GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, renderedTexture, 0);

        DrawBuffersEnum[] DrawBuffers = { DrawBuffersEnum.ColorAttachment0 };
        GL.DrawBuffers(1, DrawBuffers);
    }

    public void BindTarget()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
        GL.Viewport(0, 0, target.width, target.height);
    }

    public void Clear()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void PreRender()
    {
        
    }

    public override void Unload()
    {
        GL.DeleteRenderbuffer(id);
        GL.DeleteFramebuffer(id);
    }
}
*/