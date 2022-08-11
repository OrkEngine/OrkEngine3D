using OrkEngine3D.Core.Components;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Core.Assets
{
    public class SerializedTransform
    {
        public Vector3 LocalPosition { get; set; }
        public Quaternion LocalRotation { get; set; }
        public Vector3 LocalScale { get; set; }
        public ulong ParentID { get; set; }

        public SerializedTransform()
        {
            LocalScale = Vector3.One;
            LocalRotation = Quaternion.Identity;
        }

        public SerializedTransform(Transform transform)
        {
            LocalPosition = transform.GetLocalOrPhysicsEntityPosition();
            LocalRotation = transform.GetLocalOrPhysicsEntityRotation();
            LocalScale = transform.LocalScale;

            if (transform.Parent != null)
            {
                ParentID = transform.Parent.GameObject.ID;
            }
        }
    }
}
