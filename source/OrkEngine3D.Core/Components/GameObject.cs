using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core.Components
{
    public class GameObject
    {
        public Transform Transform { get; set; }
        public GameObject Parent { get; set; }
        public List<GameObject> Children { get; set; }

        public GameObject()
        {
            Transform = new Transform();
            Children = new List<GameObject>();
        }

        public void AddChild(GameObject child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(GameObject child)
        {
            if (Children.Contains(child))
            {
                child.Parent = null;
                Children.Remove(child);
            }
        }
    }
}
