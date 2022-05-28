using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace air_hockey
{
    internal class Ball : CircleShape
    {
        public bool isMoving;
        public float speed = 0.3f;

        public Vector2f spawnPosition;
        public Vector2f moveDirection;

        public Player lastStriked;
        public Ball(float r)
        {
            Radius = r;
            FillColor = Color.White;
            isMoving = false;
        }
    }
}