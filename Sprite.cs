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
        public Sprite(
            Texture2D texture,
            Rectangle rect,
            Rectangle srect
        )
        {
            Texture = texture;
            Rect = rect;
            Srect = srect;
            Grounded = false;
            Velocity = new();
        }

        public void Update(KeyboardState keystate, KeyboardState prevKeyState, GameTime gameTime)
        {
            Velocity.X = 0;

            Velocity.Y += 0.3f;
            Velocity.Y = Math.Min(25.0f, Velocity.Y);

            if (keystate.IsKeyDown(Keys.Right))
            {
                Velocity.X = 5;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                Velocity.X = -5;
            }

            if (Grounded && keystate.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space))
                Velocity.Y = -10;
            //Velocity.Y += -8;
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

    enum Direction { Left = -1, Right = 1, }
}
