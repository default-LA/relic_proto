using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relic_Proto
{
    class resourceControl
    {
        List<resource> tiles;

        public resourceControl(int[,] Map)
        {
            tiles = new List<resource>();
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if (Map[x, y] == 67)
                    {
                        tiles.Add(new resource(x, y));
                    }
                }
            }
        }

        public bool checkResource(int playerX, int playerY)
        {
            bool result;
            result = false;
            foreach (resource thisResource in tiles)
            {
                if (thisResource.check(playerX, playerY))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
