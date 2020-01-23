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
    class BadCollectionBox : CollectionBoxes
    {
        public BadCollectionBox(Game game) : base(game, "Images/badBox", InventoryItemType.PurplePlant|InventoryItemType.GreenPlant)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(Game.GraphicsDevice.Viewport.Width - Texture.Width, Game.GraphicsDevice.Viewport.Height - Texture.Height);
            Collider = Texture.Bounds;
            Collider.Location = Position.ToPoint();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
