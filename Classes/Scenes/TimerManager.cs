using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1.Classes
{
    class TimerManager : DrawableGameComponent
    {
        const int TIMER_MAX_TIME = 30;
        public static double RemainingTime { get; private set; }
        private bool isTimerUnder10Seconds = false;
        MovingText timerText;
        string currentTimerText;

        //sounds
        BaseSound countdown;
        double soundTimer;

        public TimerManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            countdown = new BaseSound(Game, "Sounds/1");
            RemainingTime = TIMER_MAX_TIME;
            timerText = new MovingText(Game, "Fonts/regularfont", $"{RemainingTime}", new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 10), Color.White);
            Game.Services.GetService<GameplayScene>().SceneComponents.Add(timerText);
            Game.Components.Add(timerText);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            CountdownToZero(gameTime);
            currentTimerText = $"{Math.Round(RemainingTime)}";
            timerText.TextToWrite = currentTimerText;
            base.Update(gameTime);
        }

        /// <summary>
        /// Counts down the timer
        /// </summary>
        private void CountdownToZero(GameTime gt)
        {
            RemainingTime -= gt.ElapsedGameTime.TotalSeconds;
            if (RemainingTime <= 10 && !isTimerUnder10Seconds)
            {
                isTimerUnder10Seconds = true;
                timerText.SetColor(Color.Red);
            }
            if(RemainingTime <= 0)
            {
                Game.Services.GetService<ScoreManager>().EndGame();
            }
            if(RemainingTime <=6 && RemainingTime > 1)
            {
                soundTimer += gt.ElapsedGameTime.TotalSeconds;
                if(soundTimer >= 1)
                {
                    PlayCountDownSounds((int)(RemainingTime));
                    soundTimer = 0;
                }
            }
        }

        private void PlayCountDownSounds(int second)
        {
            countdown = new BaseSound(Game, $"Sounds/{second}");
            countdown.PlaySound(0.5f);
        }


        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
