using System;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
namespace OrkEngine3D.Scripting
{
    internal class PythonSE
    {
        static ScriptEngine engine = Python.CreateEngine();
        static ScriptScope? scope;

        public static void Run()
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            //engine.CreateScriptSourceFromString("print('hello, world')").Execute();
            // Console.WriteLine("Hello World!");

            LoadScript("Testing.py").Execute(scope);

            var adder = scope.GetVariable<Func<object, object, object>>("adder");
            Console.WriteLine("ADDER: " + adder(2, 2));
            Console.WriteLine("ADDER: " + adder(2.0, 2.5));

            var myClass = scope.GetVariable<Func<object, object>>("MyClass");
            var myInstance = myClass("hello");

            Console.WriteLine("Instance: " + engine.Operations.GetMember(myInstance, "value"));

            //Console.ReadKey();
        }

        #region LoadScripts
        /*
         * Loads the scripts folder
         * 
         */
        public static ScriptSource LoadScript(string scriptName)
        {
            return engine.CreateScriptSourceFromFile("scripts/python/" + scriptName);
        }

        #endregion

       
    }
}
