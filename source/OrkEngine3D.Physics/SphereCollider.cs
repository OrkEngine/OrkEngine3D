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

using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using Newtonsoft.Json;
using OrkEngine3D.Core;
using OrkEngine3D.Mathematics;
using System;
using System.Diagnostics;

namespace OrkEngine3D.Physics;

public class SphereCollider : Collider
{
    [JsonProperty]
    private float _radius;

    public float Radius
    {
        get { return _radius; }
        set
        {
            _radius = value;
            Debug.Assert(Entity != null);
            ((Sphere)Entity).Radius = value; // Will Entity always be non-null?
        }
    }

    public SphereCollider() : this(1.0f) { }

    public SphereCollider(float radius) : this(radius, (4f / 3f) * (float)Math.PI * radius * radius * radius)
    { }

    [JsonConstructor]
    public SphereCollider(float radius, float mass)
        : base(mass)
    {
        _radius = radius;
    }

    protected override void PostAttached(SystemRegistry registry)
    {
        SetEntity(CreateEntity());
    }

    protected override Entity CreateEntity()
    {
        Vector3 scale = GameObject.Transform.Scale;
        return new Sphere(Vector3.Zero, scale.X * _radius, Mass);
    }

    protected override void ScaleChanged(Vector3 scale)
    {
        Sphere sphere = (Sphere)Entity;
        sphere.Radius = _radius * scale.X;
    }
}
