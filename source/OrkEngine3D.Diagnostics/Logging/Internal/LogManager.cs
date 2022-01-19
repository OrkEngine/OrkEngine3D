using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Diagnostics.Logging.Internal
{
    internal class LogManager
    {
        public LogManager() { }

        /*
         * This is temp, add a Debug Stream and Debug Class 
         * 
         */
        public void Push(string logMessage)
        {
            Console.WriteLine(logMessage);
        }

        public void Load(string logPath)
        {

        }

        public void Save(string logPath)
        {
            
        }

        private DateTime GetTimeStamp()
        {
            return DateTime.Parse("dd-MM-yy-HH-mm");
        }
    }
}
