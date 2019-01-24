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
    public class EnemyControl : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<MobComponent> mobs;
        public List<AllyComponent> allies = new List<AllyComponent>();
        public int currentmobs;
        public int iMapX;
        public int iMapY;
        public int[] playerposition;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        Texture2D bossSprite;
        public int iMapHeight;
        public int iMapWidth;
        int[,] iMap;
        Random random = new Random();
        int playerLevel;

        //Smooth Scrolling
        int iMapXOffset;
        int iMapYOffset;

        public EnemyControl(Game game, Texture2D sprite, Texture2D bossSprite, SpriteBatch spriteBatch, int playerLevel, int[,] Map)
            : base(game)
        {
            iMap = Map;
            this.playerLevel = playerLevel;
            this.sprite = sprite;
            this.bossSprite = bossSprite;
            this.spriteBatch = spriteBatch;
            mobs = new List<MobComponent>();
            PopulateMobs();

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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (MobComponent thisMob in mobs)
            {
                thisMob.mobs = mobs;
                thisMob.allies = allies;
                thisMob.iMapX = iMapX;
                thisMob.iMapY = iMapY;
                thisMob.playerposition = playerposition;
                thisMob.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (MobComponent thisAlly in mobs)
            {
                if (thisAlly.alive == true)
                {
                    //spriteBatch.Draw(sprite, new Vector2(((thisAlly.drawposition.X) - ((iMapX * 40) + iMapXOffset)), ((thisAlly.drawposition.Y) - ((iMapY * 40) + iMapYOffset))), thisAlly.colour);
                    spriteBatch.Draw(thisAlly.aniSprite.spritesheet, new Rectangle((int)((thisAlly.drawposition.X) - ((iMapX * 40) + iMapXOffset)), (int)((thisAlly.drawposition.Y) - ((iMapY * 40) + iMapYOffset)), 40, 40), thisAlly.aniSprite.imageToDraw, thisAlly.colour);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void PopulateMobs()
        {
            int total;
            total = RandomNumber(30, 50);
            for (int i = 0; i <= total; i++)
            {
                MobComponent thisMob;
                int[] position = new int[2];
                position[0] = RandomNumber(10,90);
                position[1] = RandomNumber(10,90);
                switch (iMap[position[0], position[1]])
                {
                    case 6:
                    case 8:
                    case 9:
                    case 11:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 49:
                    case 50:
                    case 51:
                    case 53:
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 68:
                    case 70:
                    case 71:
                    case 77:
                    case 81:
                        //Not safe to place a mob
                        break;
                    default:
                        //Safe to place a mob
                        thisMob = new MobComponent(Game, position, playerLevel, iMap, false, spriteBatch, sprite);
                        thisMob.sprite = sprite;
                        mobs.Add(thisMob);
                        break;
                }
                
            }

            
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    switch (iMap[y, x])
                    {
                        case 67:
                            MobComponent newMob;
                            int[] newMobPosition = new int[2];
                            //Add a normal mob
                            newMobPosition[0] = x;
                            newMobPosition[1] = y;
                            newMob = new MobComponent(Game, newMobPosition, playerLevel, iMap, false, spriteBatch, sprite);
                            newMob.sprite = sprite;
                            mobs.Add(newMob);
                            break;
                        case 80:
                            MobComponent newBoss;
                            int[] newBossPosition = new int[2];
                            //Add a boss
                            newBossPosition[0] = x;
                            newBossPosition[1] = y - 1;//Below the tile
                            newBoss = new MobComponent(Game, newBossPosition, playerLevel, iMap, true, spriteBatch, bossSprite);
                            newBoss.sprite = bossSprite;
                            mobs.Add(newBoss);
                            break;
                    }
                }
            }
        }

        public int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public void updatedOffset(int iMapXOffset, int iMapYOffset)
        {
            this.iMapXOffset = iMapXOffset;
            this.iMapYOffset = iMapYOffset;
        }
    }
}