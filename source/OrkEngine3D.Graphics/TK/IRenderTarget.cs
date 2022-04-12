using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics.TK
{
    public interface IRenderTarget
    {
        public void BindTarget();
        public void Clear();
        public void PreRender();
    }
}
