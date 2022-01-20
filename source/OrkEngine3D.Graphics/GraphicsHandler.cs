using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;

namespace OrkEngine3D.Graphics
{
    public abstract class GraphicsHandler
    {
        public GraphicsContext context;
        public GLResourceManager resourceManager { get { return context.glmanager; } }

        public abstract void Render();
        public abstract void Update();
        public abstract void Init();
    }
}