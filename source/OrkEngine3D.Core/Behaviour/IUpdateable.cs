using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core.Behaviour
{
    public interface IUpdateable
    {
        void Update(float deltaSeconds);
    }
}
