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

        LightScene lscene;

        Transform cubeTransform;
        Transform teapotTransform;

        Mesh cubeMesh;
        Mesh teapotMesh;
        public override void Init()
        {

            Rendering.BindContext(context);

            
            fshader = new Shader(resourceManager, File.ReadAllText("resources/shader.frag"), ShaderType.FragmentShader);
            vshader = new Shader(resourceManager, File.ReadAllText("resources/shader.vert"), ShaderType.VertexShader);

            program = new ShaderProgram(resourceManager, vshader, fshader);

            cubeMesh = new Mesh(resourceManager);

            //ObjComplete voxelInformation = ObjLoader.LoadObjFromFile("resources/2cube.obj");
            Color3 white = new Color3(1f, 1f, 1f);
            ObjComplete voxelInformation = new ObjComplete(VoxelData.GenerateVoxelInformation(), new Material[] { new Material() });

            Rendering.BindMaterials(voxelInformation.materials);

            

            cubeMesh.verticies = voxelInformation.meshInformation.verticies;
            cubeMesh.uv = voxelInformation.meshInformation.uv;
            cubeMesh.normals = voxelInformation.meshInformation.normals;
            cubeMesh.materials = voxelInformation.meshInformation.materials;
            cubeMesh.triangles = voxelInformation.meshInformation.triangles;
            cubeMesh.shader = program;

            cubeMesh.UpdateGLData();

            cubeTransform = new Transform();

            cubeTransform.position.Z = -0.5f;// + MathF.Sin(t);
            cubeTransform.position.Y = 0f;
            //cubeTransform.position.X = -1;


            /*
            teapotMesh = new Mesh(resourceManager);

            ObjComplete potInformation = ObjLoader.LoadObjFromFile("resources/teapot.obj");

            Rendering.BindMaterials(potInformation.materials);

            teapotMesh.verticies = potInformation.meshInformation.verticies;
            teapotMesh.uv = potInformation.meshInformation.uv;
            teapotMesh.normals = potInformation.meshInformation.normals;
            teapotMesh.materials = potInformation.meshInformation.materials;
            teapotMesh.shader = program;

            teapotMesh.UpdateGLData();

            teapotTransform = new Transform();

            teapotTransform.position.Z = -0.5f;// + MathF.Sin(t);
            teapotTransform.position.Y = 0f;
            //teapotTransform.position.X = 1;
            */

            camera = new Camera();
            camera.perspective = true;

            Rendering.BindCamera(camera);

            lscene = new LightScene();
            Rendering.BindLightning(lscene);

        }

        public override void Render()
        {
            Rendering.ResetTarget();
            Rendering.ClearTarget();


            Rendering.BindTransform(cubeTransform);
            cubeMesh.Render();

            //Rendering.BindTransform(teapotTransform);
            //teapotMesh.Render();

            Rendering.SwapBuffers();
        }
        float t = 0;
        public override void Update()
        {
            t += context.deltaTime;
            cubeTransform.position.Z = -3f;
            cubeTransform.Rotate(Vector3.UnitY * context.deltaTime);

            //teapotTransform.position.Z = -3f;
            //teapotTransform.Rotate(Vector3.UnitX * context.deltaTime);


            while (context.nonQueriedKeys.Count > 0)
            {
                KeyEvent e = context.nonQueriedKeys.Dequeue();
                Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.eventType.ToString()}, {e.key.ToString()}");
            }
        }
    }
}
