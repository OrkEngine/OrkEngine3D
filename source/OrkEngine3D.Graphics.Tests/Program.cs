using OrkEngine3D.Components.Core;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using System;
using System.IO;
using System.Linq;

namespace OrkEngine3D.Graphics.Tests
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

            Rendering.BindContext(context);

            mesh = new Mesh(resourceManager);
            fshader = new Shader(resourceManager, File.ReadAllText("resources/shader.frag"), ShaderType.FragmentShader);
            vshader = new Shader(resourceManager, File.ReadAllText("resources/shader.vert"), ShaderType.VertexShader);

            program = new ShaderProgram(resourceManager, vshader, fshader);

            mesh.shader = program;

            ObjComplete voxelInformation = ObjLoader.LoadObjFromFile("resources/model.obj");//VoxelData.GenerateVoxelInformation();

            Rendering.BindMaterials(voxelInformation.materials);

            mesh.verticies = voxelInformation.meshInformation.verticies;
            mesh.triangles = voxelInformation.meshInformation.triangles;
            mesh.uv = voxelInformation.meshInformation.uv;
            mesh.normals = voxelInformation.meshInformation.normals;
            mesh.materials = voxelInformation.meshInformation.materials;



            renderBuffer = new RenderBuffer(resourceManager, 1280, 720);



            mesh.UpdateGLData();

            camera = new Camera();
            camera.perspective = true;
            meshTransform = new Transform();

            Rendering.BindCamera(camera);

            lscene = new LightScene();
            Rendering.BindLightning(lscene);

            meshTransform.position.Z = -0.5f;// + MathF.Sin(t);
            meshTransform.position.Y = -4f;
            meshTransform.position.Y = 0f;

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
            meshTransform.Rotate(Vector3.UnitY * context.deltaTime);
            //lscene.light.color = new Color3((MathF.Sin(t) + 1) / 2, (MathF.Cos(t) + 1) / 2, MathF.Max(MathF.Cos(t), (MathF.Sin(t)) + 1) / 2);


            while (context.nonQueriedKeys.Count > 0)
            {
                KeyEvent e = context.nonQueriedKeys.Dequeue();
                Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.eventType.ToString()}, {e.key.ToString()}");
            }
        }
    }
}
