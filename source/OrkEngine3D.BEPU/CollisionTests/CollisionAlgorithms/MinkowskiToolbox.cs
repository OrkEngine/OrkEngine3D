using System;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUutilities;
 

namespace BEPUphysics.CollisionTests.CollisionAlgorithms
{
    ///<summary>
    /// Helper class that supports other systems using minkowski space operations.
    ///</summary>
    public static class MinkowskiToolbox
    {
        ///<summary>
        /// Gets the local transform of B in the space of A.
        ///</summary>
        ///<param name="transformA">First transform.</param>
        ///<param name="transformB">Second transform.</param>
        ///<param name="localTransformB">Transform of B in the local space of A.</param>
        public static void GetLocalTransform(ref RigidTransform transformA, ref RigidTransform transformB,
                                             out RigidTransform localTransformB)
        {
            //Put B into A's space.
            OrkEngine3D.Mathematics.Quaternion conjugateOrientationA;
            QuaternionEx.Conjugate(ref transformA.Orientation, out conjugateOrientationA);
            QuaternionEx.Concatenate(ref transformB.Orientation, ref conjugateOrientationA, out localTransformB.Orientation);
            Vector3Ex.Subtract(ref transformB.Position, ref transformA.Position, out localTransformB.Position);
            QuaternionEx.Transform(ref localTransformB.Position, ref conjugateOrientationA, out localTransformB.Position);
        }

        ///<summary>
        /// Gets the extreme point of the minkowski difference of shapeA and shapeB in the local space of shapeA.
        ///</summary>
        ///<param name="shapeA">First shape.</param>
        ///<param name="shapeB">Second shape.</param>
        ///<param name="direction">Extreme point direction in local space.</param>
        ///<param name="localTransformB">Transform of shapeB in the local space of A.</param>
        ///<param name="extremePoint">The extreme point in the local space of A.</param>
        public static void GetLocalMinkowskiExtremePoint(ConvexShape shapeA, ConvexShape shapeB, ref OrkEngine3D.Mathematics.Vector3 direction, ref RigidTransform localTransformB, out OrkEngine3D.Mathematics.Vector3 extremePoint)
        {
            //Extreme point of A-B along D = (extreme point of A along D) - (extreme point of B along -D)
            shapeA.GetLocalExtremePointWithoutMargin(ref direction, out extremePoint);
            OrkEngine3D.Mathematics.Vector3 v;
            OrkEngine3D.Mathematics.Vector3 negativeN;
            Vector3Ex.Negate(ref direction, out negativeN);
            shapeB.GetExtremePointWithoutMargin(negativeN, ref localTransformB, out v);
            Vector3Ex.Subtract(ref extremePoint, ref v, out extremePoint);

            ExpandMinkowskiSum(shapeA.collisionMargin, shapeB.collisionMargin, ref direction, out v);
            Vector3Ex.Add(ref extremePoint, ref v, out extremePoint);


        }

        ///<summary>
        /// Gets the extreme point of the minkowski difference of shapeA and shapeB in the local space of shapeA.
        ///</summary>
        ///<param name="shapeA">First shape.</param>
        ///<param name="shapeB">Second shape.</param>
        ///<param name="direction">Extreme point direction in local space.</param>
        ///<param name="localTransformB">Transform of shapeB in the local space of A.</param>
        /// <param name="extremePointA">The extreme point on shapeA.</param>
        ///<param name="extremePoint">The extreme point in the local space of A.</param>
        public static void GetLocalMinkowskiExtremePoint(ConvexShape shapeA, ConvexShape shapeB, ref OrkEngine3D.Mathematics.Vector3 direction, ref RigidTransform localTransformB,
                                                 out OrkEngine3D.Mathematics.Vector3 extremePointA, out OrkEngine3D.Mathematics.Vector3 extremePoint)
        {
            //Extreme point of A-B along D = (extreme point of A along D) - (extreme point of B along -D)
            shapeA.GetLocalExtremePointWithoutMargin(ref direction, out extremePointA);
            OrkEngine3D.Mathematics.Vector3 v;
            Vector3Ex.Negate(ref direction, out v);
            OrkEngine3D.Mathematics.Vector3 extremePointB;
            shapeB.GetExtremePointWithoutMargin(v, ref localTransformB, out extremePointB);

            ExpandMinkowskiSum(shapeA.collisionMargin, shapeB.collisionMargin, direction, ref extremePointA, ref extremePointB);
            Vector3Ex.Subtract(ref extremePointA, ref extremePointB, out extremePoint);


        }

