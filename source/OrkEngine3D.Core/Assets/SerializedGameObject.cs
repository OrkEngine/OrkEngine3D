using OrkEngine3D.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Core.Assets
{
    public class SerializedGameObject
    {
        private static HashSet<Type> s_excludedComponents = new HashSet<Type>()
        {
            typeof(Transform)
        };

        public string Name { get; set; }
        public SerializedTransform Transform { get; set; }
        public Component[] Components { get; set; }
        public ulong ID { get; set; }

        public SerializedGameObject()
        {
        }

        public SerializedGameObject(GameObject go)
        {
            Name = go.Name;
            Transform = new SerializedTransform(go.Transform);
            Components = go.GetComponents<Component>().Where(c => !s_excludedComponents.Contains(c.GetType())).ToArray();
            ID = go.ID;
        }
    }
}
