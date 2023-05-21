using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Annotations
{
    //testing move to annotations
    public class QualifiedNameAttribute : Attribute
    {
        public string QualifiedName { get; private set; }

        public QualifiedNameAttribute(string qualifiedName)
        {
            QualifiedName = qualifiedName;
        }

        public static bool CheckQualifiedName(string qualifiedName)
        {

            return false;
        }
    }
}
