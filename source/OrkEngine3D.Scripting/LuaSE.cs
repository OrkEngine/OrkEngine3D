using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
namespace OrkEngine3D.Scripting
{
    internal class LuaSE
    {
        static Lua lua = new Lua();
        static LuaFunction? Load;
      //  static LuaFunction Unload;
        static LuaFunction? Update;
       // static LuaFunction OnReset;
        //static LuaFunction Execute;
       // static LuaFunction OnExecuteLate;
        static LuaFunction? Render;
      //  static LuaFunction OnRenderLate;
       // static LuaFunction OnRenderObject;
        //static LuaFunction OnWillRenderObject;
        //static LuaFunction OnWillRenderObjectLate;
        public static void test()
        {
            //lua.DoString("print(\"hello from lua\")");
            lua.DoFile("scripts/lua/program.lua");

            var scriptFunc = lua["RunMath"] as LuaFunction;
            Load = (LuaFunction)lua["OnLoad"];
            Update = (LuaFunction)lua["OnUpdate"];
            Render = (LuaFunction)lua["OnRender"];
            //Execute = lua["Execute"] as LuaFunction;
            Int64 res = (Int64)scriptFunc.Call(3, 5).First();
            Console.WriteLine(res);

            OnLoad();
            OnRender();
            OnUpdate();
        }

        #region DummyFunctions

        public static void OnLoad()
        {
            Load.Call();
           // Console.WriteLine("OnEngineLoad");
        }

        public static void OnRender()
        {
            Render.Call();
           // Console.WriteLine("OnEngineRender");
        }

        public static void OnUpdate()
        {
            Update.Call();
           // Console.WriteLine("OnEngineUpdate");
        }
        #endregion
    }
}
