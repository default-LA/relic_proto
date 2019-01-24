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
    public class ResourceComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D sprite;
        public Color colour;
        public int[] playerposition;
        public int[] position;
        public int Strength;
        public int Endurance;
        public int Wisdom;
        public int[] Health = new int[2];
        public double Armour;
        public int aggroRange;
        public int iMapX;
        public int iMapY;
        public bool alive;
        int iMapHeight;
        int iMapWidth;
        int[,] iMap;
        CollisionComponent collision;
        SpriteBatch spriteBatch;

        public ResourceComponent(Game game, int[] tilePosition)
            : base(game)
        {
            this.position = tilePosition;
            alive = true;
            colour = Color.White;
            // collision = new CollisionComponent(game, iMap, position);
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
            //if (DrawMob() & alive)
            //{
            //CheckSelected();
            //CheckAlive();
            //    Movement();
            //}

            base.Update(gameTime);
        }

        public bool DrawTile()
        {
            if ((iMapX <= position[0]) & (iMapY <= position[1]) & (iMapX + iMapWidth >= position[0]) & (iMapY + iMapHeight >= position[1]))
            {
                return true;
            }
            else
                return false;

        }

        public override void Draw(GameTime gameTime)
        {
            if (DrawTile() & alive)
            {

            }
            base.Draw(gameTime);
        }
    }
}