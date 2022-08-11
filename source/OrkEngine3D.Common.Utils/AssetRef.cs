using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Common.Utils
{
    public class AssetRef<T>
    {
        public AssetID ID { get; set; }

        public AssetRef(AssetID id)
        {
            ID = id;
        }

        public AssetRef()
        {
        }
    }
}
