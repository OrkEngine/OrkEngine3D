using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrkEngine3D;
namespace OrkGame
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "OrkEngine3D - Test Game"))
            {
                game.Run(60.0);
            }
        }
    }
}
