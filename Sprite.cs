using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridProject
{
    public class Sprite
    {
        public Texture2D Texture; 
        public Vector2 Position;

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Update() { }
    }
}
