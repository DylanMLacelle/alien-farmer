using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Game1.Classes
{
    public enum MenuSelection
    {
        StartGame,
        Tutorial,
        HighScore,
        Credit,
        Quit
    }

    class MainMenuScene : GameScene
    {
        List<string> menuOptions = new List<string>(new string[] {"Play Game", "Tutorial", "High Scores", "Credits", "Quit" });
        public MainMenuScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            BaseTexture2D background = new BaseTexture2D(Game, Vector2.Zero, "Images/mainmenu");
            this.SceneComponents.Add(background);
            this.SceneComponents.Add(new MainMenuComponent(Game, menuOptions));
            this.Show();

            BaseSong song = new BaseSong(Game, "Music/bgSong");
            this.SceneComponents.Add(song);
            Game.Components.Add(song);

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
