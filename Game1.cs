using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace GridProject
{
    public class Game1 : Game
    {
        private int ScreenHeight;
        private int ScreenWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;

        private readonly Dictionary<Vector2, int> fg;
        private readonly Dictionary<Vector2, int> mg;
        private readonly Dictionary<Vector2, int> collisions;
        private Texture2D _textureAtlas;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            fg = LoadTileMap("Data/level1_fg.csv");
            mg = LoadTileMap("Data/level1_mg.csv");
            collisions = LoadTileMap("Data/level1_collisions.csv");
            IsMouseVisible = true;
        }

        private static Dictionary<Vector2, int> LoadTileMap(string fileName)
        {
            Dictionary<Vector2, int> res = new();
            StreamReader sr = new(fileName);

            int y = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                for (int x = 0; x < values.Length; x++)
                {
                    if (values[x] != "-1")
                        res.Add(new(x, y), int.Parse(values[x]));
                }

                y++;
            }

            return res;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _contentManager = Content;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureAtlas = Content.Load<Texture2D>("Textures-16");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            int displaySize = 28;
            int tilePerRow = 64;
            int pixelTileSize = 8;

            foreach (var item in mg)
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

                _spriteBatch.Draw(_textureAtlas, dRect, src, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
