using System.Collections.Generic;

namespace OrkEngine3D.Graphics.Resources
{
    public class GLResourceManager
    {
        public List<GLResource> resources = new List<GLResource>();

        public void Unload(){
            for (var i = 0; i < resources.Count; i++)
            {
                resources[i].Unload();
            }
            resources.Clear();
        }
    }
}