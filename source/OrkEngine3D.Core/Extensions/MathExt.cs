using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core.Extensions
{
    //TODO: port these to OrkEngine3D.Mathematics some day
    public static class MathExt
    {
        /* TODO: Important, orkmath doesnt contain Matrix2
        public static OpenTK.Mathematics.Matrix2 OrkToTkMat3(this Matrix2 matrix2)
        {
            return new OpenTK.Mathematics.Matrix2(OrkToTKVec3(matrix2.Row0), OrkToTKVec3(matrix2.Row1));
        }
        */

        public static OpenTK.Mathematics.Matrix3 OrkToTkMat3(this Matrix3 matrix3)
        {
            return new OpenTK.Mathematics.Matrix3(OrkToTKVec3(matrix3.Row0), OrkToTKVec3(matrix3.Row1), OrkToTKVec3(matrix3.Row2));
        }

        public static OpenTK.Mathematics.Matrix4 OrkToTKMat4(this Matrix4 matrix4)
        {
            return new OpenTK.Mathematics.Matrix4(OrkToTKVec4(matrix4.Row0), OrkToTKVec4(matrix4.Row1), OrkToTKVec4(matrix4.Row2), OrkToTKVec4(matrix4.Row3));
        }

        public static OpenTK.Mathematics.Vector2 OrkToTKVec2(this Vector2 vector2)
        {
            return new OpenTK.Mathematics.Vector2(vector2.X, vector2.Y);
        }

        public static OpenTK.Mathematics.Vector3 OrkToTKVec3(this Vector3 vector3)
        {
            return new OpenTK.Mathematics.Vector3(vector3.X, vector3.Y, vector3.Z);
        }

        public static OpenTK.Mathematics.Vector4 OrkToTKVec4(this Vector4 vector4)
        {
            return new OpenTK.Mathematics.Vector4(vector4.X, vector4.Y, vector4.Z, vector4.W);
        }
    }
}
