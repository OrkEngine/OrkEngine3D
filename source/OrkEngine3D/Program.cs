using System;
using System.Collections.Generic;
using System.IO;
using OrkEngine3D.Components.Core;
using OrkEngine3D.Graphics;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
using OrkEngine3D.Graphics.TK.Resources;
using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Mathematics;
using MathF = OrkEngine3D.Mathematics.MathF;

namespace OrkEngine3D
{
    public class Program
    {
        static Logger logger = Logger.Get("Program", "Program");
        public static void Main(string[] args)
        {
            logger.Log(LogMessageType.DEBUG, "Hello World");
        }
    }
}
