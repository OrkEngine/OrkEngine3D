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
using OrkEngine3D.Core.Components;
using System.Threading;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Security.Cryptography;
//using OrkEngine3D.Components;

namespace OrkEngine3D.Graphics.Tests
{
    class Program
    {
        static void Main(string[] args)
        {

            Logger logger = new Logger("MainLogger", "NoModule");
            logger.Log(LogMessageType.DEBUG, "Teapot");

            GContext ctx = new GContext("Hello World", new TestHandler());
            ctx.Run();
        }
    }

    

    class TestHandler : GraphicsBehaviour
    {
        Logger logger = new Logger("GraphicsTest", "TestHandler");
        ID fshader;
        ID vshader;
        ID program;
        Camera camera;
        public float speed = 5;

        LightScene lscene;

        Transform cubeTransform;
        Transform teapotTransform;

        ID cubeMesh;
        ID teapotMesh;
        private ID shadowManager;
        public override void OnLoad()
        {

            Rendering.BindContext(context);
            Rendering.BindResourceManager(resourceManager);
            

            
            fshader = Rendering.CreateShader(File.ReadAllText("resources/shader.frag"), TK.Resources.ShaderType.FragmentShader);
            vshader = Rendering.CreateShader(File.ReadAllText("resources/shader.vert"), TK.Resources.ShaderType.VertexShader);
            shadowManager = Rendering.CreateShadowManager();

            program = Rendering.CreateShaderProgram(vshader, fshader);

            cubeMesh = Rendering.CreateMesh();

            Color3 white = new Color3(1f, 1f, 1f);
            ObjComplete voxelInformation = ObjLoader.LoadObjFromFile("resources/scene.obj");//new ObjComplete(VoxelData.GenerateVoxelInformation(), new Material[] { new Material() });
            voxelInformation.materials[0].textures = new ID[] {Rendering.CreateTexture(Texture.GetTextureDataFromFile("resources/wod.png"))};
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

            camera = new Camera();//(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            camera.perspective = true;

            Rendering.BindCamera(camera);

            lscene = new LightScene();
            Rendering.BindLightning(lscene);
            Rendering.BindShadowManager(shadowManager);

            cubeTransform.position.Z = -3f;
            cubeTransform.position.Y = -3f;
            cubeTransform.Rotate(Vector3.UnitX * 45);

        }

        public override void OnRender(FrameEventArgs e)
        {
            Rendering.BindTransform(cubeTransform);
            Rendering.BindRenderable(cubeMesh);
            
            //Rendering.EnterShadowMode();
            //Rendering.OnRender();
            
            Rendering.ResetTarget();
            Rendering.ClearTarget();
            Rendering.Render();

            //Rendering.BindTransform(teapotTransform);
            //teapotMesh.OnRender();

            Rendering.SwapBuffers();
        }

        float t = 0;
        public override void OnUpdate(FrameEventArgs e)
        {
            t += context.deltaTime;

            //Rendering.currentCamera.transform.position = new Vector3(t, t, t);
            //teapotTransform.position.Z = -3f;
            //teapotTransform.Rotate(Vector3.UnitX * context.deltaTime);
            var fpsString = "FPS=";
            var fps = (1000 / context.deltaTime);

            bool isRaw = false;
            bool isDebug = false;
            Rendering.currentCamera.transform.position = new Vector3((float)e.Time, (float)e.Time, (float)e.Time);
            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;
            Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.Time}");

            if (fps >= 120)
            {
                if (isRaw)
                {
                    fpsString = $"FPS=~{fps}";
                }
                else
                {
                    if (fps >= 1200)
                        fpsString = "FPS=~120";
                }   
            }
            else if (fps >= 90 && fps <= 120)
            {
                fpsString = "FPS=~90";
            }
            else if (fps >= 60 && fps <= 90)
            {
                fpsString = "FPS=~60";
            }
            else if (fps >= 30 && fps >= 60)
            {
                fpsString = "FPS=~30";
            }

            //var counter = 0;

            //if (counter >= 20)
            //{
            //    for (var i = 0; i < counter; i++)
            //        counter++;
            if (isDebug)
                Console.WriteLine(fpsString);
            //Thread.Sleep(250);
            // }
            Rendering.currentCamera.transform.position.X = 600;

            while (context.nonQueriedKeys.Count > 0)
            {
                KeyEvent keyEvent = context.nonQueriedKeys.Dequeue();//GContext.Local.Keys;
                //Logger.Get("MainLogger").Log(LogMessageType.DEBUG, $"Keyboard: {e.eventType.ToString()}, {e.Key.ToString()}");
                if (keyEvent.GetKeyDown(Key.Q))
                {
                    Rendering.EnableWireframe();
                }

                if (keyEvent.GetKeyDown(Key.E)) //if (e.Key == Key.E && e.GetKeyDown(Key.E))
                {
                    Rendering.DisableWireframe();
                }
               // if (keyEvent.GetKeyDown(Key.W))
                //{
                    //Rendering.currentCamera.transform.position.X += cameraSpeed * (float)e.Time;
                //}
            }
        }

        public override void OnMouseWheel(MouseWheelEventArgs e)
        {
            // camera.Fov -= e.OffsetY;
            Rendering.currentCamera.fov -= e.OffsetY;
        }

        public override void OnResize(ResizeEventArgs args)
        {
            logger.Log(LogMessageType.INFORMATION, "OnResize event");
            //camera.AspectRatio = Size.X / (float)Size.Y;
        }   
        
        public override void OnUnload()
        {
            logger.Log(LogMessageType.INFORMATION, "OnUnload event");
        }

        public uint cubeMapTextures = LoadCubemap(new List<string>()
        {
            "right.jpg",
            "left.jpg",
            "top.jpg",
            "bottom.jpg",
            "front.jpg",
            "back.jpg"
        });

        public static uint LoadCubemap(List<string> faces)
        {
            uint textureID;

            //GL.TextureParameter()
            return 0;
        }
    }
}
