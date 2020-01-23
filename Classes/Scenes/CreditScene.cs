using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Classes
{
    class CreditScene : GameScene
    {
        public CreditScene(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            BaseTexture2D background = new BaseTexture2D(Game, new Rectangle(0,0,Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), "Images/credits");
            this.SceneComponents.Add(background);
            base.Initialize();
        }
    }
}
