using System;

namespace OrkEngine3D.Core {

    [AttributeUsage(AttributeTargets.Method)]

    public class ModuleInitializerAttribute
    {
        public ModuleInitializerAttribute()
        {
        }

        public ModuleInitializerAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}
