using System;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Diagnostics;

namespace OrkEngine3D.Scripting
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            /*
            var stopwatch = Stopwatch.Start​New();
            PythonSE.Run();
            
            stopwatch.Stop();

            Console.WriteLine("Python: " + stopwatch.ElapsedMilliseconds);

            var stopwatch2 = Stopwatch.StartNew();
            */
            LuaSE.test();

            //stopwatch2.Stop();

            //Console.WriteLine("Lua: " + stopwatch2.ElapsedMilliseconds);
        }

       
    }
}
