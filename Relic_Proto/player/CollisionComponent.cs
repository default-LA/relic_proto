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
    public class CollisionComponent : Microsoft.Xna.Framework.GameComponent
    {
        int[,] Map = new int[100, 100];
        int[,] Collision;
        int[] position = new int[2];


        public CollisionComponent(Game game, int[,] Map, int[] location)
            : base(game)
        {
            this.Map = Map;
            Collision = new int[100, 100];
            position = location;
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    switch (Map[y, x])
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
                            Collision[y, x] = 1;
                            break;
                        default:
                            Collision[y, x] = 0;
                            break;
                    }
                }
            }
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


            base.Update(gameTime);

        }

        public bool Walkable(Keys theKey)
        {
            if (theKey == Keys.W)
            {
                if (position[1] == 0)
                    return false;
                else if (Collision[position[1] - 1, position[0]] == 1)
                    return false;
            }
            else if (theKey == Keys.S)
            {
                if (position[1] == 99)
                    return false;
                else if (Collision[position[1] + 1, position[0]] == 1)
                    return false;
            }
            else if (theKey == Keys.A)
            {
                if (position[0] == 0)
                    return false;
                else if (Collision[position[1], position[0] - 1] == 1)
                    return false;
            }
            else if (theKey == Keys.D)
            {
                if (position[0] == 99)
                    return false;
                else if (Collision[position[1], position[0] + 1] == 1)
                    return false;
            }
            else
            {
                return true;
            }

            return true;
        }
    }
}