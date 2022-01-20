using System;
using System.Collections.Generic;
using System.IO;
using OrkEngine3D.Components.Core;
using OrkEngine3D.Graphics;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Mathematics;
using MathF = OrkEngine3D.Mathematics.MathF;

namespace OrkEngine3D
{
    class Program
    {
        static void Main(string[] args)
        {
			Logger logger = new Logger("MainLogger", "NoModule");
			logger.Log(LogMessageType.DEBUG, "Teapot");
            
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

            MeshInformation voxelInformation = ObjLoader.LoadObjData(File.ReadAllText("model.obj"), out string mtlfile);//VoxelData.GenerateVoxelInformation();
            Material mat = ObjLoader.LoadMTLFromFile(File.ReadAllText(mtlfile));
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

            meshTransform.position.Z = -0.5f;// + MathF.Sin(t);
            meshTransform.position.Y = -4f;
            meshTransform.scale = Vector3.One * 1.5f;
            meshTransform.position.Y = 0f;
            meshTransform.Rotate(new Vector3(MathF.PI / 3, MathF.PI / 2, 0));

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
            meshTransform.position.Z = -3f;// + MathF.Sin(t);
            meshTransform.Rotate(Vector3.One * context.deltaTime);
            //lscene.light.color = new Color3((MathF.Sin(t) + 1) / 2, (MathF.Cos(t) + 1) / 2, MathF.Max(MathF.Cos(t), (MathF.Sin(t)) + 1) / 2);


            while(context.nonQueriedKeys.Count > 0){
                KeyEvent e = context.nonQueriedKeys.Dequeue();
				Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.eventType.ToString()}, {e.key.ToString()}");
            }
        }
    }
}
