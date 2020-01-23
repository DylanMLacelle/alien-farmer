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
    class CollectionBoxes : DrawableGameComponent
    {
        protected Texture2D Texture;
        protected Vector2 Position;
        protected Rectangle Collider;
        protected List<GameComponent> BoxContents;
        protected InventoryItemType MatchingItemType;

        string ImagePath { get; }

        public CollectionBoxes(Game game, string imagePath, InventoryItemType matchingItemType) : base(game)
        {
            ImagePath = imagePath;
            BoxContents = new List<GameComponent>();
            MatchingItemType = matchingItemType;
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
            DrawOrder = 10;
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

        /// <summary>
        /// Inserts an item into the collection box
        /// </summary>
        public virtual void Insert(GameComponent item)
        {
            BoxContents.Add(item);
        }

        /// <summary>
        /// Returns a collection of everything inside the collection box
        /// </summary>
        public virtual List<GameComponent> ReadCollectionBoxContents()
        {
            return BoxContents;
        }

        /// <summary>
        /// Get the rectangle of the collection box
        /// </summary>
        public virtual Rectangle GetCollider()
        {
            return Collider;
        }

        /// <summary>
        /// Check if an inventory item matches the boxs accepted inventory items
        /// </summary>
        public virtual bool IsMatchingItemType(GameComponent item)
        {
            if(item is InventoryItem)
            {
                if(((item as InventoryItem).Type & MatchingItemType) == (item as InventoryItem).Type)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
