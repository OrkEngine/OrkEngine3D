using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Mathematics;

namespace OrkEngine.Engine
{
    public class Transform
    {
        private Vector3 m_position = new Vector3(0, 0, 0);
        private Vector3 m_rotation = new Vector3(0, 0, 0);
        private Vector3 m_scale = new Vector3(1, 1, 1);

        public Vector3 Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return m_rotation;
            }
            set
            {
                m_rotation = value;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return m_scale;
            }
            set
            {
                m_scale = value;
            }
        }
    }
}
