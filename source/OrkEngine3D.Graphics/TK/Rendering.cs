using OpenTK.Graphics.OpenGL4;
using OrkEngine3D.Core.Components;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearBufferMask = OpenTK.Graphics.OpenGL4.ClearBufferMask;
using GL = OpenTK.Graphics.OpenGL4.GL;
using ShaderType = OrkEngine3D.Graphics.TK.Resources.ShaderType;

namespace OrkEngine3D.Graphics.TK
{
    /*
    public static class Rendering
    {
        public static Camera currentCamera { get; private set; }
        public static GContext currentContext { get; private set; }
        public static Transform currentTransform { get; private set; }
        //public static IRenderTarget renderTarget { get; private set; } = WindowTarget.Global;
        public static LightScene currentLightning { get; private set; }
        public static Material[] currentMaterials { get; private set; } = new Material[] { new Material() };
        public static IRenderable currentRenderObject { get; private set; }
        public static GLResourceManager currentResourceManager { get; private set; }
        //public static ShadowHandler shadowHandler { get; private set; }
        public static bool isWireframe { get; private set; }

        //private static readonly GLTarget GlTarget = new GLTarget();
        //public static bool inShadowMode { get; private set; }

        public static void BindCamera(Camera camera)
        {
            currentCamera = camera;
        }

        public static void BindContext(GContext ctx)
        {
            currentContext = ctx;
        }

        public static void BindLightning(LightScene light)
        {
            currentLightning = light;
        }

        public static void BindMaterial(Material material)
        {
            currentMaterials = new[] { material };
        }

        public static void BindMaterials(params Material[] materials)
        {
            currentMaterials = materials;
        }

        public static void BindTransform(Transform t)
        {
            currentTransform = t;
        }

        /*
        public static void BindTarget(ID id)
        {
            renderTarget = (IRenderTarget)currentResourceManager.GetResource(id);
        }

        public static void ResetTarget()
        {
            renderTarget = GlTarget;
            GlTarget.BindTarget();
        }

        public static void ClearTarget()
        {
            renderTarget.Clear();
        }
        *

        public static void SwapBuffers()
        {
            currentContext.SwapBuffers();
        }

        public static void Render(){
            currentRenderObject.Render();
        }

        public static void BindRenderable(ID id){
            currentRenderObject = (IRenderable)currentResourceManager.GetResource(id);
        }

        public static void BindResourceManager(GLResourceManager manager){
            currentResourceManager = manager;
        }

        public static ID CreateMesh(){
            Mesh mesh = new Mesh(currentResourceManager);
            return mesh.resourceid;
        }

        public static ID UpdateMeshVerticies(ID id, Vector3[] verticies){
            currentResourceManager.GetResource<Mesh>(id).verticies = verticies;
            return id;
        }

        public static ID UpdateMeshTriangles(ID id, uint[] triangles){
            currentResourceManager.GetResource<Mesh>(id).triangles = triangles;
            return id;
        }

        public static ID UpdateMeshUVs(ID id, Vector2[] uv){
            currentResourceManager.GetResource<Mesh>(id).uv = uv;
            return id;
        }

        public static ID UpdateMeshNormals(ID id, Vector3[] normals){
            currentResourceManager.GetResource<Mesh>(id).normals = normals;
            return id;
        }

        public static ID UpdateMeshMaterials(ID id, int[] materials){
            currentResourceManager.GetResource<Mesh>(id).materials = materials;
            return id;
        }

        /*
        public static ID UpdateMeshShader(ID id, ID shader){
            currentResourceManager.GetResource<Mesh>(id).shader = currentResourceManager.GetResource<ShaderProgram>(shader);
            return id;
        }
        *

        public static void UpdateMeshGLData(ID id){
            currentResourceManager.GetResource<Mesh>(id).UpdateGLData();
        }

        /*
        public static ID CreateTexture(TextureData data){
            Texture texture = new Texture(currentResourceManager, data);

            return texture.resourceid;
        }
        */

        /*
        public static ID CreateShaderProgram(params ID[] shaders){
            Shader[] s = new Shader[shaders.Length];
            for(int i = 0; i < s.Length; i++) s[i] = currentResourceManager.GetResource<Shader>(shaders[i]); 
            ShaderProgram shaderProgram = new ShaderProgram(currentResourceManager, s);
            return shaderProgram.resourceid;
        }

        public static ID CreateShader(string source, ShaderType type){
            Shader s = new Shader(currentResourceManager, source, type);
            return s.resourceid;
        }
        

        public static ID CreateRenderBuffer(int width, int height){
            RenderBuffer renderBuffer = new RenderBuffer(currentResourceManager, width, height);
            return renderBuffer.resourceid;
        }
        *
        public static void EnableWireframe()
        {
            isWireframe = true;
        }
        
        public static void DisableWireframe()
        {
            isWireframe = false;
        }
        /*
        public static ID CreateShadowManager()
        {
            ShadowHandler shadowHandler = new ShadowHandler(currentResourceManager);
            return shadowHandler.resourceid;
        }

        public static void BindShadowManager(ID id)
        {
            shadowHandler = currentResourceManager.GetResource<ShadowHandler>(id);
        }
       
        public static void EnterShadowMode()
        {
            shadowHandler.BindTarget();
            inShadowMode = true;
        }

        public static void ExitShadowMode()
        {
            inShadowMode = false;
        }
        *

        internal class GLTarget : IRenderTarget
        {
            public void BindTarget()
            {
                currentContext.ResetFrameBuffer();
            }

            public void Clear()
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            }

            public void PreRender()
            {
                
            }
        }
    }
    */
}
