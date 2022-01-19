using System;
using OrkEngine3D.Graphics.TK;

namespace OrkEngine3D
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GraphicsContext ctx = new GraphicsContext();

            ctx.Run("Hello World");
        }
    }
}
