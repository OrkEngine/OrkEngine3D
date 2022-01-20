using OrkEngine3D.Components.Core;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK
{
    /// <summary>
    /// The graphics end camera
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Is the camera perspective or not?
        /// </summary>
        public bool perspective = true;

        /// <summary>
        /// Camera FOV
        /// </summary>
        public float fov = 90;

        /// <summary>
        /// The near plane in units
        /// Things closer than this wont render
        /// </summary>
        public float nearPlane = 0.1f;
        /// <summary>
        /// The far plane in units
        /// Things further away than this wont render
        /// </summary>
        public float farPlane = 1000f;

        /// <summary>
        /// The cameras transform, should be updated every frame
        /// </summary>
        public Transform transform = new Transform();

        /// <summary>
        /// Get the camera matrix, projection * view.
        /// </summary>
        /// <param name="ctx">The current graphics context</param>
        /// <returns>The camera matrix</returns>
        public Matrix GetMatrix()
        {
            GraphicsContext ctx = Rendering.currentContext;
            if (perspective)
                return Matrix.PerspectiveFovRH(fov, (float)ctx.window.Size.X / (float)ctx.window.Size.Y, nearPlane, farPlane);// * Matrix.LookAtRH(transform.position, transform.forward, transform.up);
            else
                return Matrix.OrthoRH((float)ctx.window.Size.X / (float)ctx.window.Size.Y, 1, nearPlane, farPlane) * Matrix.LookAtRH(transform.position, transform.forward, transform.up);
        }
    }
}