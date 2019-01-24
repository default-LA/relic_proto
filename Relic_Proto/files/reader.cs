using System;
using System.IO;
using System.Net; //For downloading files
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
    class reader
    {
        Random random = new Random();
        //private const String local = @"C:\project\"; //Must match const in downloader

        public List<item> itemFile(bool online)
        {
            //Comented out for demo
            //if (online)
            //{
            //    downloader update;
            //    update = new downloader();
            //    update.item();
            //}
            List<item> items;
            items = new List<item>();
            if (File.Exists("Content/items.txt"))
            {
                int i = 0;
                StreamReader read = new StreamReader("Content/items.txt");//Change this for a dynamic path.
                bool lineOne = true;
                while (!read.EndOfStream)
                {
                    if (lineOne)
                    {
                        string line = read.ReadLine(); //Read the line and ignore it. Important so that it moves on to the next line. 
                        lineOne = false;
                    }
                    else
                    {
                        string line = read.ReadLine(); //Read the line
                        string[] array = line.Split(',' ); //Split each field
                        Color itemColor = new Color(RandomNumber(1, 200), RandomNumber(1, 200), RandomNumber(1, 200), RandomNumber(1, 200));
                        items.Add(new item(array[0], Convert.ToInt32(array[1]), Convert.ToInt32(array[2]), Convert.ToInt32(array[3]), itemColor));//Add this item to the list. 
                    }
                    i++;

                }
                read.Close();
                return items;
            }
            else
            {
                //massive error the file does not exist
                //exit the game
                return items;
            }
        }

        public mapHolder mapFile(bool online)
        {
            mapHolder maps;
            maps = new mapHolder();
            int i;
            int x;
            List<String> listing;
            listing = new List<String>();
            StreamReader streamReader;
            bool lineOne;

            //Comented out for demo
            //if (online)
            //{
            //  downloader update;
            //    update = new downloader();
            //    update.map();
            //}

            //Open the map listing file
            if (File.Exists("Content/maps/mapListing.txt"))
            {
                streamReader = new StreamReader("Content/maps/mapListing.txt");//Change this for a dynamic path.
                lineOne = true;
                while (!streamReader.EndOfStream)
                {
                    if (lineOne)
                    {
                        string line = streamReader.ReadLine(); //Read the line and ignore it. Important so that it moves on to the next line. 
                        lineOne = false;
                    }
                    else
                    {
                        string line = streamReader.ReadLine(); //Read the line
                        listing.Add(line);
                    }

                }
                streamReader.Close();
            }
            else
            {
                //massive error the file does not exist
                //exit the game
            }

            //open each map from the listing
            foreach (String name in listing)
            {
                if (File.Exists("Content/maps/" + name))
                {
                    int[,] iMap;
                    int[] position;
                    i = 0;
                    lineOne = true;
                    streamReader = new StreamReader("Content/maps/" + name);//Change this for a dynamic path.
                    position = new int[2];
                    iMap = new int[100, 100]; //Possible update to allow for differnt sized maps.
                    while (!streamReader.EndOfStream)
                    {
                        if (lineOne)
                        {
                            string line = streamReader.ReadLine();
                            string[] array = line.Split(',');
                            lineOne = false;
                            position[0] = Convert.ToInt32(array[0]);
                            position[1] = Convert.ToInt32(array[1]);
                        }
                        else
                        {
                            string line = streamReader.ReadLine();
                            string[] array = line.Split(',');
                            x = 0;
                            foreach (String tileCode in array)
                            {
                                iMap[i, x] = Convert.ToInt32(tileCode);
                                x++;
                            }
                            i++;
                        }

                    }
                    maps.addMap(new map(iMap, position));
                }
                else
                {
                    //Error file doesn't exsit, it's okay to contiune as it won't be added to maps. 
                }
            }
            return maps;
        }

        public float RandomNumber(int min, int max)
        {
            return (float)random.Next(min, max);
        }
    }
}