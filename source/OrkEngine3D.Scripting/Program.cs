using System;
using Jint;
namespace OrkEngine3D.Scripting
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine().SetValue("log", new Action<object>(Console.WriteLine));

            engine.Execute(@"
function hello() {
    log('Hello World');
};

hello();");

            Console.WriteLine("Hello World!");
        }
    }
}
