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
    class GoodPlant : BasePlant
    {
        public GoodPlant(Game game) : base(game, new List<string>() { "Images/goodPlant1", "Images/goodPlant2", "Images/goodPlant3" })
        {
        }
    }
}
