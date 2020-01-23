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
    class ScoreManager : GameComponent
    {
        public static int Score { get; private set; }
        MovingText scoreText;
        string currentScoreText;
        List<GameComponent> gatheredPlants = new List<GameComponent>();

        public ScoreManager(Game game) : base(game)
        {
            Initialize();
        }

        public override void Initialize()
        {
            Score = 0;
            scoreText = new MovingText(Game, "Fonts/regularfont", "Score: 0", new Vector2(Game.GraphicsDevice.Viewport.Width / 2 + 200, 10), Color.White);
            Game.Services.GetService<GameplayScene>().SceneComponents.Add(scoreText);
            Game.Components.Add(scoreText);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            currentScoreText = $"Score: {Score}";
            scoreText.TextToWrite = currentScoreText; 
            base.Update(gameTime);
        }

        /// <summary>
        /// Add amount of points to score
        /// </summary>
        private static void AddPointToScore(int amount)
        {
            Score += amount;
        }

        /// <summary>
        /// Calculates the score 
        /// </summary>
        public void CalculateScore(CollectionBoxes box)
        {
            gatheredPlants = Game.Services.GetService<Player>().itemsInInventory;
            foreach (GameComponent item in gatheredPlants)
            {
                if (box.IsMatchingItemType(item))
                {
                    AddPointToScore(2);
                }
                else
                {
                    AddPointToScore(-2);
                }
            }
            if(Score < 0)
            {
                Score = 0;
            }
        }

        /// <summary>
        /// Finish the game and load a screen to display score breakdown
        /// </summary>
        public void EndGame()
        {
            //load score scene
            Game.Services.GetService<Game1>().HideAllScenes();
            Game.Services.GetService<ScoreScene>().ShowAndDisplayScore(true);
        }
    }
}
