namespace OrkEngine3D.Graphics.TK.Resources;

/// <summary>
/// GLResource is a resource wich contains non-managed OpenGL resources, such as shaders, buffers etc
/// </summary>
public abstract class GLResource
{
    public ID resourceid;
    public GLResource(GLResourceManager manager){
        this.resourceid = new ID();
        if(manager != null)
            manager.resources.Add(resourceid, this);
    }
    public abstract void Unload();
}