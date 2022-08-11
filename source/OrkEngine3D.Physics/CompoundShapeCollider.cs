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

using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using Newtonsoft.Json;
using OrkEngine3D.Core;
using OrkEngine3D.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Physics;

public class CompoundShapeCollider : Collider
{
    [JsonProperty]
    private readonly IList<BoxShapeDescription> _shapes;

    public Vector3 EntityCenter { get; private set; }

    [JsonConstructor]
    public CompoundShapeCollider(IList<BoxShapeDescription> shapes, float mass)
        : base(mass)
    {
        _shapes = shapes;
    }

    protected override void PostAttached(SystemRegistry registry)
    {
        SetEntity(CreateEntity());
    }

    protected override Entity CreateEntity()
    {
        CompoundBody cb = new CompoundBody(
            _shapes.Select(bse => BoxShapeDescription.Scale(bse, Transform.Scale).GetShapeEntry()).ToList(), Mass);
        EntityCenter = -cb.Position;
        return cb;
    }

    protected override void ScaleChanged(Vector3 scale)
    {
        SetEntity(CreateEntity());
    }
}

public abstract class ShapeDescription
{
    public Vector3 Position { get; set; }
    public Quaternion Orientation { get; set; }

    public ShapeDescription(Vector3 position, Quaternion orientation)
    {
        Position = position;
        Orientation = orientation;
    }

    public CompoundShapeEntry GetShapeEntry()
    {
        return new CompoundShapeEntry(GetEnityShape(), new BEPUutilities.RigidTransform(Position, Orientation));
    }

    public abstract EntityShape GetEnityShape();
}

public class BoxShapeDescription : ShapeDescription
{
    public Vector3 Dimensions { get; set; }

    public BoxShapeDescription(Vector3 dimensions, Vector3 position)
        : this(dimensions, position, Quaternion.Identity) { }

    [JsonConstructor]
    public BoxShapeDescription(Vector3 dimensions, Vector3 position, Quaternion orientation)
        : base(position, orientation)
    {
        Dimensions = dimensions;
        Position = position;
        Orientation = orientation;
    }

    public override EntityShape GetEnityShape()
    {
        return new BoxShape(Dimensions.X, Dimensions.Y, Dimensions.Z);
    }

    public static BoxShapeDescription Scale(BoxShapeDescription bse, Vector3 scale)
    {
        return new BoxShapeDescription(bse.Dimensions * scale, bse.Position * scale, bse.Orientation);
    }
}

public class SphereShapeDescription : ShapeDescription
{
    public float Radius { get; private set; }

    public SphereShapeDescription(float radius, Vector3 position, Quaternion orientation) : base(position, orientation)
    {
        Radius = radius;
    }

    public override EntityShape GetEnityShape()
    {
        return new SphereShape(Radius);
    }
}
