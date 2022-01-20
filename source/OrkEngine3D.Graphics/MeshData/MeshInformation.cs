using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.MeshData
{
    /// <summary>
    /// A readonly struct for passing a mesh out of a function.
    /// </summary>
    public readonly struct MeshInformation
    {
        public readonly Vector3[] verticies;
        public readonly Color4[] colors;
        public readonly Vector2[] uv;
        public readonly uint[] triangles;

        public MeshInformation(Vector3[] verticies, Color4[] colors, Vector2[] uv, uint[] triangles)
        {
            this.verticies = verticies;
            this.colors = colors;
            this.uv = uv;
            this.triangles = triangles;
        }
    }
}