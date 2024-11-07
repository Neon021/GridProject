using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridProject
{
    public class MovingSprite : ScaledSprite
    {
        private float Speed;
        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            Speed = speed;
        }

        public override void Update()
        {
            base.Update();
            Position.X += Speed;
        }
    }
}
