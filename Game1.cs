using Game1.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Services.AddService<Game1>(this);

            Classes.MainMenuScene mainMenuScene = new Classes.MainMenuScene(this);
            this.Components.Add(mainMenuScene);
            Services.AddService<Classes.MainMenuScene>(mainMenuScene);

            Classes.GameplayScene gameplayScene = new Classes.GameplayScene(this);
            this.Components.Add(gameplayScene);
            Services.AddService<Classes.GameplayScene>(gameplayScene);

            Classes.TutorialScene tutorialScene = new Classes.TutorialScene(this);
            this.Components.Add(tutorialScene);
            Services.AddService<Classes.TutorialScene>(tutorialScene);

            Classes.ScoreScene scoreScene = new Classes.ScoreScene(this);
            this.Components.Add(scoreScene);
            Services.AddService<Classes.ScoreScene>(scoreScene);

            Classes.CreditScene creditScene = new Classes.CreditScene(this);
            this.Components.Add(creditScene);
            Services.AddService<Classes.CreditScene>(creditScene);

            
            base.Initialize();

            HideAllScenes();
            mainMenuScene.Show();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService<SpriteBatch>(spriteBatch);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                HideAllScenes();
                Services.GetService<Classes.MainMenuScene>().Show();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// Deletes the current gameplay scene and makes a new one
        /// </summary>
        public void NewGame()
        {
            Services.GetService<Classes.GameplayScene>().NewGame();
            //this.Components.Remove(Services.GetService<Classes.GameplayScene>().SceneComponents.Clear());
            //Services.RemoveService(typeof(Classes.GameplayScene));

            //Classes.GameplayScene gameplayScene = new Classes.GameplayScene(this);
            //Services.AddService<Classes.GameplayScene>(gameplayScene);
            //this.Components.Add(gameplayScene);
        }

        /// <summary>
        /// Hides all the game scenes
        /// </summary>
        public void HideAllScenes()
        {
            Classes.GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if(item is Classes.GameScene)
                {
                    gs = (Classes.GameScene)item;
                    gs.Hide();
                }
            }
        }
    }
}
