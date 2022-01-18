using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core.templates
{
    /// <summary>
    /// Inherits from IGame(or IOrkGame) and IOrk<etc>
    /// This is class that people put inside of there code to reference from as game loop goes
    /// </summary>
    public class OrkGame
    {
        /// <summary>
        /// On before load, so when window opens up. (Semi used?)
        /// </summary>
        public void OnBeforeLoad()
        {

        }

        /// <summary>
        /// On load, during time most stuff loads. (Always used?)
        /// </summary>
        public void OnLoad()
        {

        }

        /// <summary>
        /// On After load, after most stuff is loaded. (Semi used?)
        /// </summary>
        public void OnAfterLoad()
        {

        }

        /// <summary>
        /// Current update
        /// </summary>
        public void OnUpdate()
        {

        }

        /// <summary>
        /// Fixed update
        /// </summary>
        public void OnFixedUpdate()
        {

        }

        /// <summary>
        /// Late update
        /// </summary>
        public void OnLateUpdate()
        {

        }

        /// <summary>
        /// On Timespan update
        /// </summary>
        public void OnTSUpdate(TimeSpan span)
        {

        }

        public void OnCleanup(CleanupTime ct)
        {

        }

        /// <summary>
        /// Clean up code using enum shows another way to do same thing.
        /// </summary>
        /// <param name="ct">Cleanup enum</param>
        /// <param name="disposeAutomatically">Does user handle when stuff is disposed or not</param>
        public void OnCleanup(CleanupTime ct, bool disposeAutomatically)
        {

        }
    }
    //temp class
    public enum CleanupTime
    {
        Before,
        Current, //During
        After
    }
}
