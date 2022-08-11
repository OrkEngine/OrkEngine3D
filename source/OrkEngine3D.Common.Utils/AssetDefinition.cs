using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Common.Utils
{
    public interface AssetDefinition
    {
        object Create(AssetDatabase ad);
    }

    public abstract class AssetDefinition<T> : AssetDefinition
    {
        public abstract T Create(AssetDatabase ad);

        object AssetDefinition.Create(AssetDatabase ad)
        {
            return Create(ad);
        }
    }
}
