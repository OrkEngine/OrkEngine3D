using OrkEngine3D.Components.Core;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK
{
    public class Camera
    {
        public bool perspective = true;
        public float fov = 90;
        public float nearPlane = 0.1f;
        public float farPlane = 1000f;
        public Transform transform = new Transform();
        public Matrix GetMatrix(GraphicsContext ctx)
        {
            if(perspective)
                return Matrix.PerspectiveFovRH(fov, (float)ctx.window.Size.X / (float)ctx.window.Size.Y, nearPlane, farPlane);// * Matrix.LookAtRH(transform.position, transform.forward, transform.up);
            else
                return Matrix.OrthoRH((float)ctx.window.Size.X / (float)ctx.window.Size.Y, 1, nearPlane, farPlane) * Matrix.LookAtRH(transform.position, transform.forward, transform.up);
        }
    }
}