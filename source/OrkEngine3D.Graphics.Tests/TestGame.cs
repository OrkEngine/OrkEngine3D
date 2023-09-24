using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
//using OrkEngine3D.Mathematics;
//using Mesh = OrkEngine3D.Graphics.TK.Resources.Mesh;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Core;
using System.Linq;
using OrkEngine3D.Mathematics;
//using OrkEngine3D.Components;

namespace OrkEngine3D.Graphics.Tests;

class TestGame : Behaviour
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

    Logger logger = new Logger("GraphicsTest", "TestHandler");
    private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    private int skyboxTexture;
    //private int _elementBufferObject;
    private int _vertexBufferObject;
    private int _skyboxBufferObject;
    //private int _vertexArrayObject;
    private int _vaoModel;
    private int _vaoLamp;
    private Shader _lampShader;
    private Shader _lightingShader;
    //private Shader _shader;
    private Texture _diffuseMap;
    private Texture _specularMap;
    //private ImGuiController _controller;

    //ID fshader;
    //ID vshader;
    //ID program;
    Camera _camera;
    private bool _firstMove = true;
    private Vector2 _lastPos;
    private double _time;
    private Skybox skybox;
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

        skybox = new Skybox(new string[] { "resources/skybox2/right.jpg", "resources/skybox2/left.jpg", "resources/skybox2/bottom.jpg", "resources/skybox2/top.jpg", "resources/skybox2/front.jpg", "resources/skybox2/back.jpg" });
        // { "resources/skybox2/right.jpg", "resources/skybox2/left.jpg", "resources/skybox2/top.jpg", "resources/skybox2/bottom.jpg", "resources/skybox2/front.jpg", "resources/skybox2/back.jpg" }
        GL.Enable(EnableCap.DepthTest);
        //GL.Enable(EnableCap.CullFace);
        //_skyboxBufferObject = GL.GenBuffer();
        //GL.BindBuffer(BufferTarget.ArrayBuffer, _skyboxBufferObject);
        //GL.BufferData(BufferTarget.ArrayBuffer, skyboxVertices.Length * sizeof(float), skyboxVertices, BufferUsageHint.StaticDraw);
        //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        //GL.EnableVertexAttribArray(0);

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

       // skybox = new Skybox(new string[] {"resources/skybox/right.jpg", "resources/skybox/left.jpg", "resources/skybox/top.jpg", "resources/skybox/bottom.jpg"
        //    , "resources/skybox/front.jpg", "resources/skybox/back.jpg" });

        CursorState = CursorState.Grabbed;
    }

    public override void OnRender(FrameEventArgs e)
    {
        _time += 4.0 * e.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //TODO cannot get this aligned, code works however, bugs: needs to be fixed view (camera bug), needs to have gl.disable correctly placed

        //GL.Disable(EnableCap.DepthTest);

        GL.BindVertexArray(_vaoModel);

        _diffuseMap.Use(TextureUnit.Texture0);
        _specularMap.Use(TextureUnit.Texture1);

        Matrix4 view = _camera.GetViewMatrix();
        Matrix4 projection = _camera.GetProjectionMatrix();


        //GL.BindTexture(TextureTarget.TextureCubeMap, skyboxTexture);

        BaseLight baseLight = new BaseLight(_camera, _lightingShader);

        /*
           Here we set all the uniforms for the 5/6 types of lights we have. We have to set them manually and index
           the proper PointLight struct in the array to set each uniform variable. This can be done more code-friendly
           by defining light types as classes and set their values in there, or by using a more efficient uniform approach
           by using 'Uniform buffer objects', but that is something we'll discuss in the 'Advanced GLSL' tutorial.
        */

        // Directional light
        DirectionalLight directionalLight = new DirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f));

        // Point lights
        PointLight pointLight = new PointLight(_pointLightPositions);

        // Spot light
        SpotLight spotLight = new SpotLight(_camera.Position, _camera.Front);

        for (int i = 0; i < _cubePositions.Length; i++)
        {
            Matrix4 model = Matrix4.CreateTranslation(_cubePositions[i]);
            float angle = 20.0f * i;
            model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
            _lightingShader.SetMatrix4("model", ref model);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        

        GL.BindVertexArray(_vaoLamp);

        

        _lampShader.Use();

        _lampShader.SetMatrix4("view", ref view);
        _lampShader.SetMatrix4("projection", ref projection);
        // We use a loop to draw all the lights at the proper position
        for (int i = 0; i < _pointLightPositions.Length; i++)
        {
            Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_pointLightPositions[i]);

            _lampShader.SetMatrix4("model", ref lampMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        //GL.DepthFunc(DepthFunction.Lequal);
        //;Matrix4 view = _camera.GetViewMatrix();
        //;Matrix4 projection = _camera.GetProjectionMatrix();
        //skybox.Draw(view, projection);
        //GL.DepthFunc(DepthFunction.Less);

        SwapBuffers();
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
}
