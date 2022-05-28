using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace air_hockey
{
    internal class Player
    {
        public RectangleShape line;
        public int score;
        public Player(RenderWindow window, Vector2f position)
        {
            line = new RectangleShape();
            line.Size = new Vector2f(5, 100);
            line.Position = position;
            line.FillColor = Color.White;
            score = 0;
        }
    }
}