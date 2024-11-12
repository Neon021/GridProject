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
            _intersections = new();

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
                    new Rectangle(TILESIZE, TILESIZE, TILESIZE, TILESIZE * 2),
                    new Rectangle(0, 0, 8, 16));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Update(Keyboard.GetState());

            // add _player's velocity and grab the intersecting tiles
            _player.Rect.X += (int)_player.Velocity.X;
            _intersections = GetIntersectingTilesHorizontally(_player.Rect);

            foreach (var rect in _intersections)
            {
                // handle collisions if the tile position exists in the tile map layer.
                if (_collisionsTiles.TryGetValue(new Vector2(rect.X, rect.Y), out _))
                {
                    // create temp rect to handle collisions (not necessary, you can optimize!)
                    Rectangle collision = new(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    );

                    // handle collisions based on the direction the _player is moving
                    if (_player.Velocity.X > 0.0f)
                    {
                        _player.Rect.X = collision.Left - _player.Rect.Width;
                    }
                    else if (_player.Velocity.X < 0.0f)
                    {
                        _player.Rect.X = collision.Right;
                    }
                }
            }

            // same as horizontal collisions
            _player.Rect.Y += (int)_player.Velocity.Y;
            _intersections = GetIntersectingTilesVertically(_player.Rect);

            foreach (var rect in _intersections)
            {
                if (_collisionsTiles.TryGetValue(new Vector2(rect.X, rect.Y), out _))
                {

                    Rectangle collision = new(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    );

                    if (_player.Velocity.Y > 0.0f)
                    {
                        _player.Rect.Y = collision.Top - _player.Rect.Height;
                    }
                    else if (_player.Velocity.Y < 0.0f)
                    {
                        _player.Rect.Y = collision.Bottom;
                    }

                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _mg.Draw(_spriteBatch, _textureAtlas);
            _fg.Draw(_spriteBatch, _textureAtlas);

            foreach (var rect in _intersections)
            {
                DrawRectHollow(
                    _spriteBatch,
                    new Rectangle(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    ),
                    4
                );
            }
            _player.Draw(_spriteBatch);
            DrawRectHollow(_spriteBatch, _player.Rect, 4);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public List<Rectangle> GetIntersectingTilesHorizontally(Rectangle target)
        {
            List<Rectangle> intersections = new();

            int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
            int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

            for (int x = 0; x < widthInTiles; x++)
            {
                for (int y = 0; y < heightInTiles; y++)
                {
                    intersections.Add(new(
                        (target.X + x * TILESIZE) / TILESIZE,
                        (target.Y + y * (TILESIZE - 1)) / TILESIZE,
                        TILESIZE,
                        TILESIZE
                        ));
                }
            }

            return intersections;
        }
        public List<Rectangle> GetIntersectingTilesVertically(Rectangle target)
        {
            List<Rectangle> intersections = new();

            int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
            int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

            for (int x = 0; x < widthInTiles; x++)
            {
                for (int y = 0; y < heightInTiles; y++)
                {
                    intersections.Add(new(
                        (target.X + x * (TILESIZE - 1)) / TILESIZE,
                        (target.Y + y * TILESIZE) / TILESIZE,
                        TILESIZE,
                        TILESIZE
                        ));
                }
            }

            return intersections;
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
