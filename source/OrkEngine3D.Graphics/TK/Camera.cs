using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK
{
    public class Camera
    {
        public bool perspective = true;
        public float fov = 90;
        public float nearPlane = 0.1f;
        public float farPlane = 1000f;
        public Matrix GetMatrix(GraphicsContext ctx)
        {
            if(perspective)
                return Matrix.PerspectiveFovRH(fov, (float)ctx.window.Size.X / (float)ctx.window.Size.Y, nearPlane, farPlane);
            else
                return Matrix.OrthoRH((float)ctx.window.Size.X / (float)ctx.window.Size.Y, 1, nearPlane, farPlane);
        }
    }
}