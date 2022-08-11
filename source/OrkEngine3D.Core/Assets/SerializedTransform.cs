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
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Core.Assets;

public class SerializedTransform
{
    public Vector3 LocalPosition { get; set; }
    public Quaternion LocalRotation { get; set; }
    public Vector3 LocalScale { get; set; }
    public ulong ParentID { get; set; }

    public SerializedTransform()
    {
        LocalScale = Vector3.One;
        LocalRotation = Quaternion.Identity;
    }

    public SerializedTransform(Transform transform)
    {
        LocalPosition = transform.GetLocalOrPhysicsEntityPosition();
        LocalRotation = transform.GetLocalOrPhysicsEntityRotation();
        LocalScale = transform.LocalScale;

        if (transform.Parent != null)
        {
            ParentID = transform.Parent.GameObject.ID;
        }
    }
}
