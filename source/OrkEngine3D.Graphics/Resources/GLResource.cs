namespace OrkEngine3D.Graphics.Resources
{
    public abstract class GLResource
    {
        public GLResource(GLResourceManager manager){
            manager.resources.Add(this);
        }
        public abstract void Unload();
    }
}