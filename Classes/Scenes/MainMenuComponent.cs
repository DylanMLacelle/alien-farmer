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
    class MainMenuComponent : DrawableGameComponent
    {
        SpriteFont regularFont;
        SpriteFont selectedFont;

        private List<string> menuOptions;
        private int SelectedIndex { get; set; }
        private Vector2 position;

        private Color regularColor = Color.Black;
        private Color selectedColor = Color.White;

        KeyboardState oldState;

        public MainMenuComponent(Game game, List<string> menuItems) : base(game)
        {
            menuOptions = menuItems;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 tempPos = position;

            sb.Begin();

            for (int i = 0; i < menuOptions.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                if(SelectedIndex == i)
                {
                    activeFont = selectedFont;
                    activeColor = selectedColor;
                }

                sb.DrawString(activeFont, menuOptions[i], tempPos, activeColor);

                //update the position of the next string
                tempPos.Y += regularFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            position = new Vector2(GraphicsDevice.Viewport.Width - 250,
                                50);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if(SelectedIndex == menuOptions.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if(ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if(SelectedIndex == -1)
                {
                    SelectedIndex = menuOptions.Count - 1;
                }
            }
            oldState = ks;

            if(ks.IsKeyDown(Keys.Enter))
            {
                LoadSelectedScene();
            }


            base.Update(gameTime);
        }

        private void LoadSelectedScene()
        {
            //load scene based on MenuOptions enum
            ((Game1)Game).HideAllScenes();

            switch((MenuSelection)SelectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<GameplayScene>().Show();
                    break;
                case MenuSelection.Quit:
                    Game.Exit();
                    break;
                case MenuSelection.Tutorial:
                    Game.Services.GetService<TutorialScene>().Show();
                    break;
                case MenuSelection.HighScore:
                    Game.Services.GetService<ScoreScene>().ShowAndDisplayScore(false);
                    break;
                case MenuSelection.Credit:
                    Game.Services.GetService<CreditScene>().Show();
                    break;
                default:
                    Game.Services.GetService<MainMenuScene>().Show();
                    break;
            }
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            selectedFont = Game.Content.Load<SpriteFont>("Fonts/selectedFont");
            base.LoadContent();
        }
    }
}
