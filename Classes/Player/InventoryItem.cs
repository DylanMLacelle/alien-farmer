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
    [Flags]
    public enum InventoryItemType
    {
        RedMushroom = 1,
        WhiteMushroom = 2,
        BrownMushroom = 4,
        PurplePlant = 8,
        GreenPlant = 16
    }


    /// <summary>
    /// An item that is held in the inventory 
    /// </summary>
    public class InventoryItem : DrawableGameComponent
    {
        const int INVENTORY_OFFSET = 5;
        public const int MAX_INVENTORY_ITEMS = 10;

        Texture2D Texture;
        Vector2 Position;
        string ImagePath;
        public InventoryItemType Type { get; }

        //tracks the amount of Inventory items that are active
        public static int InventoryItems { get; set; }

        public InventoryItem(Game game, string imagePath) : base(game)
        {
            ImagePath = imagePath;
            switch(ImagePath)
            {
                case "Images/goodPlant1":
                    Type = InventoryItemType.BrownMushroom;
                    break;
                case "Images/goodPlant2":
                    Type = InventoryItemType.RedMushroom;
                    break;
                case "Images/goodPlant3":
                    Type = InventoryItemType.WhiteMushroom;
                    break;
                case "Images/badPlant1":
                    Type = InventoryItemType.GreenPlant;
                    break;
                case "Images/badPlant2":
                    Type = InventoryItemType.PurplePlant;
                    break;
            }
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
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture =  Game.Content.Load<Texture2D>(ImagePath);
            Position = new Vector2((INVENTORY_OFFSET + InventoryItems * Texture.Width), (INVENTORY_OFFSET));
            InventoryItems++;
            base.LoadContent();
        }
    }
}
