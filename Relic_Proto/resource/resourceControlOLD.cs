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
    public class ResourceConrolOLD : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<ResourceComponent> tiles;
        public int currentTile;
        public int iMapX;
        public int iMapY;
        public int[] playerposition;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        public int iMapHeight;
        public int iMapWidth;
        int[,] Map;

        public ResourceConrolOLD(Game game, Texture2D sprite, SpriteBatch spriteBatch, int[,] Map)
            : base(game)
        {
            this.Map = Map;
            this.sprite = sprite;
            this.spriteBatch = spriteBatch;
            tiles = new List<ResourceComponent>();
            PopulateTiles();
            tiles.Count();
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
            foreach (ResourceComponent thisTile in tiles)
            {
                //thisTile.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ResourceComponent thisTile in tiles)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(sprite, new Rectangle((((thisTile.position[0] * 40) - ((iMapX * 40)))),
                         ((thisTile.position[1] * 40) - ((iMapY * 40))), 40, 40), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public void PopulateTiles()
        {
            ResourceComponent thisTile;
            int[] position = new int[2];

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (Map[y, x] == 67)
                    {
                        position[0] = y;
                        position[1] = x;
                        thisTile = new ResourceComponent(Game, position);
                        tiles.Add(thisTile);
                    }
                }
            }
        }
    }
}