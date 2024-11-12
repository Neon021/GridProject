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

        private PixelTile fg;
        private readonly Dictionary<Vector2, int> fgTiles;
        private PixelTile mg;
        private readonly Dictionary<Vector2, int> mgTiles;
        private PixelTile collisions;
        private readonly Dictionary<Vector2, int> collisionsTiles;

        private Texture2D _textureAtlas;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            fgTiles = LoadTileMap("Data/level1_fg.csv");
            mgTiles = LoadTileMap("Data/level1_mg.csv");
            collisionsTiles = LoadTileMap("Data/level1_collisions.csv");
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

            mg = new(mgTiles);
            fg = new(fgTiles);
            collisions = new(collisionsTiles);

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

            mg.Draw(_spriteBatch, _textureAtlas);
            fg.Draw(_spriteBatch, _textureAtlas);
            collisions.Draw(_spriteBatch, _textureAtlas);
            


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
