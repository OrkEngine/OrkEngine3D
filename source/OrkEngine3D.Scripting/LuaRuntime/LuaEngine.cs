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
        private LuaFunction? Load;
        private LuaFunction? Update;
        private LuaFunction? Render;

        /// <summary>
        /// Class Constructor
        /// </summary>
        public LuaEngine() : this(false, false) { }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loadCLR">Will use CLR Packages</param>
        /// <param name="engineInit">Will use engine init</param>
        public LuaEngine(bool loadCLR, bool engineInit)
        {
            if (loadCLR)
                engine.LoadCLRPackage();

            if (engineInit)
            {
                Load = (LuaFunction)engine["OnLoad"];
                Update = (LuaFunction)engine["OnUpdate"];
                Render = (LuaFunction)engine["OnRender"];
            }
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

        #region GameEngine Code

        public virtual void OnLoad()
        {
            Load?.Call();
        }

        public virtual void OnRender()
        {
            Render?.Call();
        }

        public virtual void OnUpdate()
        {
            Update?.Call();
        }

        #endregion
    }
}
