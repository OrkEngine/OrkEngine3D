using System;

namespace BEPUutilities
{
    /// <summary>
    /// Provides XNA-like 2D vector math.
    /// </summary>
    public static class Vector2Ex
    {
        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="a">First vector to add.</param>
        /// <param name="b">Second vector to add.</param>
        /// <param name="sum">Sum of the two vectors.</param>
        public static void Add(ref OrkEngine3D.Mathematics.Vector2 a, ref OrkEngine3D.Mathematics.Vector2 b, out OrkEngine3D.Mathematics.Vector2 sum)
        {
            sum.X = a.X + b.X;
            sum.Y = a.Y + b.Y;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">Vector to subtract from.</param>
        /// <param name="b">Vector to subtract from the first vector.</param>
        /// <param name="difference">Result of the subtraction.</param>
        public static void Subtract(ref OrkEngine3D.Mathematics.Vector2 a, ref OrkEngine3D.Mathematics.Vector2 b, out OrkEngine3D.Mathematics.Vector2 difference)
        {
            difference.X = a.X - b.X;
            difference.Y = a.Y - b.Y;
        }

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="v">Vector to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled vector.</param>
        public static void Multiply(ref OrkEngine3D.Mathematics.Vector2 v, float scale, out OrkEngine3D.Mathematics.Vector2 result)
        {
            result.X = v.X * scale;
            result.Y = v.Y * scale;
        }

        /// <summary>
        /// Divides a vector's components by some amount.
        /// </summary>
        /// <param name="v">Vector to divide.</param>
        /// <param name="divisor">Value to divide the vector's components.</param>
        /// <param name="result">Result of the division.</param>
        public static void Divide(ref OrkEngine3D.Mathematics.Vector2 v, float divisor, out OrkEngine3D.Mathematics.Vector2 result)
        {
            float inverse = 1 / divisor;
            result.X = v.X * inverse;
            result.Y = v.Y * inverse;
        }

        /// <summary>
        /// Computes the dot product of the two vectors.
        /// </summary>
        /// <param name="a">First vector of the dot product.</param>
        /// <param name="b">Second vector of the dot product.</param>
        /// <param name="dot">Dot product of the two vectors.</param>
        public static void Dot(ref OrkEngine3D.Mathematics.Vector2 a, ref OrkEngine3D.Mathematics.Vector2 b, out float dot)
        {
            dot = a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static OrkEngine3D.Mathematics.Vector2 Zero
        {
            get
            {
                return new OrkEngine3D.Mathematics.Vector2();
            }
        }

        /// <summary>
        /// Gets a vector pointing along the X axis.
        /// </summary>
        public static OrkEngine3D.Mathematics.Vector2 UnitX
        {
            get { return new OrkEngine3D.Mathematics.Vector2 { X = 1 }; }
        }

        /// <summary>
        /// Gets a vector pointing along the Y axis.
        /// </summary>
        public static OrkEngine3D.Mathematics.Vector2 UnitY
        {
            get { return new OrkEngine3D.Mathematics.Vector2 { Y = 1 }; }
        }


        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        /// <param name="v">Vector to normalize.</param>
        /// <returns>Normalized copy of the vector.</returns>
        public static OrkEngine3D.Mathematics.Vector2 Normalize(OrkEngine3D.Mathematics.Vector2 v)
        {
            OrkEngine3D.Mathematics.Vector2 toReturn;
            Normalize(ref v, out toReturn);
            return toReturn;
        }

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        /// <param name="v">Vector to normalize.</param>
        /// <param name="result">Normalized vector.</param>
        public static void Normalize(ref OrkEngine3D.Mathematics.Vector2 v, out OrkEngine3D.Mathematics.Vector2 result)
        {
            float inverse = (float)(1 / System.Math.Sqrt(v.X * v.X + v.Y * v.Y));
            result.X = v.X * inverse;
            result.Y = v.Y * inverse;
        }

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="v">Vector to negate.</param>
        /// <param name="negated">Negated version of the vector.</param>
        public static void Negate(ref OrkEngine3D.Mathematics.Vector2 v, out OrkEngine3D.Mathematics.Vector2 negated)
        {
            negated.X = -v.X;
            negated.Y = -v.Y;
        }

        /// <summary>
        /// Computes the absolute value of the input vector.
        /// </summary>
        /// <param name="v">Vector to take the absolute value of.</param>
        /// <param name="result">Vector with nonnegative elements.</param>
        public static void Abs(ref OrkEngine3D.Mathematics.Vector2 v, out OrkEngine3D.Mathematics.Vector2 result)
        {
            if (v.X < 0)
                result.X = -v.X;
            else
                result.X = v.X;
            if (v.Y < 0)
                result.Y = -v.Y;
            else
                result.Y = v.Y;
        }

        /// <summary>
        /// Computes the absolute value of the input vector.
        /// </summary>
        /// <param name="v">Vector to take the absolute value of.</param>
        /// <returns>Vector with nonnegative elements.</returns>
        public static OrkEngine3D.Mathematics.Vector2 Abs(OrkEngine3D.Mathematics.Vector2 v)
        {
            OrkEngine3D.Mathematics.Vector2 result;
            Abs(ref v, out result);
            return result;
        }

        /// <summary>
        /// Creates a vector from the lesser values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <param name="min">Vector containing the lesser values of each vector.</param>
        public static void Min(ref OrkEngine3D.Mathematics.Vector2 a, ref OrkEngine3D.Mathematics.Vector2 b, out OrkEngine3D.Mathematics.Vector2 min)
        {
            min.X = a.X < b.X ? a.X : b.X;
            min.Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Creates a vector from the lesser values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <returns>Vector containing the lesser values of each vector.</returns>
        public static OrkEngine3D.Mathematics.Vector2 Min(OrkEngine3D.Mathematics.Vector2 a, OrkEngine3D.Mathematics.Vector2 b)
        {
            OrkEngine3D.Mathematics.Vector2 result;
            Min(ref a, ref b, out result);
            return result;
        }


        /// <summary>
        /// Creates a vector from the greater values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <param name="max">Vector containing the greater values of each vector.</param>
        public static void Max(ref OrkEngine3D.Mathematics.Vector2 a, ref OrkEngine3D.Mathematics.Vector2 b, out OrkEngine3D.Mathematics.Vector2 max)
        {
            max.X = a.X > b.X ? a.X : b.X;
            max.Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Creates a vector from the greater values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <returns>Vector containing the greater values of each vector.</returns>
        public static OrkEngine3D.Mathematics.Vector2 Max(OrkEngine3D.Mathematics.Vector2 a, OrkEngine3D.Mathematics.Vector2 b)
        {
            OrkEngine3D.Mathematics.Vector2 result;
            Max(ref a, ref b, out result);
            return result;
        }
    }
}
