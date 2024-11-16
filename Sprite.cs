using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GridProject
{
    public class Sprite
    {

        public Texture2D Texture;
        public Rectangle Rect;
        public Rectangle Srect;
        public Vector2 Velocity;

        public bool Grounded { get; set; }
        public Direction Direction { get; set; }
        public Direction prevDirection;

        private int numOfJumps = 2;
        public int JumpCounter { get; set; }
        public Sprite(
            Texture2D texture,
            Rectangle rect,
            Rectangle srect
        )
        {
            Texture = texture;
            Rect = rect;
            Srect = srect;

            Direction = Direction.Left;
            Grounded = false;

            Velocity = new();
        }

        public void Update(KeyboardState keystate, KeyboardState prevKeyState, GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds * 75;
            prevDirection = Direction;

            //Velocity.X = 0;

            Velocity.Y += 0.3f * dt;
            Velocity.Y = Math.Min(25.0f, Velocity.Y);


            if (keystate.IsKeyDown(Keys.Right))
            {
                Velocity.X += 0.5f * dt;
                Direction = Direction.Right;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                Velocity.X += -0.5f * dt;
                Direction = Direction.Left;
            }

            if (JumpCounter < numOfJumps && keystate.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space))
            {
                Velocity.Y = -10 * dt;
                JumpCounter++;
            }

            //Should it be in update or draw?
            if (prevDirection != Direction)
            {
                Srect.X += Srect.Width;
                Srect.Width = -Srect.Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Rect,
                Srect,
                Color.White
            );
        }
    }

    public enum Direction { Left = -1, Right = 1, }
}
