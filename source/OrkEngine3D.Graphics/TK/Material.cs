using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics.TK
{
    public class Material
    {
        public Color3 ambient = new Color3(1.0f, 1.0f, 1.0f);
        public Color3 diffuse = new Color3(1.0f, 1.0f, 1.0f);
        public Color3 specular = new Color3(1.0f, 1.0f, 1.0f);
        public float shininess = 64f;
        public Texture[] textures = new Texture[0];
    }
}
