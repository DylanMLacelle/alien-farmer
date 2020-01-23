using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Classes
{
    class BaseSong : DrawableGameComponent
    {
        string FilePath { get; }
        private Song Music { get; set; }

        public BaseSong(Game game, string filePath) : base(game)
        {
            FilePath = filePath;
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(Music);
                MediaPlayer.Volume = 0.2f;
            }
        }
            
        protected override void LoadContent()
        {
            Music = Game.Content.Load<Song>(FilePath);
            base.LoadContent();
        }
    }
}
