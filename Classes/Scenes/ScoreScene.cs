using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Classes
{
    /// <summary>
    /// Scene for displaying the score after a game or when provoked on the menu
    /// </summary>
    class ScoreScene : GameScene
    {
        const string FILE_PATH = "highScores.txt";
        const int SCORES_TO_DRAW = 10;

        //we use this to tell how many to save to file and read from file
        int amountToDraw = 0;

        bool JustFinishedGame = false;
        bool hasDrawnScore = false;
        MovingText score;
        List<HighScore> scores = new List<HighScore>();

        List<string> defaultHighScores = new List<string>() { "John - 62", "Ben - 6", "Fred - 22", "Cody - 34", "Arnold - 18", "Alien Farmer - 70" };
        public ScoreScene(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            if(!File.Exists(FILE_PATH))
            {
                StreamWriter writer = new StreamWriter(FILE_PATH);
                using (writer)
                {
                    foreach (string str in defaultHighScores)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // if user hits enter return to main menu
            if(Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Game.Services.GetService<Game1>().HideAllScenes();
                Game.Services.GetService<Game1>().NewGame();
                Game.Services.GetService<MainMenuScene>().Show();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws scores from a run and high scores for comparison
        /// </summary>
        public void DrawRunScore()
        {
            SceneComponents.Clear();
            BaseTexture2D background = new BaseTexture2D(Game, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), "Images/highscore");
            this.SceneComponents.Add(background);
            Game.Components.Add(background);
            scores = new List<HighScore>();
            if (JustFinishedGame)
            {
                if (!hasDrawnScore)
                {
                    scores.Add(new HighScore("You", ScoreManager.Score));
                    hasDrawnScore = true;
                }
                score = new MovingText(Game, "fonts/regularFont", $"Your Score this run: {ScoreManager.Score}", new Vector2(210, Game.GraphicsDevice.Viewport.Height  - 50), Color.White, false);
                this.SceneComponents.Add(score);
                Game.Components.Add(score);
            }
            DrawHighScores();
            UpdateHighScoreFile();
        }

        /// <summary>
        /// Draws the high scores from file
        /// </summary>
        private void DrawHighScores()
        {
            if(!JustFinishedGame)
            {
                BaseTexture2D background = new BaseTexture2D(Game, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), "Images/highscore");
                this.SceneComponents.Add(background);
                Game.Components.Add(background);

            }
            if (File.Exists(FILE_PATH))
            {
                StreamReader reader = new StreamReader(FILE_PATH);
                using (reader)
                {
                    string record = reader.ReadLine();
                    while(record != null)
                    {
                        scores.Add(HighScore.ToHighScore(record));
                        record = reader.ReadLine();
                    }
                }
                scores.Sort();
                scores.Reverse();

                score = new MovingText(Game, "fonts/regularFont", $"High Scores", new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 50), Color.White);
                this.SceneComponents.Add(score);
                Game.Components.Add(score);

                //if there is more than 10 scores just take 10 to draw
                if(SCORES_TO_DRAW < scores.Count)
                {
                    amountToDraw = 10;
                }
                else
                {
                    amountToDraw = scores.Count;
                }
                for (int i = 0; i < 3; i++)
                {
                    score = new MovingText(Game, "fonts/regularFont", $"{i + 1}. {scores[i]}", new Vector2(360, (40 + (52 * (i + 1)))), Color.White, trueCenter: false);
                    this.SceneComponents.Add(score);
                    Game.Components.Add(score);
                }

                for (int i = 3; i < amountToDraw; i++)
                {
                    score = new MovingText(Game, "fonts/regularFont", $"{i + 1}. {scores[i]}", new Vector2(360, (120 + (26 * (i + 1)))), Color.White, trueCenter: false);
                    this.SceneComponents.Add(score);
                    Game.Components.Add(score);
                }
            }
            else
            {
                score = new MovingText(Game, "fonts/regularFont", $"Failed to load high scores.", new Vector2(Game.GraphicsDevice.Viewport.Width / 2, (50 + 26)), Color.White);
            }
        }

        /// <summary>
        /// When this scene is called to display, it runs the commands for drawing the scores
        /// </summary>
        public void ShowAndDisplayScore(bool justFinishedGame = false)
        {
            JustFinishedGame = justFinishedGame;
            DrawRunScore();
            Show();
        }

        /// <summary>
        /// Writes all the scores to file and overwrites it
        /// </summary>
        private void UpdateHighScoreFile()
        {
            if(File.Exists(FILE_PATH))
            {
                StreamWriter writer = new StreamWriter(FILE_PATH);
                using(writer)
                {
                    for (int i = 0; i < amountToDraw; i++)
                    {
                        writer.WriteLine(scores[i].ToString());
                    }
                }
            }
        }
    }
}
