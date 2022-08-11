using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core
{
    public static class VectorExtensions
    {
        public static Vector3 XYZ(this Vector4 v) => new Vector3(v.X, v.Y, v.Z);
        public static Vector2 XY(this Vector4 v) => new Vector2(v.X, v.Y);
        public static Vector2 XY(this Vector3 v) => new Vector2(v.X, v.Y);
    }
}
