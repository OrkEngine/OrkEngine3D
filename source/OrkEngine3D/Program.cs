using System;
using System.Collections.Generic;
using System.IO;
using OrkEngine3D.Components.Core;
using OrkEngine3D.Graphics;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using MathF = OrkEngine3D.Mathematics.MathF;

namespace OrkEngine3D
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GraphicsContext ctx = new GraphicsContext("Hello World", new TestHandler());
            ctx.Run();
        }
    }

    class TestHandler : GraphicsHandler
    {
        Shader fshader;
        Shader vshader;
        ShaderProgram program;
        Camera camera;
        Mesh mesh;
        Transform meshTransform;
        RenderBuffer renderBuffer;
        LightScene lscene;
        public override void Init()
        {
            mesh = new Mesh(resourceManager);
            fshader = new Shader(resourceManager, File.ReadAllText("shader.frag"), ShaderType.FragmentShader);
            vshader = new Shader(resourceManager, File.ReadAllText("shader.vert"), ShaderType.VertexShader);

            program = new ShaderProgram(resourceManager, vshader, fshader);

            mesh.shader = program;

            MeshInformation voxelInformation = VoxelData.GenerateVoxelInformation();

            mesh.verticies = voxelInformation.verticies;
            mesh.triangles = voxelInformation.triangles;
            mesh.uv = voxelInformation.uv;
            mesh.normals = voxelInformation.normals;

            Texture testTexture = new Texture(resourceManager, Texture.GetTextureDataFromFile("thevroom.png"));

            renderBuffer = new RenderBuffer(resourceManager, 1280, 720);

            mesh.textures = new Texture[] { testTexture };

            mesh.UpdateGLData();

            camera = new Camera();
            camera.perspective = true;
            meshTransform = new Transform();

            Rendering.BindContext(context);
            
            Rendering.BindCamera(camera);


            lscene = new LightScene();
            Rendering.BindLightning(lscene);

        }

        public override void Render()
        {

            Rendering.BindTarget(renderBuffer);
            Rendering.ClearTarget();

            Rendering.BindTransform(meshTransform);
            mesh.Render();


            Rendering.ResetTarget();
            Rendering.ClearTarget();

            Rendering.BindTransform(meshTransform);
            mesh.Render();

            Rendering.SwapBuffers();
        }
        float t = 0;
        public override void Update()
        {
            t += context.deltaTime;
            meshTransform.position.Z = -2f;// + MathF.Sin(t);
            meshTransform.Rotate(-Vector3.One * context.deltaTime);

            lscene.light.color = new Color3((MathF.Sin(t) + 1) / 2, (MathF.Cos(t) + 1) / 2, MathF.Max(MathF.Cos(t), (MathF.Sin(t)) + 1) / 2);


            while(context.nonQueriedKeys.Count > 0){
                KeyEvent e = context.nonQueriedKeys.Dequeue();
                Console.WriteLine($"Keyboard: {e.eventType.ToString()}, {e.key.ToString()}");
            }
        }

        string vshadersource = @"
#version 330 core
in vec3 vert_position;
in vec4 vert_color;
in vec2 vert_uv;
in vec3 vert_normal;

out vec4 fColor;
out vec3 fPos;
out vec2 fUV;
out vec3 fNorm;

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
uniform sampler2D mat_texture1;

void main()
{
    FragColor = texture(mat_texture1, fUV);
    FragColor = texture(mat_texture0, fUV);
}

        ";
    }
}
