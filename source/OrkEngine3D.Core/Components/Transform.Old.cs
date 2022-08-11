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

using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Core.Components;

public partial class Transform
{
    /// <summary>
    /// The objects position
    /// </summary>
    public Vector3 position = Vector3.Zero;
    /// <summary>
    /// The scale of the object
    /// </summary>
    public Vector3 scale = Vector3.One;

    /// <summary>
    /// The rotation of the object
    /// </summary>
    public Quaternion rotation = Quaternion.Identity;

    /// <summary>
    /// The euler angles the object is rotated in.
    /// </summary>
    public Vector3 eulers
    {
        get => Quaternion.ToEulerAngles(rotation);
    }

    /// <summary>
    /// Forward direction, "the local UnitZ"
    /// </summary>
    /// <returns>The normalized forward vector</returns>
    public Vector3 forward => Vector3.Transform(Vector3.UnitZ, rotation);

    /// <summary>
    /// Right direction, "the local UnitX"
    /// </summary>
    /// <returns>The normalized right vector</returns>
    public Vector3 right => Vector3.Transform(Vector3.UnitX, rotation);

    /// <summary>
    /// Up direction, "the local UnitY"
    /// </summary>
    /// <returns>The normalized up vector</returns>
    public Vector3 up => Vector3.Transform(Vector3.UnitY, rotation);

    /// <summary>
    /// Generates the transformation matrix
    /// </summary>
    /// <returns>The generated transformation matrix</returns>
    public Matrix GetMatrix()
    {
        return Matrix.Transformation(Vector3.Zero, Quaternion.Identity, scale, Vector3.Zero, rotation, position);
    }

    /// <summary>
    /// Rotate the object
    /// </summary>
    /// <param name="eulers">The rotation to rotate the object with, in euler angles</param>
    public void Rotate(Vector3 eulers)
    {
        rotation = Quaternion.Multiply(rotation, Quaternion.RotationYawPitchRoll(eulers.Y, eulers.X, eulers.Z));
    }

}
