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
    public class dropController : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D closedSprite;
        Texture2D openSprite;
        public List<dropItem> dropList;
        List<item> allItems;
        public Vector2 IMap;
        public int selected;
        MouseState oldMouse;
        Vector2 playerOffest;

        public dropController(Game game, Texture2D closedSprite, Texture2D openSprite, SpriteBatch spriteBatch, List<item> items) //Modified for reader
            : base(game)
        {
            // TODO: Construct any child components here
            this.closedSprite = closedSprite;
            this.openSprite = openSprite;
            this.spriteBatch = spriteBatch;
            dropList = new List<dropItem>();
            allItems = items;
            selected = -1;
            dropList.Add(new dropItem(Game, 2, 5, 5));
        }

        /// <summary>z
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (dropItem thisItem in dropList)
            {
                thisItem.iMap = IMap;
                thisItem.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 position;
            foreach (dropItem thisItem in dropList)
            {
                position = new Vector2(thisItem.position.X - IMap.X - playerOffest.X, thisItem.position.Y - IMap.Y - playerOffest.Y);
                spriteBatch.Begin();
                if (thisItem.isSelected)
                {
                    spriteBatch.Draw(openSprite, position, thisItem.colour);
                }
                else
                {
                    spriteBatch.Draw(closedSprite, position, thisItem.colour);
                }
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public int count(bool mob)//Does not return the Highest value as we don't want the power Sword to drop from mobs. 
        {
            int highest;
            if (mob)
            {
                highest = allItems.Count - 1;
            }
            else
            {
                highest = allItems.Count;
            }
            return highest;
        }

        public void updateIMap(int X, int Y)
        {
            this.IMap.X = X * 40;
            this.IMap.Y = Y * 40;
        }

        public int getStr(int itemNum)
        {
            return allItems[itemNum].Str;
        }

        public int getEnd(int itemNum)
        {
            return allItems[itemNum].End;
        }

        public int getWis(int itemNum)
        {
            return allItems[itemNum].Wis;
        }
        public String getName(int itemNum)
        {
            return allItems[itemNum].name;
        }

        public Color getColour(int itemNum)
        {
            return allItems[itemNum].colour;
        }

        public void dropNewItem(int X, int Y, int randomNum)
        {
            dropList.Add(new dropItem(Game, randomNum, X, Y));
        }

        public bool HUDSelected()
        {
            //Slots are the postions of the Item slots in the HUD
            Vector2[] slot;
            slot = new Vector2[4];
            slot[0] = new Vector2(532, 510);
            slot[1] = new Vector2(576, 510);
            slot[2] = new Vector2(532, 555);
            slot[3] = new Vector2(576, 555);

            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if ((curMouseState.LeftButton == ButtonState.Pressed) && (oldMouse.LeftButton == ButtonState.Released))
                //if (curMouseState.LeftButton == ButtonState.Pressed)
                {
                    if ((slot[i].X < curMouseState.X) &&
                     ((slot[i].X + 40) > curMouseState.X) &&
                     (slot[i].Y < curMouseState.Y) &&
                     ((slot[i].Y + 40) > curMouseState.Y))
                    {

                        if (curKeyboardState.IsKeyDown(Keys.LeftShift))
                        {
                            oldMouse = curMouseState;
                            selected = i;
                            return true;
                        }
                        else
                        {
                            oldMouse = curMouseState;
                            selected = i;
                            return false;
                        }
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            if (count == 4)
            {
                //No item slot selected
                selected = -1;
            }
            oldMouse = curMouseState;
            return false;
        }

        public void updateOffset(Vector2 playerOffest)
        {
            this.playerOffest = playerOffest;
        }
    }
}