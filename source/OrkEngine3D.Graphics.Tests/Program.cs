using OrkEngine3D.Components.Core;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using System;
using System.IO;
using System.Linq;
using Mesh = OrkEngine3D.Graphics.TK.Resources.Mesh;
using Material = OrkEngine3D.Graphics.TK.Material;

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
        ID fshader;
        ID vshader;
        ID program;
        Camera camera;

        LightScene lscene;

        Transform cubeTransform;
        Transform teapotTransform;

        ID cubeMesh;
        ID teapotMesh;
        public override void Init()
        {

            Rendering.BindContext(context);
            Rendering.BindResourceManager(resourceManager);
            

            
            fshader = Rendering.CreateShader( File.ReadAllText("resources/shader.frag"), ShaderType.FragmentShader);
            vshader = Rendering.CreateShader(File.ReadAllText("resources/shader.vert"), ShaderType.VertexShader);

            program = Rendering.CreateShaderProgram(vshader, fshader);

            cubeMesh = Rendering.CreateMesh();

            Color3 white = new Color3(1f, 1f, 1f);
            ObjComplete voxelInformation = new ObjComplete(VoxelData.GenerateVoxelInformation(), new Material[] { new Material() });
            voxelInformation.materials[0].textures = new ID[] {Rendering.CreateTexture(Texture.GetTextureDataFromFile("resources/logo.png"))};
            Rendering.BindMaterials(voxelInformation.materials);

            Rendering.UpdateMeshVerticies(cubeMesh, voxelInformation.meshInformation.verticies);
            Rendering.UpdateMeshUVs(cubeMesh, voxelInformation.meshInformation.uv);
            Rendering.UpdateMeshNormals(cubeMesh, voxelInformation.meshInformation.normals);
            Rendering.UpdateMeshMaterials(cubeMesh, voxelInformation.meshInformation.materials);
            Rendering.UpdateMeshTriangles(cubeMesh, voxelInformation.meshInformation.triangles);
            Rendering.UpdateMeshShader(cubeMesh, program);

            Rendering.UpdateMeshGLData(cubeMesh);

            cubeTransform = new Transform();

            cubeTransform.position.Z = -0.5f;
            cubeTransform.position.Y = 0f;

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
            Rendering.BindRenderable(cubeMesh);
            Rendering.Render();

            //Rendering.BindTransform(teapotTransform);
            //teapotMesh.Render();

            Rendering.SwapBuffers();
        }
        float t = 0;
        public override void Update()
        {
            t += context.deltaTime;
            cubeTransform.position.Z = -3f;
            cubeTransform.Rotate(Vector3.One * context.deltaTime);

            //teapotTransform.position.Z = -3f;
            //teapotTransform.Rotate(Vector3.UnitX * context.deltaTime);


            while (context.nonQueriedKeys.Count > 0)
            {
                KeyEvent e = context.nonQueriedKeys.Dequeue();
                //Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.eventType.ToString()}, {e.key.ToString()}");

                if (e.key == Key.Q && e.eventType == KeyEventType.KeyDown)
                {
                    Rendering.EnableWireframe();
                }
                if (e.key == Key.E && e.eventType == KeyEventType.KeyDown)
                {
                    Rendering.EnableWireframe();
                }
            }
        }
    }
}
