using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GridProject
{
    public abstract class Tile
    {
        public int displaySize;
        public int tilePerRow;
        public int pixelTileSize;
        public Dictionary<Vector2, int> TileMap;

        public Tile(Dictionary<Vector2, int> tileMap)
        {
            TileMap = tileMap;
        }


        public virtual void Draw(SpriteBatch spriteBatch, Texture2D textureAtlas)
        {

        }
    }
}
