using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Mathematics;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace OrkEngine3D.Graphics.TK.Resources;
/*
public class ShadowHandler : GLResource
{
    public int depthMapFBO;
    public int depthMap;
    public const int ShadowWidth = 1024, ShadowHeight = 1024;
    public ShaderProgram shaderProgram;
    public ShadowHandler(GLResourceManager manager) : base(manager)
    {
        depthMapFBO = GL.GenFramebuffer();
        Logger.Get("ShadowHandler", "Graphics").Log(LogMessageType.DEBUG, depthMapFBO.ToString());

        depthMap = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, depthMap);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, ShadowWidth, 
            ShadowHeight, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
        
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthMapFBO);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthMap, 0);
        GL.DrawBuffer(DrawBufferMode.None);
        GL.ReadBuffer(ReadBufferMode.None);
        
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        shaderProgram = new ShaderProgram(manager, 
            new Shader(manager, @"
#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 lightSpaceMatrix;
uniform mat4 model;

void main()
{
    gl_Position = lightSpaceMatrix * model * vec4(aPos, 1.0);
}
", ShaderType.VertexShader
            ),
        new Shader(manager, @"
#version 330 core

void main()
{             
    // gl_FragDepth = gl_FragCoord.z;
}
", ShaderType.FragmentShader
            ));
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
        Logger.Get("ShadowHandler", "Graphics").Log(LogMessageType.FATAL, "Cannot clear shadow handler!");
    }

    public void PreRender()
    {
        float near_plane = 1.0f, far_plane = 7.5f;
        Matrix lightProjection = Matrix.OrthoOffCenterLH(-10.0f, 10.0f, -10.0f, 10.0f, near_plane, far_plane);
        Mathematics.Vector3 target = Rendering.currentLightning.ambient.position;
        Matrix lightView = Matrix.LookAtLH(target, Mathematics.Vector3.Zero, Mathematics.Vector3.UnitY);
        Matrix lightSpaceMatrix = Matrix.Multiply(lightProjection, lightView);
        shaderProgram.Use();
        shaderProgram.UniformMatrix("lightSpaceMatrix", lightSpaceMatrix);
    }
}*/