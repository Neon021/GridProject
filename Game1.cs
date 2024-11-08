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
        private readonly string TileTexture = "TileTexture";
        private readonly string CharacterTexture = "Character";
        //private readonly string TextureAtlas = "GrassTiles";
        private readonly string TextureAtlas = "minimal_assets";
        private int ScreenHeight;
        private int ScreenWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;

        //private readonly Dictionary<Vector2, int> TileMap = new();
        private Dictionary<Vector2, int> mg = new();
        private Dictionary<Vector2, int> fg = new();
        private Dictionary<Vector2, int> colorLayer = new();
        private Dictionary<Vector2, int> collisions = new();
        private Texture2D _textureAtlas;
        private Dictionary<int, Rectangle> _textureStore;
        //private List<Rectangle> _textureStore;
        //private Texture2D _textureAtlas;

        //private Texture2D _tileTexture;
        //private int _tileHeight;
        //private int _tileWidth;

        //private MovingSprite _movingSprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_textureStore = new()
            //{
            //    new(23, 103, 47, 17),
            //    new(7, 15, 69, 79)
            //};
            _textureStore = new()
            {
                //Determine which textures were used from Tiled file
                //Calculate their positions and width/height from the texture atlas
                //And use thie CSV number as their Key part 

            };
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
                    if (values[x] != "0")
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

            //_tileTexture = _contentManager.Load<Texture2D>(TileTexture);
            //_tileHeight = _tileTexture.Height;
            //_tileWidth = _tileTexture.Width;

            //_movingSprite = new(_contentManager.Load<Texture2D>(CharacterTexture), Vector2.Zero, 5f);

            //_textureAtlas = _contentManager.Load<Texture2D>(TextureAtlas);

            mg = LoadTileMap("Data/level1_mg.csv");
            fg = LoadTileMap("Data/level1_fg.csv");
            colorLayer = LoadTileMap("Data/level1_color layer.csv");
            collisions = LoadTileMap("Data/level1_collisions.csv");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //_movingSprite.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //int numTilesX = (int)Math.Ceiling((float)ScreenWidth / _tileWidth);
            //int numTilesY = (int)Math.Ceiling((float)ScreenHeight / _tileHeight);


            //for (int x = 0; x < numTilesX; x++)
            //{
            //    for (int y = 0; y < numTilesY; y++)
            //    {
            //        _spriteBatch.Draw(_tileTexture, new Vector2(x * _tileWidth, y * _tileHeight), Color.White);
            //    }
            //}

            //_spriteBatch.Draw(_movingSprite.Texture, _movingSprite.Rectangle, Color.White);

            //foreach (var kv in TileMap)
            //{
            //    Rectangle dest = new(
            //        (int)kv.Key.X * 128,
            //        (int)kv.Key.Y * 128,
            //        128,
            //        128);

            //    Rectangle src = _textureStore[kv.Value - 1];

            //    _spriteBatch.Draw(_textureAtlas, dest, src, Color.White);
            //}
            _spriteBatch.Begin();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
