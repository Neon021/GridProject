using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridProject
{
    public class Sprite
    {

        public Texture2D Texture;
        public Rectangle Rect;
        public Rectangle Srect;
        public Vector2 Velocity;

        public Sprite(
            Texture2D texture,
            Rectangle rect,
            Rectangle srect
        )
        {
            Texture = texture;
            Rect = rect;
            Srect = srect;
            Velocity = new();
        }

        public void Update(KeyboardState keystate)
        {
            Velocity = Vector2.Zero;

            if (keystate.IsKeyDown(Keys.Right))
            {
                Velocity.X = 5;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                Velocity.X = -5;
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                Velocity.Y = -5;
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                Velocity.Y = 5;
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
}
