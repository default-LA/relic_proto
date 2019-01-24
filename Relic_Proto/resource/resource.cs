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
    class resource
    {
        Vector2 position;
        int available;

        public resource(int x, int y)
        {
            position.X = x;
            position.Y = y;
            available = 10;
        }

        private int getX()
        {
            return (int)(this.position.X);
        }

        private int getY()
        {
            return (int)(this.position.Y);
        }

        private void lower()
        {
            available = available - 1;
        }

        public bool check(int playerX, int playerY)
        {
            if ((playerX == getX()) & (playerY == getY()) & (available > 0))
            {
                lower();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
