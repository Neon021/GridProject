using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridProject
{
    public class MovingSprite : ScaledSprite
    {
        private float Speed;
        private bool _isMoving;
        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            Speed = speed;
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= Speed;
            }
            base.Update();
        }
    }
}
