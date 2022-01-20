using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Components.Core
{
    /// <summary>
    /// The transformation of an object
    /// </summary>
    public class Transform : Component
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
        public Vector3 eulers {
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
        public Matrix GetMatrix(){
            return Matrix.Transformation(Vector3.Zero, Quaternion.Identity, scale, Vector3.Zero, rotation, position);
        }

        /// <summary>
        /// Rotate the object
        /// </summary>
        /// <param name="eulers">The rotation to rotate the object with, in euler angles</param>
        public void Rotate(Vector3 eulers){
            rotation = Quaternion.Multiply(rotation, Quaternion.RotationYawPitchRoll(eulers.Y, eulers.Z, eulers.X));
        }
    }
}