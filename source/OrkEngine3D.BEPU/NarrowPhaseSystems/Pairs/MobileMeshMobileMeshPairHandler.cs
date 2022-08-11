﻿using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUutilities.ResourceManagement;
using BEPUutilities;

namespace BEPUphysics.NarrowPhaseSystems.Pairs
{
    ///<summary>
    /// Handles a mobile mesh-mobile mesh collision pair.
    ///</summary>
    public class MobileMeshMobileMeshPairHandler : MobileMeshMeshPairHandler
    {


        MobileMeshCollidable mesh;

        public override Collidable CollidableB
        {
            get { return mesh; }
        }
        public override Entities.Entity EntityB
        {
            get { return mesh.entity; }
        }
        protected override Materials.Material MaterialB
        {
            get { return mesh.entity.material; }
        }

        protected override TriangleCollidable GetOpposingCollidable(int index)
        {
            //Construct a TriangleCollidable from the static mesh.
            var toReturn = PhysicsResources.GetTriangleCollidable();
            toReturn.Shape.sidedness = mesh.Shape.Sidedness;
            toReturn.Shape.collisionMargin = mobileMesh.Shape.MeshCollisionMargin;
            toReturn.Entity = mesh.entity;
            return toReturn;
        }

        protected override void CleanUpCollidable(TriangleCollidable collidable)
        {
            collidable.Entity = null;
            base.CleanUpCollidable(collidable);
        }

        protected override void ConfigureCollidable(TriangleEntry entry, float dt)
        {
            var shape = entry.Collidable.Shape;
            mesh.Shape.TriangleMesh.Data.GetTriangle(entry.Index, out shape.vA, out shape.vB, out shape.vC);
            Matrix3x3 o;
            Matrix3x3.CreateFromQuaternion(ref mesh.worldTransform.Orientation, out o);
            Matrix3x3.Transform(ref shape.vA, ref o, out shape.vA);
            Matrix3x3.Transform(ref shape.vB, ref o, out shape.vB);
            Matrix3x3.Transform(ref shape.vC, ref o, out shape.vC);
            OrkEngine3D.Mathematics.Vector3 center;
            Vector3Ex.Add(ref shape.vA, ref shape.vB, out center);
            Vector3Ex.Add(ref center, ref shape.vC, out center);
            Vector3Ex.Multiply(ref center, 1 / 3f, out center);
            Vector3Ex.Subtract(ref shape.vA, ref center, out shape.vA);
            Vector3Ex.Subtract(ref shape.vB, ref center, out shape.vB);
            Vector3Ex.Subtract(ref shape.vC, ref center, out shape.vC);

            Vector3Ex.Add(ref center, ref mesh.worldTransform.Position, out center);
            //The bounding box doesn't update by itself.
            entry.Collidable.worldTransform.Position = center;
            entry.Collidable.worldTransform.Orientation = OrkEngine3D.Mathematics.Quaternion.Identity;
            entry.Collidable.UpdateBoundingBoxInternal(dt);
        }

        ///<summary>
        /// Initializes the pair handler.
        ///</summary>
        ///<param name="entryA">First entry in the pair.</param>
        ///<param name="entryB">Second entry in the pair.</param>
        public override void Initialize(BroadPhaseEntry entryA, BroadPhaseEntry entryB)
        {
            mesh = (MobileMeshCollidable)entryB;

            base.Initialize(entryA, entryB);
        }






        ///<summary>
        /// Cleans up the pair handler.
        ///</summary>
        public override void CleanUp()
        {

            base.CleanUp();
            mesh = null;


        }




        protected override void UpdateContainedPairs(float dt)
        {
            var overlappedElements = CommonResources.GetIntList();
            BoundingBox localBoundingBox;
            AffineTransform meshTransform;
            AffineTransform.CreateFromRigidTransform(ref mesh.worldTransform, out meshTransform);

            OrkEngine3D.Mathematics.Vector3 sweep;
            Vector3Ex.Subtract(ref mobileMesh.entity.linearVelocity, ref mesh.entity.linearVelocity, out sweep);
            Vector3Ex.Multiply(ref sweep, dt, out sweep);
            mobileMesh.Shape.GetSweptLocalBoundingBox(ref mobileMesh.worldTransform, ref meshTransform, ref sweep, out localBoundingBox);
            mesh.Shape.TriangleMesh.Tree.GetOverlaps(localBoundingBox, overlappedElements);
            for (int i = 0; i < overlappedElements.Count; i++)
            {
                TryToAdd(overlappedElements.Elements[i]);
            }

            CommonResources.GiveBack(overlappedElements);

        }


    }
}
