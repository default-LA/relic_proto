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
    /// 
    public class Allies : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<AllyComponent> allies;
        public int maximum;
        public int currentallies;
        private int oldallies;
        public int iMapX;
        public int iMapY;
        int offsetX;
        int offsetY;
        int formation;
        int playerLevel;
        public int[] playerposition;
        public int[,] iMap;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        KeyboardState ksKeyboard;
        KeyboardState ksOldkeyboard;
        public List<MobComponent> mobs;
        string race;

        public bool nextTo;
        public Vector2 nearestTent;//Only correct if nextTo is true

        //Smooth Scrolling
        int iMapXOffset;
        int iMapYOffset;

        public Allies(Game game, SpriteBatch spriteBatch, Texture2D allySprite, int playerLevel, int[,] Map)
            : base(game)
        {
            iMap = Map;
            this.playerLevel = playerLevel;
            sprite = allySprite;
            this.spriteBatch = spriteBatch;
            maximum = 4;
            allies = new List<AllyComponent>();
            allies.Capacity = maximum;
            currentallies = 0;
            oldallies = 0;
            formation = 1;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //CheckInput(); Now from campaignscreen
            CheckAlive();

            if (currentallies > oldallies & (currentallies <= maximum))
            {
                oldallies = currentallies;
                if (oldallies % 4 == 1)
                {
                    offsetX = 1;
                    offsetY = 1;
                }
                if (oldallies % 4 == 2)
                {
                    offsetX = -1;
                    offsetY = 1;
                }
                if (oldallies % 4 == 3)
                {
                    offsetX = 1;
                    offsetY = -1;
                }
                if (oldallies % 4 == 0)
                {
                    offsetX = -1;
                    offsetY = -1;
                }
                AllyComponent tempAlly = new AllyComponent(this.Game, offsetX, offsetY, playerLevel, iMap, nearestTent, spriteBatch, sprite);
                allies.Add(tempAlly);
            }

            foreach (AllyComponent thisAlly in allies)
            {
                if (thisAlly.alive == true)
                {
                    thisAlly.formation = formation;
                    thisAlly.mobs = mobs;
                    thisAlly.allies = allies;
                    thisAlly.Update(gameTime);
                    thisAlly.playerposition = playerposition;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (AllyComponent thisAlly in allies)
            {
                if (thisAlly.alive == true)
                {
                    //spriteBatch.Draw(sprite, new Vector2(((thisAlly.drawposition.X) - ((iMapX * 40) + iMapXOffset)), ((thisAlly.drawposition.Y) - ((iMapY * 40) + iMapYOffset))), Color.White);
                    spriteBatch.Draw(thisAlly.aniSprite.spritesheet, new Rectangle((int)((thisAlly.drawposition.X) - ((iMapX * 40) + iMapXOffset)), (int)((thisAlly.drawposition.Y) - ((iMapY * 40) + iMapYOffset)), 40, 40), thisAlly.aniSprite.imageToDraw, Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void CheckAlive()
        {
            foreach (AllyComponent thisAlly in allies)
            {
                if (thisAlly.alive == true)
                {
                    if (thisAlly.Health[0] <= 0)
                    {
                        thisAlly.alive = false;
                        maximum += 1;
                    }
                }
            }

        }

        public int CheckInput(int test)
        {
            ksKeyboard = Keyboard.GetState();
            if (test > 2) //IE at least 3!
            {
                if ((ksKeyboard.IsKeyDown(Keys.Space)) & (!ksOldkeyboard.IsKeyDown(Keys.Space)) & (nextTo))
                {
                    if (currentallies < maximum)
                    {
                        test -= 3; 
                        currentallies += 1;
                    }
                }

                if (ksKeyboard.IsKeyDown(Keys.Q) & !ksOldkeyboard.IsKeyDown(Keys.Q))
                {
                    if (formation == 1)
                        formation = 2;
                    else
                        formation = 1;
                }
            }
            ksOldkeyboard = ksKeyboard;
            return test;
        }

        public void updatedOffset(int iMapXOffset, int iMapYOffset)
        {
            this.iMapXOffset = iMapXOffset;
            this.iMapYOffset = iMapYOffset;
        }

    }
}