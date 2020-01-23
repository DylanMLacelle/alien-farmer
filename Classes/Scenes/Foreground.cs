using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Classes
{
    class Foreground : DrawableGameComponent
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; private set; }
        public static Vector2 ForegroundOffset { get; private set; }

        public Foreground(Game game) : base(game)
        {
            LoadContent();
            Position = new Vector2(0, Game.GraphicsDevice.Viewport.Height - Texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(Texture, Position, Color.White);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            DrawOrder = 1000;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("Images/foreground_grass");
            ForegroundOffset = Position;
            base.LoadContent();
        }
    }
}
