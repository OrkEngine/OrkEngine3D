using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Components.Core
{
    public class Transform : Component
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 scale = Vector3.One;
        public Quaternion rotation = Quaternion.Identity;
        public Vector3 eulers {
            get => Quaternion.ToEulerAngles(rotation);
            set => rotation = Quaternion.RotationYawPitchRoll(value.Y, value.Z, value.X);
        }

        public Vector3 forward => Vector3.Transform(Vector3.UnitZ, rotation);
        public Vector3 right => Vector3.Transform(Vector3.UnitX, rotation);
        public Vector3 up => Vector3.Transform(Vector3.UnitY, rotation);

        public Matrix GetMatrix(){
            return Matrix.Transformation(Vector3.Zero, Quaternion.Identity, scale, Vector3.Zero, rotation, position);
        }

        public void Rotate(Vector3 eulers){
            rotation = Quaternion.Multiply(rotation, Quaternion.RotationYawPitchRoll(eulers.Y, eulers.Z, eulers.X));
        }
    }
}