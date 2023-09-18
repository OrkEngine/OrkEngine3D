using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Graphics.TK.Resources;

public class GLResourceManager
{
    public Dictionary<ID, GLResource> resources = new Dictionary<ID, GLResource>();

    public void Unload(){
        for (var i = 0; i < resources.Count; i++)
        {
            resources.Values.ToArray()[i].Unload();
        }
        resources.Clear();
    }

    public GLResource GetResource(ID id){
        return resources[id];
    }

    public T GetResource<T>(ID id) where T : GLResource{
        return (T)GetResource(id);
    }
}