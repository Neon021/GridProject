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
            prevDirection = Direction;

            Velocity.X = 0;

            Velocity.Y += 0.3f;
            Velocity.Y = Math.Min(25.0f, Velocity.Y);


            if (keystate.IsKeyDown(Keys.Right))
            {
                Velocity.X = 5;
                Direction = Direction.Right;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                Velocity.X = -5;
                Direction = Direction.Left;
            }

            if (Grounded && keystate.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space))
                Velocity.Y = -10;

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
