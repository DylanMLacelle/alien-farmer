using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Classes
{
    class TutorialScene : GameScene
    {
        public TutorialScene(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            BaseTexture2D background = new BaseTexture2D(Game, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), "Images/tutorial");
            this.SceneComponents.Add(background);
            base.Initialize();
        }
    }
}
