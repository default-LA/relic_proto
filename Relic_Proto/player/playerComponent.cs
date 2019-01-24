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
    public class playerComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int Level;
        public int Strength;
        public int Defence;
        public int Wisdom;
        public int Resource = 0;
        public int manaPot;
        public int healthPot;
        public int[] Health = new int[2];
        public int[] Mana = new int[2];
        public int Experience;
        public String Race;
        public String Class;
        public String[] Attributes = new String[10];
        public int[] items = new int[4];
        public int LevelGain;

        public playerComponent(Game game)
            : base(game)
        {
            Level = 1;
            LevelGain = 0;
            Strength = 10;
            Defence = 10;
            Wisdom = 10;
            Resource = 9;
            healthPot = 2;
            manaPot = 2;
            Health[0] = (100 + (Defence * 15));
            Health[1] = (100 + (Defence * 15));
            Mana[0] = Wisdom * 15;
            Mana[1] = Wisdom * 15;
            //Item Code
            items[0] = 0;
            items[1] = 0;
            items[2] = 0;
            items[3] = 0;
            //Item Code
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
            // TODO: Add your update code here
            if (Experience >= (1000 * Level)) 
            {
                LevelUp();
            }

            Attributes[0] =  Level.ToString();
            Attributes[1] =  Health[0].ToString();
            Attributes[2] =  Health[1].ToString();
            Attributes[3] =  Strength.ToString();
            Attributes[4] =  Defence.ToString();
            Attributes[5] =  Wisdom.ToString();
            Attributes[6] = Experience.ToString(); 
            Attributes[7] = Resource.ToString();
            Attributes[8] = healthPot.ToString();
            Attributes[9] = manaPot.ToString();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void LevelUp()
        {
            Level += 1;
            LevelGain += 1;
            Experience = 0;
        }

        public int removeItem(int slot) //Item Code
        {
            if (slot != -1)
            {
                items[slot] = 0;
                for (int i = slot; i < 3; i++)
                {
                    items[i] = 0;
                    items[i] = items[i + 1];
                }
                items[3] = 0;
            }
            return -1;
        }
    }
}