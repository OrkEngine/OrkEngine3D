using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
namespace OrkEngine3D.Scripting.LuaRuntime
{
    public class LuaEngine
    {
        private Lua engine = new Lua();

        /// <summary>
        /// Class Constructor
        /// </summary>
        public LuaEngine() : this(false) { }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loadCLR">Will use CLR Packages</param>
        public LuaEngine(bool loadCLR)
        {
            if (loadCLR)
                engine.LoadCLRPackage();
        }

        /// <summary>
        /// Lua Engine Reference
        /// </summary>
        public Lua Engine => engine;

        #region CodeExecution

        /// <summary>
        /// Run Lua String
        /// </summary>
        /// <param name="code">Code to run</param>
        /// <returns>returns class instance</returns>
        public LuaEngine DoString(string code)
        {
            engine.DoString(code);
            return this;
        }

        /// <summary>
        /// Registers a lua function/method/object
        /// </summary>
        /// <param name="luaObject">lua object</param>
        /// <returns>registered lua object</returns>
        public LuaFunction RegisterLuaObject(string luaObject)
        {
            return (LuaFunction)engine[luaObject];
        }

        /// <summary>
        /// Run Lua String Multiple
        /// </summary>
        /// <param name="code">Code to run</param>
        /// <returns>returns class instance</returns>
        public LuaEngine DoStringM(string[] code)
        {
            foreach (string codeItem in code)
            {
                engine.DoString(codeItem);
            }
            return this;
        }

        /// <summary>
        /// Run Lua File
        /// </summary>
        /// <param name="pfile">path to file location</param>
        /// <returns>returns class instance</returns>
        public LuaEngine DoFile(string pfile)
        {
            engine.DoFile(pfile);
            return this;
        }

        /// <summary>
        /// Run Lua File(s)
        /// </summary>
        /// <param name="pfiles">path to files location</param>
        /// <returns>returns class instance</returns>
        public LuaEngine DoFile(string[] pfiles)
        {
            foreach (string pfile in pfiles)
            {
                engine.DoFile(pfile);
            }
            return this;
        }

        #endregion
    }
}
