using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class tent
    {
        public Vector2 position;

        public tent(int x, int y)
        {
            x = ((x * 40) - 40);//The tent is bigger then the tile so we need to
            y = ((y * 40) - 40);//calculate the top left coner for the tent sprite
            position = new Vector2((float)x, (float)y);
        }

        public int getX()
        {
            return Convert.ToInt32(position.X / 40);
        }

        public int getY()
        {
            return Convert.ToInt32(position.Y / 40);
        }
    }
}
