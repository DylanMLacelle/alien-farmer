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
    class GameplayScene : GameScene
    {
        bool firstLoad = true;

        public GameplayScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            if(firstLoad)
            {
                NewGame();
            }
        }

        public void NewGame()
        {
            SceneComponents.Clear();

            Player player = new Player(Game);
            this.SceneComponents.Add(player);

            this.SceneComponents.Add(new PlantManager(Game));
            this.SceneComponents.Add(new FarmPlotManager(Game));
            
            GoodCollectionBox goodCollectionBox = new GoodCollectionBox(Game);
            this.SceneComponents.Add(goodCollectionBox);

            BadCollectionBox badCollectionBox = new BadCollectionBox(Game);
            this.SceneComponents.Add(badCollectionBox);

            GameplaySceneBackgroundAndMusicManager gameplaySceneManager = new GameplaySceneBackgroundAndMusicManager(Game);
            this.SceneComponents.Add(gameplaySceneManager);

            ScoreManager scoreManager = new ScoreManager(Game);
            this.SceneComponents.Add(scoreManager);
            
            TimerManager timerManager = new TimerManager(Game);
            this.SceneComponents.Add(timerManager);
            
            if (firstLoad)
            {
                Game.Services.AddService<Player>(player);
                Game.Services.AddService<ScoreManager>(scoreManager);
                Game.Services.AddService<TimerManager>(timerManager);
                Game.Services.AddService<GoodCollectionBox>(goodCollectionBox);
                Game.Services.AddService<BadCollectionBox>(badCollectionBox);
                Game.Services.AddService<GameplaySceneBackgroundAndMusicManager>(gameplaySceneManager);
            }
            firstLoad = false;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(Enabled)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<MainMenuScene>().Show();
                }
            }

            base.Update(gameTime);
        }
    }
}
