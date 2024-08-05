using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CurveCreator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Curve _curve;
        private CurveVisualiser _visualiser;

        private KeyboardState _oldState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            Texture2D blank = Content.Load<Texture2D>("square");
            SpriteFont font = Content.Load<SpriteFont>("Minecraft16");
            _visualiser = new CurveVisualiser(blank, font);
            _curve = new Curve(new Vector2(-100,0f), new Vector2(100f, 0f), new Vector2(100, 1f));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            Vector2 mousePos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            //_curve.SetControl(_visualiser.ScreenToGraphSpace(mousePos, _curve));

            KeyboardState keyboardState = Keyboard.GetState();

            #region abomination
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                _curve.AdjustPoint(0, 0, 0.02f);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _curve.AdjustPoint(0, 0, -0.02f);
            }
            
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _curve.AdjustPoint(1, 2f, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _curve.AdjustPoint(1, -2f, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _curve.AdjustPoint(1, 0, 0.02f);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _curve.AdjustPoint(1, 0, -0.02f);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                _curve.AdjustPoint(2, 0, 0.02f);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _curve.AdjustPoint(2, 0, -0.02f);
            }
            #endregion

            if (keyboardState.IsKeyDown(Keys.C) && _oldState.IsKeyUp(Keys.C))
            {
                Debug.WriteLine($"[{_curve.P0.X}, {_curve.P0.Y}, {_curve.P1.X}, {_curve.P1.Y}, {_curve.P2.X}, {_curve.P2.Y}]");
            }

            _oldState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin(SpriteSortMode.Deferred);

            _visualiser.Draw(_curve, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
