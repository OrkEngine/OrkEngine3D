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
using OrkEngine3D.Core.Components;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Physics;

public class BoxCollider : Collider
{
    [JsonProperty]
    private float _width;
    [JsonProperty]
    private float _height;
    [JsonProperty]
    private float _depth;

    public float Width
    {
        get { return _width; }
        set
        {
            _width = value;
            DimensionsChanged();

        }
    }

    public float Height
    {
        get { return _height; }
        set
        {
            _height = value;
            DimensionsChanged();

        }
    }

    public float Depth
    {
        get { return _depth; }
        set
        {
            _depth = value;
            DimensionsChanged();
        }
    }

    public BoxCollider() : this(1.0f, 1.0f, 1.0f, 1.0f) { }

    [JsonConstructor]
    public BoxCollider(float width, float height, float depth)
        : this(width, height, depth, 1.0f * (width * height * depth)) { }

    public BoxCollider(float width, float height, float depth, float mass)
        : base(mass)
    {
        _width = width;
        _height = height;
        _depth = depth;
    }

    protected override void PostAttached(SystemRegistry registry)
    {
        SetEntity(CreateEntity());
    }

    protected override Entity CreateEntity()
    {
        Vector3 scale = GameObject.Transform.Scale;
        return new Box(Vector3.Zero, _width * scale.X, _height * scale.Y, _depth * scale.Z, Mass);
    }

    protected override void ScaleChanged(Vector3 scale)
    {
        Box box = (Box)Entity;
        box.Width = _width * scale.X;
        box.Height = _height * scale.Y;
        box.Length = _depth * scale.Z;
    }

    private void DimensionsChanged()
    {
        if (Entity != null)
        {
            Box box = (Box)Entity;
            Vector3 scale = Transform.Scale;
            box.Width = _width * scale.X;
            box.Height = _height * scale.Y;
            box.Length = _depth * scale.Z;
        }
    }
}
