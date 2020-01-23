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
    class PlantManager : GameComponent
    {
        const int TOTAL_PLANTS_SPAWNED = 9;

        public static List<GameComponent> Plants;
        Random random = new Random();

        double timeUntilNextSpawn = 0;
        const double SPAWN_INTERVAL = 0.6;

        public PlantManager(Game game) : base(game)
        {
            Plants = new List<GameComponent>();
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //if the farm isnt full increment time, but if its full dont increment
            if(!FarmPlotManager.IsFarmFull())
            {
                timeUntilNextSpawn += gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if (timeUntilNextSpawn >= SPAWN_INTERVAL && (Plants.Count < TOTAL_PLANTS_SPAWNED))
            {
                SpawnPlant();
                timeUntilNextSpawn = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Spawns a new plant good or bad
        /// </summary>
        private void SpawnPlant()
        {
            BasePlant plantToAdd;

            if(isGoodPlant())
            {
                plantToAdd = new GoodPlant(Game);
                Game.Services.GetService<GameplayScene>().SceneComponents.Add(plantToAdd);
                Game.Components.Add(plantToAdd);
            }
            else
            {
                plantToAdd = new BadPlant(Game);
                Game.Services.GetService<GameplayScene>().SceneComponents.Add(plantToAdd);
                Game.Components.Add(plantToAdd);
            }
            UpdateList();
        }

        /// <summary>
        /// checks whether a good or bad plant will be spawned
        /// </summary>
        /// <returns></returns>
        private bool isGoodPlant()
        {
            if(random.Next(0,100) >= 50)
            {
                return true;
            }
            return false;
        }

        public static BasePlant GetPlantCollisions(Rectangle playerCollider)
        {
            foreach (BasePlant plant in Plants)
            {
                if (playerCollider.Intersects(plant.Collider))
                {
                    return ((BasePlant)plant);
                }
            }
            return null;
        }

        /// <summary>
        /// updates the plant list to have all active game plant components in the scene
        /// </summary>
        private void UpdateList()
        {
            Plants = new List<GameComponent>();
            foreach (GameComponent component in Game.Components)
            {
                if(component is BasePlant)
                {
                    Plants.Add(((BasePlant)component));
                }
            }
        }
    }
}
