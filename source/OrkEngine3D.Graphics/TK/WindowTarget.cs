using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics.TK
{
    public class WindowTarget : IRenderTarget
    {
        public static readonly WindowTarget Global = new WindowTarget();
        private WindowTarget() { }
        public void BindTarget()
        {
            GL.Viewport(0, 0, Rendering.currentContext.window.Size.X, Rendering.currentContext.window.Size.Y);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void PreRender()
        {
            
        }
    }
}
