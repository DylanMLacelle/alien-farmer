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
    public abstract class BasePlant : DrawableGameComponent
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Collider;
        string ImagePath;
        FarmPlot farmPlot = FarmPlotManager.GetAvailableFarmPlot();
        Random random = new Random();

        protected BasePlant(Game game, List<string> imagePaths) : base(game)
        {
            Position = farmPlot.PlotLocation;
            farmPlot.PlotObject = this;
            int index = random.Next(0, imagePaths.Count);
            ImagePath = imagePaths[index];
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();

            sb.Draw(Texture, Position, GetColor());

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
            Collider = new Rectangle(0,0,Texture.Bounds.Width / 2, Texture.Bounds.Height);
            Collider.Location = new Point(Position.ToPoint().X + Texture.Width / 2, Position.ToPoint().Y);
            DrawOrder = 10;
            base.LoadContent();
        }

        public Color GetColor()
        {
            if (Collider.Intersects(Game.Services.GetService<Player>().collider))
            {
                return Color.LightGreen; 
            }
            else
            {
                return Color.White;
            }
        }

        /// <summary>
        /// returns an inventory item for the player to hold on to
        /// </summary>
        public virtual InventoryItem Gather()
        {
            Game.Components.Remove(this);
            PlantManager.Plants.Remove(this);
            farmPlot.PlotObject = null;
            return new InventoryItem(Game, ImagePath);
        }
    }
}
