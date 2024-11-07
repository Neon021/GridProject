using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GridProject
{
    public class Game1 : Game
    {
        private readonly string TileTexture = "TileTexture";
        private readonly string CharacterTexture = "Character";
        private int ScreenHeight;
        private int ScreenWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;

        private Texture2D _tileTexture;
        private int _tileHeight;
        private int _tileWidth;

        private List<MovingSprite> _movingSprites = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            // TODO: use this.Content to load your game content here
            _tileTexture = _contentManager.Load<Texture2D>(TileTexture);
            _tileHeight = _tileTexture.Height;
            _tileWidth = _tileTexture.Width;

            for (int i = 0; i < 10; i++)
            {
                _movingSprites.Add(new(_contentManager.Load<Texture2D>(CharacterTexture), new(0, (float)Math.Log10(20 * i)), (float)Math.Sqrt(i)));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _movingSprites.ForEach(ms => ms.Update());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            int numTilesX = (int)Math.Ceiling((float)ScreenWidth / _tileWidth);
            int numTilesY = (int)Math.Ceiling((float)ScreenHeight / _tileHeight);

            _spriteBatch.Begin();

            for (int x = 0; x < numTilesX; x++)
            {
                for (int y = 0; y < numTilesY; y++)
                {
                    _spriteBatch.Draw(_tileTexture, new Vector2(x * _tileWidth, y * _tileHeight), Color.White);
                }
            }

            _movingSprites.ForEach(ms => _spriteBatch.Draw(ms.Texture, ms.Rectangle, Color.White));
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
