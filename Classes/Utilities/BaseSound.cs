using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Classes
{
    /// <summary>
    /// Used for creating sounds
    /// </summary>
    class BaseSound : DrawableGameComponent
    {
        string FilePath { get; }
        private SoundEffect Sound { get; set; }

        public BaseSound(Game game, string filePath) : base(game)
        {
            FilePath = filePath;
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// plays the sound once
        /// </summary>
        public virtual void PlaySound(float volume = 1)
        {
            Sound.Play(volume,0,0);
        }

        protected override void LoadContent()
        {
            Sound = Game.Content.Load<SoundEffect>(FilePath);
            base.LoadContent();
        }
    }
}
