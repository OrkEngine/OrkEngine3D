using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
namespace OrkEngine3D.Graphics.TK
{
    public abstract class GraphicsHandler
    {
        public GraphicsContext context;
        public GLResourceManager resourceManager { get { return context.glmanager; } }

        /// <summary>
        /// On screen resize event
        /// </summary>
        public abstract void Resize();

        /// <summary>
        /// Unload references, same as Dispose()
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// On application render event
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// On application update event
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// On application load event
        /// </summary>
        public abstract void Init();
    }
}