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
    class BaseTexture2D : DrawableGameComponent
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }
        string ImagePath { get; set; }
        Rectangle Size { get; set; }

       public BaseTexture2D(Game game, Vector2 position, string imagePath) : base(game)
        {
            Position = position;
            ImagePath = imagePath;
        }

        public BaseTexture2D(Game game, Rectangle position, string imagePath): base(game)
        {
            Position = new Vector2(position.X,position.Y);
            ImagePath = imagePath;
            Size = position;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            if(Size == Rectangle.Empty)
            {
                sb.Draw(Texture, Position, Color.White);
            }
            else
            {
                sb.Draw(Texture, Size, Color.White);
            }
            sb.End();
            base.Draw(gameTime);
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>(ImagePath);
            base.LoadContent();
        }
    }
}
