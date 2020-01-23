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
    public abstract class GameScene : GameComponent
    {
        /// <summary>
        /// this will hold the components in the scene
        /// </summary>
        public List<GameComponent> SceneComponents { get; set; }

        public GameScene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();

            Hide();
        }

        /// <summary>
        /// this will hide the game scene and components
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;

            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }

        /// <summary>
        /// shows all components of gamescene
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        /// <summary>
        /// adds all the scene components into the framework
        /// </summary>
        public override void Initialize()
        {
            foreach (GameComponent component in SceneComponents)
            {
                if (Game.Components.Contains(component) == false)
                {
                    Game.Components.Add(component);
                }
            }


            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
