namespace OrkEngine3D.Graphics.TK.Resources
{
    /// <summary>
    /// GLResource is a resource wich contains non-managed OpenGL resources, such as shaders, buffers etc
    /// </summary>
    public abstract class GLResource
    {
        public GLResource(GLResourceManager manager){
            manager.resources.Add(this);
        }
        public abstract void Unload();
    }
}