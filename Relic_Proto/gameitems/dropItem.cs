using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Relic_Proto
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class dropItem : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int itemNumber;
        public Vector2 position;
        public Color colour;
        public MouseState oldMouse;
        public bool isNextTo;
        public bool isSelected;
        public bool isPickedUp;
        public Vector2 iMap;

        public dropItem(Game game, int itemNumber, int X, int Y)
            : base(game)
        {
            this.itemNumber = itemNumber;
            position.X = X * 40;
            position.Y = Y * 40;
            colour = Color.White;
            isNextTo = false;
            isSelected = false;
            isPickedUp = false;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            CheckSelected();
            base.Update(gameTime);
        }

        public void CheckSelected()
        {
            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();
            if ((curMouseState.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released) && (isNextTo))
            {
                if (((position.X - iMap.X) < curMouseState.X) &&
                 (((position.X + 40) - iMap.X) > curMouseState.X) &&
                 ((position.Y - iMap.Y) < curMouseState.Y) &&
                 (((position.Y + 40) - iMap.Y) > curMouseState.Y))
                {
                    isSelected = true;
                    if (curKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        isPickedUp = true;
                    }
                }

                else
                {
                    isSelected = false;
                }
            }
            oldMouse = curMouseState;
        }

        public int getX()
        {
            return (int)(this.position.X / 40);
        }

        public int getY()
        {
            return (int)(this.position.Y / 40);
        }

    }
    }