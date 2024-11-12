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
        private readonly int TILESIZE = 64;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;

        private PixelTile _fg;
        private readonly Dictionary<Vector2, int> _fgTiles;
        private PixelTile _mg;
        private readonly Dictionary<Vector2, int> _mgTiles;
        private PixelTile _collisions;
        private readonly Dictionary<Vector2, int> _collisionsTiles;
        private Texture2D _textureAtlas;


        private Sprite _player;
        private Texture2D _rectangleTexture;

        private List<Rectangle> _intersections;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                SynchronizeWithVerticalRetrace = true,
            };
            Content.RootDirectory = "Content";
            _fgTiles = LoadTileMap("Data/level1_fg.csv");
            _mgTiles = LoadTileMap("Data/level1_mg.csv");
            _collisionsTiles = LoadTileMap("Data/level1_collisions.csv");
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

            _mg = new(_mgTiles);
            _fg = new(_fgTiles);
            _collisions = new(_collisionsTiles);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureAtlas = Content.Load<Texture2D>("Textures-16");

            _rectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _rectangleTexture.SetData(new Color[] { new(255, 0, 0, 255) });
            _player = new Sprite(
                    Content.Load<Texture2D>("player_static"),
                    new Rectangle(28, 28, 28, 28 * 2),
                    new Rectangle(0, 0, 8, 16)
);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Update(Keyboard.GetState());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _mg.Draw(_spriteBatch, _textureAtlas);
            _fg.Draw(_spriteBatch, _textureAtlas);

            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawRectHollow(SpriteBatch spriteBatch, Rectangle rect, int thickness)
        {
            spriteBatch.Draw(
                _rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                _rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Bottom - thickness,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                _rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );
            spriteBatch.Draw(
                _rectangleTexture,
                new Rectangle(
                    rect.Right - thickness,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );
        }
    }
}
