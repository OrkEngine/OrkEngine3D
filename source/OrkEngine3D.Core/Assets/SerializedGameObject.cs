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

using OrkEngine3D.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Core.Assets;

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
