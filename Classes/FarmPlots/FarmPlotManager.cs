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
    /// <summary>
    /// This class manages all the farm plots we need to cover the map
    /// </summary>
    class FarmPlotManager : GameComponent
    {
        const int DEFAULT_TEXTURE_SIZE = 70;
        int SCREEN_OFFSET;
        static Random random = new Random();
        const int MAX_PLOTS = 9;
        public static List<FarmPlot> FarmPlots;

        public FarmPlotManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            FarmPlots = new List<FarmPlot>();
            // leaves an even amount of space on both sides depending on number of plots 
            SCREEN_OFFSET = ((Game.GraphicsDevice.Viewport.Width - (DEFAULT_TEXTURE_SIZE * MAX_PLOTS)) / 2);
            for (int plots = 0; plots < MAX_PLOTS; plots++)
            {
                FarmPlots.Add(new FarmPlot(new Vector2(SCREEN_OFFSET + (plots * DEFAULT_TEXTURE_SIZE), Game.GraphicsDevice.Viewport.Height - DEFAULT_TEXTURE_SIZE)));
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// searches through the list of farmplots to see if there is a location available 
        /// and returns the position
        /// </summary>
        public static FarmPlot GetAvailableFarmPlot()
        {
            int startingIndex = random.Next(0, FarmPlots.Count);

            //get a random number and go to top of plot to check for free plot
            //this will make the plants grow in a random pattern
            //next for statement checks going down so all spots are covered
            for (int i = startingIndex; i < FarmPlots.Count; i++)
            {
                if (FarmPlots[i].PlotObject == null)
                {
                    return FarmPlots[i];
                }
            }
            for (int i = startingIndex; i < FarmPlots.Count; i--)
            {
                if (FarmPlots[i].PlotObject == null)
                {
                    return FarmPlots[i];
                }
            }
            return null;
        }

        /// <summary>
        /// used to check if the farm is full
        /// </summary>
        /// <returns></returns>
        public static bool IsFarmFull()
        {
            return PlantManager.Plants.Count == MAX_PLOTS;
        }
    }
}
