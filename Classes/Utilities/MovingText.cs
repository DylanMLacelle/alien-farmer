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
    /// <summary>
    /// Transitions for moving text
    /// </summary>
    public enum MovingTextTransitions {
        None,
        StraightLine
    }


    /// <summary>
    /// Allows for fontsprites to have basic movement / animations
    /// </summary>
    class MovingText : DrawableGameComponent
    {
        Vector2 Position { get; set; }
        Vector2 StartPosition { get; }
        Vector2 EndPosition { get; }
        Vector2 TargetDirection { get; }
        MovingTextTransitions Transition { get; }

        SpriteFont Font;
        Color Color = Color.White;
        string FontFilePath;
        public string TextToWrite { get; set; }

        /// <summary>
        ///  Instantiates a static text
        /// </summary>
        /// <param name="filePath">The file path for the SpriteFont</param>
        /// <param name="fontContext">Displayed on the viewport</param>
        /// <param name="location">The location of the static text</param>
        public MovingText(Game game, string filePath, string fontContext, Vector2 location, Color color, bool trueCenter = true) : base(game)
        {
            FontFilePath = filePath;
            Transition = MovingTextTransitions.None;
            TextToWrite = fontContext;
            //Load content so the Font gets loaded for the position calculation
            LoadContent();
            if(trueCenter)
            {
                Position = new Vector2(location.X - Font.MeasureString(TextToWrite).Length() / 2, location.Y);
            }
            else
            {
                Position = new Vector2(location.X / 2, location.Y);
            }
            Color = color;
        }

        /// <summary>
        /// Instantiates a static text with a transition 
        /// </summary>
        /// <param name="filePath">The file path for the SpriteFont</param>
        /// <param name="fontContext">Displayed on the viewport</param>
        /// <param name="startLocation">The starting location of the text</param>
        /// <param name="endLocation">The ending location of the text</param>
        /// <param name="transition">The transition effect for the text</param>
        /// <param name="color">Color of the text</param>
        public MovingText(Game game, string filePath, string fontContext, Vector2 startLocation, Vector2 endLocation, MovingTextTransitions transition, Color color) : base(game)
        {
            FontFilePath = filePath;
            TextToWrite = fontContext;
            //Load content so the Font gets loaded for the position calculation
            LoadContent();
            StartPosition = new Vector2(startLocation.X - Font.MeasureString(TextToWrite).Length() / 2, startLocation.Y);
            Position = startLocation;
            EndPosition = endLocation;
            Transition = transition;
            TargetDirection = (EndPosition - StartPosition);
            Color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.DrawString(Font, TextToWrite, Position, Color);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            switch (Transition)
            {
                case MovingTextTransitions.None:
                    break;
                case MovingTextTransitions.StraightLine:
                    if (Position.X <= EndPosition.X && Position.Y <= EndPosition.Y)
                    {
                        Position += new Vector2(TargetDirection.X * 0.01f, TargetDirection.Y * 0.01f);
                        Position = new Vector2(MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Font.MeasureString(TextToWrite).Length()),
                        MathHelper.Clamp(Position.Y, 0, GraphicsDevice.Viewport.Height - Font.LineSpacing));
                    }
                    break;
                default:
                    throw new NotImplementedException("This MovingTextTransition is not Implemented.");
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Font = Game.Content.Load<SpriteFont>(FontFilePath);
            base.LoadContent();
        }

        /// <summary>
        /// Sets the color of the text
        /// </summary>
        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}
