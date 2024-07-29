using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CurveCreator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Curve _curve;
        private CurveVisualiser _visualiser;

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
            _visualiser = new CurveVisualiser(blank);
            _curve = new Curve(new Vector2(0,0), new Vector2(0.5f, 0.5f), new Vector2(1, 1));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            Vector2 mousePos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            //_curve.SetControl(_visualiser.ScreenToGraphSpace(mousePos, _curve));

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _curve.DecrementControlX();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _curve.IncrementControlX();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _curve.DecrementControlY();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _curve.IncrementControlY();
            }

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
