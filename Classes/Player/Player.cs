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
    public enum PlayerAnimationState
    {
        None,
        MovingLeft,
        MovingRight,
        GatheringLeft,
        GatheringRight
    }

    class Player : DrawableGameComponent
    {
        private Texture2D idleTexture;
        private Texture2D gatherTexture;
        private Texture2D walkTexture1;
        private Texture2D walkTexture2;
        private bool walkTexture1Active = true;
        public Rectangle collider;

        PlayerAnimationState playerAnimationState = PlayerAnimationState.None;
        private double walkAnimTimer = 0;
        private double gatherAnimTimer = 0;
        private double WALK_ANIMATION_TIMER_INTERVAL = 0.3;
        //Gathering speed (Idk if this is fun, should I change it?, set to 0 for now so gathering is instant)
        private double GATHER_ANIMATION_TIMER_INTERVAL = 0;
        
        private Vector2 curPosition;
        private Vector2 walkSpeed;

        //Player will hold inventory items in this list (Should I change this?)
        public List<GameComponent> itemsInInventory;
        const int MAX_ITEMS_IN_INVENTORY = 3;

        //Sound
        BaseSound gatherSound;
        Random random = new Random();
        BaseSound footStepSound;
        BaseSound insertSound;

        KeyboardState oldKSState;

        public Player(Game game) : base(game)
        {
            walkSpeed = new Vector2(6, 0);
            itemsInInventory = new List<GameComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            PlayCurrentStateAnimation();
            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            UpdateTimers(gameTime);

            if (ks.IsKeyDown(Keys.Right) && (!playerAnimationState.Equals(PlayerAnimationState.GatheringRight) || !playerAnimationState.Equals(PlayerAnimationState.GatheringLeft)))
            {
                playerAnimationState = PlayerAnimationState.MovingRight;
                curPosition += walkSpeed;
                walkAnimTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(ks.IsKeyDown(Keys.Left) && (!playerAnimationState.Equals(PlayerAnimationState.GatheringLeft) || !playerAnimationState.Equals(PlayerAnimationState.GatheringRight)))
            {
                playerAnimationState = PlayerAnimationState.MovingLeft;
                curPosition -= walkSpeed;
                walkAnimTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(ks.IsKeyDown(Keys.Space) && oldKSState.IsKeyUp(Keys.Left))
            {
                Gathering(PlayerAnimationState.GatheringLeft, gameTime);
            }
            else if (ks.IsKeyDown(Keys.Space) && oldKSState.IsKeyUp(Keys.Right))
            {
                Gathering(PlayerAnimationState.GatheringRight, gameTime);
            }
            else
            {
                playerAnimationState = PlayerAnimationState.None;
            }

            if (PlantManager.GetPlantCollisions(collider) != null)
            {
                PlantManager.GetPlantCollisions(collider).GetColor();
            }

            oldKSState = ks;
            curPosition.X = MathHelper.Clamp(curPosition.X, 0, GraphicsDevice.Viewport.Width - idleTexture.Width);
            collider.Location = new Point(curPosition.ToPoint().X + idleTexture.Width / 4, curPosition.ToPoint().Y);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            idleTexture = Game.Content.Load<Texture2D>("Images/playerIdle");
            gatherTexture = Game.Content.Load<Texture2D>("Images/playerGather");
            walkTexture1 = Game.Content.Load<Texture2D>("Images/playerWalk1");
            walkTexture2 = Game.Content.Load<Texture2D>("Images/playerWalk2");
            curPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - idleTexture.Width, GraphicsDevice.Viewport.Height - idleTexture.Height);
            collider = new Rectangle(new Point(0,0), new Point(idleTexture.Bounds.Width / 2, idleTexture.Bounds.Height));
            DrawOrder = 5;
            base.LoadContent();
        }


        /// <summary>
        /// Draws the animation for the corresponding animation state
        /// </summary>
        void PlayCurrentStateAnimation()
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            switch (playerAnimationState)
            {
                case PlayerAnimationState.None:
                    sb.Draw(idleTexture, new Vector2(curPosition.X, Foreground.ForegroundOffset.Y - idleTexture.Height), Color.White);
                    break;
                case PlayerAnimationState.MovingLeft:
                    sb.Draw(walkTexture1Active ? walkTexture1 : walkTexture2, new Vector2(curPosition.X, Foreground.ForegroundOffset.Y - idleTexture.Height), null, null, null, 0f, null, Color.White, SpriteEffects.FlipHorizontally, 0);
                    break;
                case PlayerAnimationState.MovingRight:
                    sb.Draw(walkTexture1Active ? walkTexture1 : walkTexture2, new Vector2(curPosition.X, Foreground.ForegroundOffset.Y - idleTexture.Height), Color.White);
                    break;
                case PlayerAnimationState.GatheringLeft:
                    sb.Draw(gatherTexture, new Vector2(curPosition.X, Foreground.ForegroundOffset.Y - gatherTexture.Height), color:Color.White, effects:SpriteEffects.FlipHorizontally);
                    break;
                case PlayerAnimationState.GatheringRight:
                    sb.Draw(gatherTexture, new Vector2(curPosition.X, Foreground.ForegroundOffset.Y - gatherTexture.Height), Color.White);
                    break;
                default:
                    // If no state just use idle animation
                    sb.Draw(idleTexture, curPosition, Color.White);
                    break;
            }
        }

        /// <summary>
        /// Checks if the passed box collider is touching with players collider for item insertion
        /// </summary>
        private void InsertItemsFromHandToBox(CollectionBoxes box)
        {
            if (InventoryItem.InventoryItems > 0)
            {
                foreach (GameComponent component in itemsInInventory)
                {
                    if (component is InventoryItem)
                    {
                        // we have to cycle through scene componnents to find out which box 
                        box.Insert(component);
                        Game.Components.Remove(component);
                        insertSound = new BaseSound(Game, "Sounds/insert");
                        insertSound.PlaySound(0.02f);
                    }
                }
                // We reset the static varaible in the inventory item class so it draws on the correct position in screen
                Game.Services.GetService<ScoreManager>().CalculateScore(box);
                InventoryItem.InventoryItems = 0;
                itemsInInventory = new List<GameComponent>();
            }
        }

        /// <summary>
        /// Update the timers for update
        /// </summary>
        private void UpdateTimers(GameTime gameTime)
        {
            if (walkAnimTimer >= WALK_ANIMATION_TIMER_INTERVAL)
            {
                walkTexture1Active = !walkTexture1Active;
                PlayFootStepSound();
                walkAnimTimer = 0;
            }
            if(gatherAnimTimer >= GATHER_ANIMATION_TIMER_INTERVAL)
            {
                gatherAnimTimer = 0;
            }
        }
        /// <summary>
        /// Player attempts to gather a plant
        /// </summary>
        /// <param name="gatheringDirection">The direction the gathering animation should be played</param>
        private void Gathering(PlayerAnimationState gatheringDirection, GameTime gameTime)
        {
            playerAnimationState = gatheringDirection;
            gatherAnimTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (gatherAnimTimer >= GATHER_ANIMATION_TIMER_INTERVAL)
            {
                //If we are colliding with a plant
                if (PlantManager.GetPlantCollisions(collider) != null)
                {
                    if (itemsInInventory.Count < MAX_ITEMS_IN_INVENTORY)
                    {
                        InventoryItem item = PlantManager.GetPlantCollisions(collider).Gather();
                        Game.Services.GetService<GameplayScene>().SceneComponents.Add(item);
                        Game.Components.Add(item);
                        itemsInInventory.Add(item);
                        PlayGatherSound();
                    }
                }
                //else If we are colliding with a collection box
                else if (collider.Intersects(Game.Services.GetService<GoodCollectionBox>().GetCollider()))
                {
                    InsertItemsFromHandToBox(Game.Services.GetService<GoodCollectionBox>());
                }
                else if (collider.Intersects(Game.Services.GetService<BadCollectionBox>().GetCollider()))
                {
                    InsertItemsFromHandToBox(Game.Services.GetService<BadCollectionBox>());
                }
            }
        }

        private void PlayGatherSound()
        {
            int soundFile = random.Next(1, 3);
            gatherSound = new BaseSound(Game, $"Sounds/gather{soundFile}");
            gatherSound.PlaySound(0.9f);
        }

        private void PlayFootStepSound()
        {
            int soundFile = random.Next(0, 10);
            gatherSound = new BaseSound(Game, $"Sounds/footstep0{soundFile}");
            gatherSound.PlaySound(0.2f);
        }
    }
}
