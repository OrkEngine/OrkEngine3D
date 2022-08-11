/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Newtonsoft.Json;
using OrkEngine3D.Core.Components;
using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Core.Assets;

public class SerializedPrefab
{
    public SerializedGameObject[] GameObjects { get; set; }

    public SerializedPrefab(IEnumerable<GameObject> gameObjects)
    {
        GameObjects = gameObjects.Select(go => new SerializedGameObject(go)).ToArray();
    }

    [JsonConstructor]
    internal SerializedPrefab(SerializedGameObject[] gameObjects)
    {
        GameObjects = gameObjects;
    }

    public GameObject Instantiate(GameObjectQuerySystem goqs)
    {
        Dictionary<ulong, GameObject> prefabIDToGO = new Dictionary<ulong, GameObject>();

        foreach (SerializedGameObject sgo in GameObjects)
        {
            GameObject go = new GameObject(sgo.Name);
            go.Transform.LocalPosition = sgo.Transform.LocalPosition;
            go.Transform.LocalRotation = sgo.Transform.LocalRotation;
            go.Transform.LocalScale = sgo.Transform.LocalScale;

            foreach (var component in sgo.Components)
            {
                go.AddComponent(component);
            }

            prefabIDToGO.Add(sgo.ID, go);
        }

        foreach (SerializedGameObject sgo in GameObjects)
        {
            GameObject go = prefabIDToGO[sgo.ID];
            ulong parentOriginalID = sgo.Transform.ParentID;
            if (parentOriginalID != 0)
            {
                go.Transform.Parent = prefabIDToGO[parentOriginalID].Transform;
            }
        }

        return prefabIDToGO[GameObjects.First().ID];
    }
}
