using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
//using OrkEngine3D.Mathematics;
using System;
using System.IO;
using System.Linq;
//using Mesh = OrkEngine3D.Graphics.TK.Resources.Mesh;
using Material = OrkEngine3D.Graphics.TK.Material;
using OrkEngine3D.Core.Components;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OrkEngine3D.Core;
//using OrkEngine3D.Components;

namespace OrkEngine3D.Graphics.Tests;

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
class TestHandler : Behaviour
{
    private readonly float[] _vertices =
    {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
    };

    private readonly Vector3[] _cubePositions =
    {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3(2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f, 3.0f, -7.5f),
            new Vector3(1.3f, -2.0f, -2.5f),
            new Vector3(1.5f, 2.0f, -2.5f),
            new Vector3(1.5f, 0.2f, -1.5f),
            new Vector3(-1.3f, 1.0f, -1.5f)
    };

    // We need the point lights' positions to draw the lamps and to get light the materials properly
    private readonly Vector3[] _pointLightPositions =
    {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -12.0f),
            new Vector3(0.0f, 0.0f, -3.0f)
    };
    /*
    // The vertices are now used to draw cubes.
    // For this example, we aren't using texture coordinates.
    // You can use textures with lighting (and we will get onto this), but for simplicity's sake, we'll just use a solid color here
    private readonly float[] _vertices =
    {
        // Position
        -0.5f, -0.5f, -0.5f, // Front face
         0.5f, -0.5f, -0.5f,
         0.5f,  0.5f, -0.5f,
         0.5f,  0.5f, -0.5f,
        -0.5f,  0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,

        -0.5f, -0.5f,  0.5f, // Back face
         0.5f, -0.5f,  0.5f,
         0.5f,  0.5f,  0.5f,
         0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,
        -0.5f, -0.5f,  0.5f,

        -0.5f,  0.5f,  0.5f, // Left face
        -0.5f,  0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,
        -0.5f, -0.5f, -0.5f,
        -0.5f, -0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,

         0.5f,  0.5f,  0.5f, // Right face
         0.5f,  0.5f, -0.5f,
         0.5f, -0.5f, -0.5f,
         0.5f, -0.5f, -0.5f,
         0.5f, -0.5f,  0.5f,
         0.5f,  0.5f,  0.5f,

        -0.5f, -0.5f, -0.5f, // Bottom face
         0.5f, -0.5f, -0.5f,
         0.5f, -0.5f,  0.5f,
         0.5f, -0.5f,  0.5f,
        -0.5f, -0.5f,  0.5f,
        -0.5f, -0.5f, -0.5f,

        -0.5f,  0.5f, -0.5f, // Top face
         0.5f,  0.5f, -0.5f,
         0.5f,  0.5f,  0.5f,
         0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f,  0.5f,
        -0.5f,  0.5f, -0.5f
    };
    */
    /* plane
    private readonly float[] _vertices =
    {
        // Position         Texture coordinates
         0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
    };

    private readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };
    */
    Logger logger = new Logger("GraphicsTest", "TestHandler");
    private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    //private int _elementBufferObject;
    private int _vertexBufferObject;
    //private int _vertexArrayObject;
    private int _vaoModel;
    private int _vaoLamp;
    private Shader _lampShader;
    private Shader _lightingShader;
    //private Shader _shader;
    private Texture _diffuseMap;
    private Texture _specularMap;

    //ID fshader;
    //ID vshader;
    //ID program;
    Camera _camera;
    private bool _firstMove = true;
    private Vector2 _lastPos;
    private double _time;
    //public float speed = 5;
    //ShaderProgram shader;

    //LightScene lscene;

    //Transform cubeTransform;
    //Transform teapotTransform;

    //ID cubeMesh;
    //ID teapotMesh;
    //private ID shadowManager;
    public override void OnLoad()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        GL.Enable(EnableCap.DepthTest);

        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        // Load the two different shaders, they use the same vertex shader program. However they have two different fragment shaders.
        // This is because the lamp only uses a basic shader to turn it white, it wouldn't make sense to have the lamp lit in other colors.
        // The lighting shaders uses the lighting.frag shader which is what a large part of this chapter will be about
        _lightingShader = new Shader("resources/shaders/shader.vert", "resources/shaders/lighting.frag");
        _lampShader = new Shader("resources/shaders/shader.vert", "resources/shaders/shader.frag");

        {
            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);

            // All of the vertex attributes have been updated to now have a stride of 8 float sizes.
            var positionLocation = _lightingShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            // The texture coords have now been added too, remember we only have 2 coordinates as the texture is 2d,
            // so the size parameter should only be 2 for the texture coordinates.
            var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        {
            _vaoLamp = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLamp);

            // The lamp shader should have its stride updated aswell, however we dont actually
            // use the texture coords for the lamp, so we dont need to add any extra attributes.
            var positionLocation = _lampShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        }

        // Our two textures are loaded in from memory, you should head over and
        // check them out and compare them to the results.
        _diffuseMap = Texture.LoadFromFile("resources/container2.png");
        _specularMap = Texture.LoadFromFile("resources/container2_specular.png");

        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;
        //shader = new ShaderProgram();
        //Rendering.BindContext(context);
        //Rendering.BindResourceManager(resourceManager);



        //fshader = Rendering.CreateShader(File.ReadAllText("resources/shader.frag"), TK.Resources.ShaderType.FragmentShader);
        //vshader = Rendering.CreateShader(File.ReadAllText("resources/shader.vert"), TK.Resources.ShaderType.VertexShader);
        //shadowManager = Rendering.CreateShadowManager();

        //program = Rendering.CreateShaderProgram(vshader, fshader);

        //cubeMesh = Rendering.CreateMesh();



        //Color3 white = new Color3(1f, 1f, 1f);
        //ObjComplete voxelInformation = ObjLoader.LoadObjFromFile("resources/scene.obj");//new ObjComplete(VoxelData.GenerateVoxelInformation(), new Material[] { new Material() });
        //voxelInformation.materials[0].textures = new ID[] {Rendering.CreateTexture(Texture.GetTextureDataFromFile("resources/wod.png"))};
        //Rendering.BindMaterials(voxelInformation.materials);

        //Rendering.UpdateMeshVerticies(cubeMesh, voxelInformation.meshInformation.verticies);
        //Rendering.UpdateMeshUVs(cubeMesh, voxelInformation.meshInformation.uv);
        //Rendering.UpdateMeshNormals(cubeMesh, voxelInformation.meshInformation.normals);
        //Rendering.UpdateMeshMaterials(cubeMesh, voxelInformation.meshInformation.materials);
        //Rendering.UpdateMeshTriangles(cubeMesh, voxelInformation.meshInformation.triangles);
        //Rendering.UpdateMeshShader(cubeMesh, program);

        //Rendering.UpdateMeshGLData(cubeMesh);

        //cubeTransform = new Transform();

        //cubeTransform.position.Z = -0.5f;
        //cubeTransform.position.Y = 0f;

        //camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
        //camera.perspective = true;

        //Rendering.BindCamera(camera);

        //lscene = new LightScene();
        //Rendering.BindLightning(lscene);
        //Rendering.BindShadowManager(shadowManager);

        //cubeTransform.position.Z = -3f;
        //cubeTransform.position.Y = -3f;
        //cubeTransform.Rotate(Vector3.UnitX * 45);

    }

    public override void OnRender(FrameEventArgs e)
    {
        _time += 4.0 * e.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.BindVertexArray(_vaoModel);

        _diffuseMap.Use(TextureUnit.Texture0);
        _specularMap.Use(TextureUnit.Texture1);

        BaseLight baseLight = new BaseLight(_camera, _lightingShader);
        /*
        _lightingShader.Use();

        _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        _lightingShader.SetVector3("viewPos", _camera.Position);

        _lightingShader.SetInt("material.diffuse", 0);
        _lightingShader.SetInt("material.specular", 1);
        _lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        _lightingShader.SetFloat("material.shininess", 32.0f);

        */

        /*
           Here we set all the uniforms for the 5/6 types of lights we have. We have to set them manually and index
           the proper PointLight struct in the array to set each uniform variable. This can be done more code-friendly
           by defining light types as classes and set their values in there, or by using a more efficient uniform approach
           by using 'Uniform buffer objects', but that is something we'll discuss in the 'Advanced GLSL' tutorial.
        */
        // Directional light
        DirectionalLight directionalLight = new DirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f));
        //_lightingShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
        //_lightingShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
        //_lightingShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
        //_lightingShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
        
        //pointLight.Camera = _camera;
        // Point lights
        PointLight pointLight = new PointLight(_pointLightPositions);
        /*
        for (int i = 0; i < _pointLightPositions.Length; i++)
        {
            _lightingShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
            _lightingShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _lightingShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
            _lightingShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetFloat($"pointLights[{i}].constant", 1.0f);
            _lightingShader.SetFloat($"pointLights[{i}].linear", 0.09f);
            _lightingShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
        }
        */

        // Spot light

        SpotLight spotLight = new SpotLight(_camera.Position, _camera.Front);

        /*
        _lightingShader.SetVector3("spotLight.position", _camera.Position);
        _lightingShader.SetVector3("spotLight.direction", _camera.Front);
        _lightingShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
        _lightingShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
        _lightingShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
        _lightingShader.SetFloat("spotLight.constant", 1.0f);
        _lightingShader.SetFloat("spotLight.linear", 0.09f);
        _lightingShader.SetFloat("spotLight.quadratic", 0.032f);
        _lightingShader.SetFloat("spotLight.cutOff", Mathematics.MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
        _lightingShader.SetFloat("spotLight.outerCutOff", Mathematics.MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
        */

        for (int i = 0; i < _cubePositions.Length; i++)
        {
            Matrix4 model = Matrix4.CreateTranslation(_cubePositions[i]);
            float angle = 20.0f * i;
            model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
            _lightingShader.SetMatrix4("model", model);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        GL.BindVertexArray(_vaoLamp);

        _lampShader.Use();

        _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        // We use a loop to draw all the lights at the proper position
        for (int i = 0; i < _pointLightPositions.Length; i++)
        {
            Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_pointLightPositions[i]);

            _lampShader.SetMatrix4("model", lampMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        SwapBuffers();
        //Rendering.BindTransform(cubeTransform);
        //Rendering.BindRenderable(cubeMesh);

        //Rendering.EnterShadowMode();
        //Rendering.OnRender();

        //Rendering.ResetTarget();
        //Rendering.ClearTarget();
        //Rendering.Render();

        //Rendering.BindTransform(teapotTransform);
        //teapotMesh.OnRender();

        //Rendering.SwapBuffers();
    }

    //float t = 0;
    public override void OnUpdate(FrameEventArgs e)
    {
        if (!IsFocused) // Check to see if the window is focused
        {
            return;
        }

        var input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;

        if (input.IsKeyDown(Keys.W))
        {
            _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
        }

        if (input.IsKeyDown(Keys.S))
        {
            _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
        }
        if (input.IsKeyDown(Keys.A))
        {
            _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
        }
        if (input.IsKeyDown(Keys.D))
        {
            _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
        }
        if (input.IsKeyDown(Keys.Space))
        {
            _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
        }
        if (input.IsKeyDown(Keys.LeftShift))
        {
            _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
        }

        // Get the mouse state
        var mouse = MouseState;

        if (_firstMove) // This bool variable is initially set to true.
        {
            _lastPos = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
        }
        else
        {
            // Calculate the offset of the mouse position
            var deltaX = mouse.X - _lastPos.X;
            var deltaY = mouse.Y - _lastPos.Y;
            _lastPos = new Vector2(mouse.X, mouse.Y);

            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            _camera.Yaw += deltaX * sensitivity;
            _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
        }
    }

    public override void OnMouseWheel(MouseWheelEventArgs e)
    {
        // camera.Fov -= e.OffsetY;
        // Rendering.currentCamera.fov -= e.OffsetY;
        _camera.Fov -= e.OffsetY;
    }

    public override void OnResize(ResizeEventArgs args)
    {
        logger.Log(LogMessageType.INFORMATION, "OnResize event");
        //camera.AspectRatio = Size.X / (float)Size.Y;
        GL.Viewport(0, 0, Size.X, Size.Y);
        // We need to update the aspect ratio once the window has been resized.
        _camera.AspectRatio = Size.X / (float)Size.Y;
    }

    public override void OnUnload()
    {
        logger.Log(LogMessageType.INFORMATION, "OnUnload event");
    }

    /*
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
    */
}
