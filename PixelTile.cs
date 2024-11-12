using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GridProject
{
    public class PixelTile : Tile
    {
        public PixelTile(Dictionary<Vector2, int> tileMap) : base(tileMap)
        {
            displaySize = 28;
            tilePerRow = 64;
            pixelTileSize = 8;

            TileMap = tileMap;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D textureAtlas)
        {
            foreach (var item in TileMap)
            {
                Rectangle dRect = new(
                    (int)item.Key.X * displaySize,
                    (int)item.Key.Y * displaySize,
                    displaySize,
                    displaySize
                );

                //Because the Value in KV is the actual ID of the tile in the atlas
                int x = item.Value % tilePerRow;
                int y = item.Value / tilePerRow;

                Rectangle src = new(
                    x * pixelTileSize,
                    y * pixelTileSize,
                    pixelTileSize,
                pixelTileSize
                );

                spriteBatch.Draw(textureAtlas, dRect, src, Color.White);
            }
        }
    }
}