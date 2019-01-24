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
    public class buildingControl : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<tent> tents;
        int playerX;
        int playerY;
        SpriteBatch spriteBatch;
        Texture2D tentSprite;
        int[,] Map;

        Vector2 iMap; //Stores what tile is displayed  in the top left of the screen.

        //Used to get information to the allies class.
        public bool tentBool;//True if the player is next to a tent
        //public Vector2 nearestTent;//Only correct if nextTo is true

        public buildingControl(Game game, Texture2D tentSprite, SpriteBatch spriteBatch, int[,] Map)
            : base(game)
        {
            this.Map = Map;
            this.tentSprite = tentSprite;
            this.spriteBatch = spriteBatch;
            tents = new List<tent>();
            generateTents();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
             base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (tent thisTent in tents)
            {
                spriteBatch.Draw(tentSprite, new Vector2((thisTent.position.X - iMap.X), (thisTent.position.Y - iMap.Y)), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void updateInfo(Vector2 iMap)
        {
            this.iMap = iMap;
        }

        private void generateTents()
        {
            tents.Count();
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if(Map[x,y] == 77)
                    {
                        tents.Add(new tent(y,x));
                    }
                }
            }
            tents.Count();
        }

        public bool nextToTent(int playerX, int playerY)
        {
            tentBool = false;
            foreach (tent thisTent in tents)
            {
                if ((Math.Abs((thisTent.getX() + 1) - playerX) < 3) & ((Math.Abs(thisTent.getY() - (playerY - 1)) + 1) < 3))
                {
                    tentBool = true;
                }
            }
            return tentBool;
        }

        public Vector2 nearestTent(int playerX, int playerY)
        {
            foreach (tent thisTent in tents)
            {
                if ((Math.Abs((thisTent.getX() + 1) - playerX) < 3) & ((Math.Abs(thisTent.getY() - (playerY - 1)) + 1) < 3))
                {
                    return new Vector2(thisTent.getX(), thisTent.getY());
                }
            }
            return new Vector2(); //Never reached, but stops XNA having a fit
        }
    }
}