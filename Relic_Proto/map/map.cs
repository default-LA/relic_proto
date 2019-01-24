using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relic_Proto
{
    //This class stores maps for use in the game. 
    public class mapHolder
    {
        private List<map> maps;

        public mapHolder()
        {
            maps = new List<map>();
        }

        public void addMap(map map)
        {
            maps.Add(map);
        }

        public map newMap(int random)
        {
            return maps[random ];
        }

        public int total()
        {
            return maps.Count() - 1;
        }
    }

    public class map
    {
        private int[,] iMap;
        private int[] position;

        public map(int[,] map, int[] position)
        {
            this.iMap = map;
            this.position = position;
        }

        public int[,] returnMap()
        {
            return iMap;
        }

        public int[] returnPosition()
        {
            return position;
        }
    }
}