        ///<summary>
        /// Gets the extreme point of the minkowski difference of shapeA and shapeB in the local space of shapeA.
        ///</summary>
        ///<param name="shapeA">First shape.</param>
        ///<param name="shapeB">Second shape.</param>
        ///<param name="direction">Extreme point direction in local space.</param>
        ///<param name="localTransformB">Transform of shapeB in the local space of A.</param>
        /// <param name="extremePointA">The extreme point on shapeA.</param>
        /// <param name="extremePointB">The extreme point on shapeB.</param>
        ///<param name="extremePoint">The extreme point in the local space of A.</param>
        public static void GetLocalMinkowskiExtremePoint(ConvexShape shapeA, ConvexShape shapeB, ref OrkEngine3D.Mathematics.Vector3 direction, ref RigidTransform localTransformB,
                                                 out OrkEngine3D.Mathematics.Vector3 extremePointA, out OrkEngine3D.Mathematics.Vector3 extremePointB, out OrkEngine3D.Mathematics.Vector3 extremePoint)
        {
            //Extreme point of A-B along D = (extreme point of A along D) - (extreme point of B along -D)
            shapeA.GetLocalExtremePointWithoutMargin(ref direction, out extremePointA);
            OrkEngine3D.Mathematics.Vector3 v;
            Vector3Ex.Negate(ref direction, out v);
            shapeB.GetExtremePointWithoutMargin(v, ref localTransformB, out extremePointB);

            ExpandMinkowskiSum(shapeA.collisionMargin, shapeB.collisionMargin, direction, ref extremePointA, ref extremePointB);
            Vector3Ex.Subtract(ref extremePointA, ref extremePointB, out extremePoint);


        }

        ///<summary>
        /// Gets the extreme point of the minkowski difference of shapeA and shapeB in the local space of shapeA, without a margin.
        ///</summary>
        ///<param name="shapeA">First shape.</param>
        ///<param name="shapeB">Second shape.</param>
        ///<param name="direction">Extreme point direction in local space.</param>
        ///<param name="localTransformB">Transform of shapeB in the local space of A.</param>
        ///<param name="extremePoint">The extreme point in the local space of A.</param>
        public static void GetLocalMinkowskiExtremePointWithoutMargin(ConvexShape shapeA, ConvexShape shapeB, ref OrkEngine3D.Mathematics.Vector3 direction, ref RigidTransform localTransformB, out OrkEngine3D.Mathematics.Vector3 extremePoint)
        {
            //Extreme point of A-B along D = (extreme point of A along D) - (extreme point of B along -D)
            shapeA.GetLocalExtremePointWithoutMargin(ref direction, out extremePoint);
            OrkEngine3D.Mathematics.Vector3 extremePointB;
            OrkEngine3D.Mathematics.Vector3 negativeN;
            Vector3Ex.Negate(ref direction, out negativeN);
            shapeB.GetExtremePointWithoutMargin(negativeN, ref localTransformB, out extremePointB);
            Vector3Ex.Subtract(ref extremePoint, ref extremePointB, out extremePoint);

        }



        ///<summary>
        /// Computes the expansion of the minkowski sum due to margins in a given direction.
        ///</summary>
        ///<param name="marginA">First margin.</param>
        ///<param name="marginB">Second margin.</param>
        ///<param name="direction">Extreme point direction.</param>
        ///<param name="contribution">Margin contribution to the extreme point.</param>
        public static void ExpandMinkowskiSum(float marginA, float marginB, ref OrkEngine3D.Mathematics.Vector3 direction, out OrkEngine3D.Mathematics.Vector3 contribution)
        {
            float lengthSquared = direction.LengthSquared();
            if (lengthSquared > Toolbox.Epsilon)
            {
                //The contribution to the minkowski sum by the margin is:
                //direction * marginA - (-direction) * marginB.
                Vector3Ex.Multiply(ref direction, (marginA + marginB) / (float)Math.Sqrt(lengthSquared), out contribution);

            }
            else
            {
                contribution = new OrkEngine3D.Mathematics.Vector3();
            }


        }


        ///<summary>
        /// Computes the expansion of the minkowski sum due to margins in a given direction.
        ///</summary>
        ///<param name="marginA">First margin.</param>
        ///<param name="marginB">Second margin.</param>
        ///<param name="direction">Extreme point direction.</param>
        ///<param name="toExpandA">Margin contribution to the shapeA.</param>
        ///<param name="toExpandB">Margin contribution to the shapeB.</param>
        public static void ExpandMinkowskiSum(float marginA, float marginB, OrkEngine3D.Mathematics.Vector3 direction, ref OrkEngine3D.Mathematics.Vector3 toExpandA, ref OrkEngine3D.Mathematics.Vector3 toExpandB)
        {
            float lengthSquared = direction.LengthSquared();
            if (lengthSquared > Toolbox.Epsilon)
            {
                lengthSquared = 1 / (float)Math.Sqrt(lengthSquared);   
                //The contribution to the minkowski sum by the margin is:
                //direction * marginA - (-direction) * marginB. 
                OrkEngine3D.Mathematics.Vector3 contribution;
                Vector3Ex.Multiply(ref direction, marginA * lengthSquared, out contribution);
                Vector3Ex.Add(ref toExpandA, ref contribution, out toExpandA);
                Vector3Ex.Multiply(ref direction, marginB * lengthSquared, out contribution);
                Vector3Ex.Subtract(ref toExpandB, ref contribution, out toExpandB);
            }
            //If the direction is too small, then the expansion values are left unchanged.

        }
    }
}
