using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace air_hockey
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();

        }
    }
}
