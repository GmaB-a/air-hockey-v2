using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace air_hockey
{
    internal class Game
    {
        private RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");

        private Player player1;
        private Player player2;

        private Ball ball = new Ball(5f);
        private Random rnd = new Random();
        public float lineSpeed = 0.2f;
        public void Play()
        {
            window.Closed += WindowClosed;

            player1 = new Player(window, new Vector2f(50, window.Size.Y / 2 - 25));
            player2 = new Player(window, new Vector2f(window.Size.X - 50, window.Size.Y / 2 - 25));

            ball.spawnPosition = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            ball.Position = ball.spawnPosition;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                GetInput();
                BallLogic();
                DrawLines();
                //DrawScores();
                CoinLogic();

                window.Display();
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }

        private void DrawLines()
        {
            window.Draw(player1.line);
            window.Draw(player2.line);
        }

        private void GetInput()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) CheckIfCanMove(player1.line, -1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) CheckIfCanMove(player1.line, 1);


            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) CheckIfCanMove(player2.line, -1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) CheckIfCanMove(player2.line, 1);
        }

        private void CheckIfCanMove(RectangleShape line, int dy)
        {
            if ((line.Position.Y - lineSpeed < 0) && dy < 0) return;
            if ((line.Position.Y + line.Size.Y + lineSpeed > window.Size.Y) && dy > 0) return;
            Move(line, dy);
        }

        private void Move(RectangleShape line, int dy)
        {
            Vector2f newPosition = new Vector2f(line.Position.X, line.Position.Y + dy * lineSpeed);
            line.Position = newPosition;
        }

        /*void DrawScores()
        {
            string stringScores = player1Score + " : " + player2Score;
            Text scores = new Text();
            scores.DisplayedString = stringScores;
            scores.CharacterSize = 15;
            scores.FillColor = Color.White;
            scores.Position = new SFML.System.Vector2f(window.Size.X / 2, 160);
            window.Draw(scores);
        } */

        private void BallLogic()
        {
            if (!ball.isMoving)
            {
                while (ball.moveDirection == new Vector2f(0, 0))
                    ball.moveDirection = new Vector2f((float)rnd.NextDouble() * rnd.Next(-1, 2), (float)rnd.NextDouble() * rnd.Next(-1, 2));
                ball.isMoving = true;
            }
            ball.Position += ball.moveDirection * ball.speed;
            CheckIfSomebodyGotScore();
            CheckIfShouldChangeDirection(); ;
            window.Draw(ball);
        }

        private void CheckIfSomebodyGotScore()
        {
            if (ball.Position.X >= window.Size.X - ball.Radius)
            {
                ball.Position = ball.spawnPosition;
                ball.isMoving = false;
                player1.score++;
            }
            else if (ball.Position.X <= ball.Radius)
            {
                ball.Position = ball.spawnPosition;
                ball.isMoving = false;
                player2.score++;
            }
        }
        private void CheckIfShouldChangeDirection()
        {
            if (ball.Position.Y > window.Size.Y - ball.Radius && ball.moveDirection.Y > 0) ball.moveDirection.Y *= -1f;
            else if (ball.Position.Y < ball.Radius && ball.moveDirection.Y < 0) ball.moveDirection.Y *= -1f;
            CheckIfHitPlayerLine();
        }

        void CheckIfHitPlayerLine()
        {
            if (IsOnRightY(player1))
            {
                if (IsOnRightX(player1, 1))
                {
                    ChangeDirectionAndLastStriked(player1);
                }
            }

            if (IsOnRightY(player2))
            {
                if (IsOnRightX(player2, 2))
                {
                    ChangeDirectionAndLastStriked(player2);
                }
            }
        }

        private bool IsOnRightY(Player player)
            => (ball.Position.Y > player.line.Position.Y) && (ball.Position.Y < player.line.Position.Y + player.line.Size.Y);

        private bool IsOnRightX(Player player, int operator_func)
        {
            if(operator_func == 1) return player.line.Position.X > ball.Position.X + ball.Radius;
            return player.line.Position.X < ball.Position.X + ball.Radius;
        }

        private void ChangeDirectionAndLastStriked(Player player)
        {
            ball.moveDirection.X *= -1f;
            ball.lastStriked = player;
        }

        private void CoinLogic()
        {

        }
    }
}