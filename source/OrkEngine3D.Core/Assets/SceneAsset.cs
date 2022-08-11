using OrkEngine3D.Core.Components;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core.Assets
{
    public class SceneAsset
    {
        public string Name { get; set; }

        public SerializedGameObject[] GameObjects { get; set; }

        public void GenerateGameObjects(bool parallel = false)
        {
            ConcurrentDictionary<ulong, GameObject> idToGO = new ConcurrentDictionary<ulong, GameObject>();

            if (parallel)
            {
                Task.WaitAll(GameObjects.Select((sgo) => Task.Run(() =>
                {
                    GameObject go = new GameObject(sgo.Name);
                    go.Transform.LocalPosition = sgo.Transform.LocalPosition;
                    go.Transform.LocalRotation = sgo.Transform.LocalRotation;
                    go.Transform.LocalScale = sgo.Transform.LocalScale;

                    foreach (var component in sgo.Components)
                    {
                        go.AddComponent(component);
                    }

                    if (!idToGO.TryAdd(sgo.ID, go))
                    {
                        throw new InvalidOperationException("Multiple objects with the same ID were detected. ID = " + sgo.ID);
                    }
                })).ToArray());
            }
            else
            {
                foreach (var sgo in GameObjects)
                {
                    GameObject go = new GameObject(sgo.Name);
                    go.Transform.LocalPosition = sgo.Transform.LocalPosition;
                    go.Transform.LocalRotation = sgo.Transform.LocalRotation;
                    go.Transform.LocalScale = sgo.Transform.LocalScale;

                    foreach (var component in sgo.Components)
                    {
                        go.AddComponent(component);
                    }

                    if (!idToGO.TryAdd(sgo.ID, go))
                    {
                        throw new InvalidOperationException("Multiple objects with the same ID were detected. ID = " + sgo.ID);
                    }
                }
            }
            foreach (var sgo in GameObjects)
            {
                ulong parentID = sgo.Transform.ParentID;
                if (parentID != 0)
                {
                    var parent = idToGO[parentID];
                    idToGO[sgo.ID].Transform.Parent = parent.Transform;
                }
            }
        }
    }
}
