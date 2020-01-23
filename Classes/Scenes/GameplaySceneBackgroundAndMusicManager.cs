using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1.Classes
{
    /// <summary>
    /// Manages the backgrounds and music / sound effects for the gameplay scene
    /// </summary>
    public class GameplaySceneBackgroundAndMusicManager : DrawableGameComponent
    {
        Texture2D BackgroundTexture { get; set; }
        //The grass for the plants to grow out of
        Texture2D ForegroundTexture { get; set; }
        Rectangle SizeOfTextures { get; }

        public GameplaySceneBackgroundAndMusicManager(Game game) : base(game)
        {
            SizeOfTextures = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            Foreground foreground = new Foreground(Game);
            Game.Services.GetService<GameplayScene>().SceneComponents.Add(foreground);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(BackgroundTexture, SizeOfTextures, Color.White);
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
            BackgroundTexture = Game.Content.Load<Texture2D>("Images/background1");
            base.LoadContent();
        }
    }
}
