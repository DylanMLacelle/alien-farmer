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
    public class FarmPlot
    {
        bool PlotTaken { get; }
        public Vector2 PlotLocation { get; set; }
        public GameComponent PlotObject { get; set; }

        public FarmPlot(Vector2 plotLocation, bool plotTaken = false, GameComponent componentToPlot = null)
        {
            PlotObject = componentToPlot;
            PlotTaken = plotTaken;
            PlotLocation = plotLocation;
        }
    }
}
