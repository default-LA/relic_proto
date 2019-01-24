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
    public class item
    {
        public int Str;
        public int End;
        public int Wis;
        public String name;
        public Texture2D sprite;
        public bool isSelected;
        public Color colour;

        public item(String name, int Str, int End, int Wis)
        {
            this.name = name;
            this.Str = Str;
            this.End = End;
            this.Wis = Wis;
            colour = Color.White;
        }
        public item(String name, int Str, int End, int Wis, Color colour)
        {
            this.name = name;
            this.Str = Str;
            this.End = End;
            this.Wis = Wis;
            this.colour = colour;
        }

        public item(String name, int Str, int End, int Wis, Texture2D sprite, Color solour)
        {
            this.name = name;
            this.Str = Str;
            this.End = End;
            this.Wis = Wis;
            this.sprite = sprite;
        }


    }
}
