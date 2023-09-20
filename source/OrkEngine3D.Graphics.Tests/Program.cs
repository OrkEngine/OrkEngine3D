using OrkEngine3D.Diagnostics.Logging;
using OrkEngine3D.Graphics.MeshData;
using OrkEngine3D.Graphics.TK;
//using OrkEngine3D.Mathematics;
using System;
using System.IO;
using System.Linq;
//using Mesh = OrkEngine3D.Graphics.TK.Resources.Mesh;
using Material = OrkEngine3D.Graphics.TK.Material;
using OrkEngine3D.Core.Components;
using System.Collections.Generic;
//using OrkEngine3D.Components;

namespace OrkEngine3D.Graphics.Tests;

class Program
{
    static void Main(string[] args)
    {

        Logger logger = new Logger("MainLogger", "NoModule");
        logger.Log(LogMessageType.DEBUG, "Teapot");

        GContext ctx = new GContext("Hello World", new TestGame()).Run();
    }
}
