using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;

namespace OrkEngine
{
    /// <summary>
    /// Default Game class
    /// </summary>
    public class Game// : GameWindow
    {

    } /*: GameWindow
    { 
        
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();
            
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            /*
             * TODO: game interface 
             * Have an import of a custom game interface, and plug it in here, so that
             * the game will run the included interface as game, so onupdateframe doesn't have to be
             * over written again. Example:
             * <interfaceclass>.OnUpdate(<args>);
             
            base.OnUpdateFrame(e);
        }

    }*/
}
