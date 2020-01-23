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
    class GoodCollectionBox : CollectionBoxes
    {
        public GoodCollectionBox(Game game) : base(game, "Images/goodBox", InventoryItemType.BrownMushroom|InventoryItemType.RedMushroom|InventoryItemType.WhiteMushroom)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(0, Game.GraphicsDevice.Viewport.Height - Texture.Height);
            Collider = Texture.Bounds;
            Collider.Location = Position.ToPoint();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
