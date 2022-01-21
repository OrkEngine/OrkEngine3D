using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sMathf = System.MathF;
namespace OrkEngine3D.Mathematics
{
    public static class MathF
    {
        public const float E = 2.71828175F;

        public const float PI = 3.14159274F;

        public const float Tau = 6.28318548F;

        public static float Abs(float x) => sMathf.Abs(x);

        public static float Acos(float x) => sMathf.Acos(x);

        public static float Acosh(float x) => sMathf.Acosh(x);

        public static float Asin(float x) => sMathf.Asin(x);

        public static float Asinh(float x) => sMathf.Asinh(x);

        public static float Atan(float x) => sMathf.Atan(x);

        public static float Atan2(float y, float x) => sMathf.Atan2(x, y);

        public static float Atanh(float x) => sMathf.Atanh(x);

        public static float BitDecrement(float x) => sMathf.BitDecrement(x);

        public static float BitIcrement(float x) => sMathf.BitIncrement(x);

        public static float Cbrt(float x) => sMathf.Cbrt(x);

        public static float Ceiling(float x) => sMathf.Ceiling(x);

        public static float CopySign(float x, float y) => sMathf.CopySign(x, y);

        public static float Cos(float x) => sMathf.Cos(x);

        public static float Cosh(float x) => sMathf.Cosh(x);

        public static float Exp(float x) => sMathf.Exp(x);

        public static float Floor(float x) => sMathf.Floor(x);

        public static float FusedMultipleAdd(float x, float y, float z) => sMathf.FusedMultiplyAdd(x, y, z);

        public static float IEEERemainder(float x, float y) => sMathf.IEEERemainder(x, y);

        public static int ILogB(float x) => sMathf.ILogB(x);

        public static float Log(float x) => sMathf.Log(x);

        public static float Log(float x, float y) => sMathf.Log(x, y);

        public static float Log10(float x) => sMathf.Log10(x);

        public static float Log2(float x) => sMathf.Log2(x);

        public static float Max(float x, float y) => sMathf.Max(x, y);

        public static float MaxMagnitude(float x, float y) => sMathf.MaxMagnitude(x, y);

        public static float Min(float x, float y) => sMathf.Min(x, y);

        public static float MinMagnitude(float x, float y) => sMathf.MinMagnitude(x, y);

        public static float Pow(float x, float y) => sMathf.Pow(x, y);

        public static float Round(float x, MidpointRounding mode) => sMathf.Round(x, mode);

        public static float Round(float x, int digits, MidpointRounding mode) => sMathf.Round(x, digits, mode);

        public static float Round(float x) => sMathf.Round(x);

        public static float Round(float x, int digits) => sMathf.Round(x, digits);

        public static float ScaleB(float x, int n) => sMathf.ScaleB(x, n);

        public static int Sign(float x) => sMathf.Sign(x);

        public static float Sin(float x) => sMathf.Sin(x);

        public static float Sinh(float x) => sMathf.Sinh(x);

        public static float Sqrt(float x) => sMathf.Sqrt(x);

        public static float Tan(float x) => sMathf.Tan(x);

        public static float Tanh(float x) => sMathf.Tanh(x);

        public static float Truncate(float x) => sMathf.Truncate(x);
    }
}
