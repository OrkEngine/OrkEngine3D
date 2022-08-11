using BEPUutilities;


namespace BEPUphysics.Paths
{
    /// <summary>
    /// Wrapper around an orientation curve that specifies a specific velocity at which to travel.
    /// </summary>
    public class ConstantAngularSpeedCurve : ConstantSpeedCurve<OrkEngine3D.Mathematics.Quaternion>
    {
        /// <summary>
        /// Constructs a new constant speed curve.
        /// </summary>
        /// <param name="speed">Speed to maintain while traveling around a curve.</param>
        /// <param name="curve">Curve to wrap.</param>
        public ConstantAngularSpeedCurve(float speed, Curve<OrkEngine3D.Mathematics.Quaternion> curve)
            : base(speed, curve)
        {
        }

        /// <summary>
        /// Constructs a new constant speed curve.
        /// </summary>
        /// <param name="speed">Speed to maintain while traveling around a curve.</param>
        /// <param name="curve">Curve to wrap.</param>
        /// <param name="sampleCount">Number of samples to use when constructing the wrapper curve.
        /// More samples increases the accuracy of the speed requirement at the cost of performance.</param>
        public ConstantAngularSpeedCurve(float speed, Curve<OrkEngine3D.Mathematics.Quaternion> curve, int sampleCount)
            : base(speed, curve, sampleCount)
        {
        }

        protected override float GetDistance(OrkEngine3D.Mathematics.Quaternion start, OrkEngine3D.Mathematics.Quaternion end)
        {
            QuaternionEx.Conjugate(ref end, out end);
            QuaternionEx.Multiply(ref end, ref start, out end);
            return QuaternionEx.GetAngleFromQuaternion(ref end);
        }
    }
}