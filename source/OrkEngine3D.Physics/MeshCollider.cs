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

namespace OrkEngine3D.Physics;
/*I'll fix this guy later*/
/*
public class MeshCollider : Collider
{
    private AssetSystem _as;

    private RefOrImmediate<MeshData> _mesh;
    public RefOrImmediate<MeshData> Mesh
    {
        get
        {
            return _mesh;
        }
        set
        {
            _mesh = value;
            if (Entity != null)
            {
                MeshChanged();
            }
        }
    }

    public MeshCollider() : base(1.0f) { }

    public MeshCollider(float mass) : base(mass)
    {
    }

    private void MeshChanged()
    {
        SetEntity(CreateEntity());
    }

    protected override void PostAttached(SystemRegistry registry)
    {
        _as = registry.GetSystem<AssetSystem>();
        if (_mesh.GetRef() != null)
        {
            SetEntity(CreateEntity());
        }
    }

    protected override void OnEnabled()
    {
        base.OnEnabled();
        if (Entity == null && Mesh.GetRef() != null)
        {
            SetEntity(CreateEntity());
        }
    }

    protected override Entity CreateEntity()
    {
        MeshData meshData = Mesh.Get(_as.Database);
        Vector3[] positions = meshData.GetVertexPositions().Select(v => v * Transform.Scale).ToArray();
        int[] indices = meshData.GetIndices().Select(u16 => checked((int)u16)).ToArray();
        Vector3 center;
        MobileMeshShape mms = new MobileMeshShape(
            positions,
            indices,
            AffineTransform.Identity,
            MobileMeshSolidity.Solid,
            out center);

        foreach (var meshRenderer in GameObject.GetComponents<MeshRenderer>())
        {
            meshRenderer.RenderOffset = Matrix4x4.CreateTranslation(-center / Transform.Scale);
        }

        return new Entity(mms, Mass);
    }

    protected override void ScaleChanged(Vector3 scale)
    {
        if (Entity != null)
        {
            SetEntity(CreateEntity());
        }
    }
}*/
