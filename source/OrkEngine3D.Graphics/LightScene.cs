using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics
{
    public class LightScene
    {
        public Light ambient = new Light(0.2f, new Color3(1f, 1f, 1f), new Vector3(0, 0, 0));
        public Light[] lights = new Light[] {
            new Light(0.9f, new Color3(1f, 1f, 1f), new Vector3(10, 0, 0)),
        };
    }

    public struct Light
    {
        public float strength;
        public Color3 color;
        public Vector3 position;

        public Light(float strength, Color3 color, Vector3 pos)
        {
            this.strength = strength;
            this.color = color;
            this.position = pos;
        }
    }
}
