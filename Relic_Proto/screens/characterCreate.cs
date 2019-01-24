using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Relic_Proto
{
    class characterCreateScreen : GameScreen
    {
        Texture2D image;
        Rectangle imageRectangle;
        SpriteFont spriteFont;
        Texture2D elf;
        Texture2D human;
        Texture2D orc;
        Texture2D warrior;
        Texture2D mage;
        Texture2D tank;
        public String selectedClass;
        public String selectedRace;
        public String raceDescription;
        public String classDescription;
        public Texture2D selectedRaceSprite;
        public playerComponent Player;

        public characterCreateScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image, Texture2D human, Texture2D elf, Texture2D orc, Texture2D sword, Texture2D shield, Texture2D staff)
            : base(game, spriteBatch)
        {
            selectedRaceSprite = human;
            Player = new playerComponent(game);
            Components.Add(Player);
            this.human = human;
            this.orc = orc;
            this.elf = elf;
            warrior = sword;
            tank = shield;
            mage = staff;
            this.spriteFont = spriteFont;
            this.image = image;
            imageRectangle = new Rectangle(
                00,
                00,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);
            selectedRace = "None";
            selectedClass = "None";
            raceDescription = "";
            classDescription = "";
        }

        public override void Update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                if ((curMouseState.X > 10 && curMouseState.X < 50) & (curMouseState.Y > 75 & curMouseState.Y < 115))
                {
                    selectedRace = "Human";
                    raceDescription = "- Provides a Defence Bonus on Level Up.";
                    selectedRaceSprite = human;
                }
                else if ((curMouseState.X > 70 && curMouseState.X < 110) & (curMouseState.Y > 75 & curMouseState.Y < 115))
                {
                    selectedRace = "Elf";
                    raceDescription = "- Provides a Wisdom Bonus on Level Up.";
                    selectedRaceSprite = elf;
                }
                else if ((curMouseState.X > 130 && curMouseState.X < 170) & (curMouseState.Y > 75 & curMouseState.Y < 115))
                {
                    selectedRace = "Orc";
                    raceDescription = "- Provides a Strength Bonus on Level Up.";
                    selectedRaceSprite = orc;
                }
                else if ((curMouseState.X > 10 && curMouseState.X < 50) & (curMouseState.Y > 150 & curMouseState.Y < 190))
                {
                    selectedClass = "Knight";
                    classDescription = "- Provides a Defence Bonus on Level Up.";
                }
                else if ((curMouseState.X > 70 && curMouseState.X < 110) & (curMouseState.Y > 150 & curMouseState.Y < 190))
                {
                    selectedClass = "Mage";
                    classDescription = "- Provides a Wisdom Bonus on Level Up.";
                }
                else if ((curMouseState.X > 130 && curMouseState.X < 170) & (curMouseState.Y > 150 & curMouseState.Y < 190))
                {
                    selectedClass = "Warrior";
                    classDescription = "- Provides a Strength Bonus on Level Up.";
                }
            }

            CalculateStats();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, imageRectangle, Color.White);
            spriteBatch.DrawString(spriteFont, "Use the cursor to select a Race and Class for your Hero.", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(spriteFont, "Race:", new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(spriteFont, selectedRace , new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(spriteFont, raceDescription , new Vector2(120, 50), Color.White);
            spriteBatch.Draw(human, new Rectangle(10, 75, 40, 40), new Rectangle(0, 0, 40, 40), Color.White);
            spriteBatch.Draw(elf, new Rectangle(70, 75, 40, 40), new Rectangle(0, 0, 40, 40), Color.White);
            spriteBatch.Draw(orc, new Rectangle(130, 75, 40, 40), new Rectangle(0, 0, 40, 40), Color.White);
            spriteBatch.DrawString(spriteFont, "Class:", new Vector2(10, 120), Color.White);
            spriteBatch.DrawString(spriteFont, selectedClass , new Vector2(50, 120), Color.White);
            spriteBatch.DrawString(spriteFont,classDescription , new Vector2(120, 120), Color.White);
            spriteBatch.Draw(tank, new Rectangle(10, 150, 40, 40), Color.White);
            spriteBatch.Draw(mage, new Rectangle(70, 150, 40, 40), Color.White);
            spriteBatch.Draw(warrior, new Rectangle(130, 150, 40, 40), Color.White);
            spriteBatch.DrawString(spriteFont, "When done, press Enter to continue.", new Vector2(40, 550), Color.White);

            DrawCurrentStats();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void CalculateStats()
        {
            Player.Strength = 10;
            Player.Defence = 10;
            Player.Wisdom = 10;
            if (selectedRace == "Human")
            {
                Player.Defence += 1;
            }
            else if (selectedRace == "Elf")
            {
                Player.Wisdom += 1;
            }
            else if (selectedRace == "Orc")
            {
                Player.Strength += 1;
            }

            if (selectedClass == "Warrior")
            {
                Player.Strength += 1;
            }
            else if (selectedClass == "Knight")
            {
                Player.Defence += 1;
            }
            else if (selectedClass == "Mage")
            {
                Player.Wisdom += 1;
            }

            Player.Health[1] = (Player.Defence * 15) + 100;
            Player.Health[0] = (Player.Defence * 15) + 100;
            Player.Mana[1] = (Player.Wisdom * 15);
            Player.Mana[0] = (Player.Wisdom * 15);
            Player.Level = 1;
            Player.Race = selectedRace;
            Player.Class = selectedClass;
        }

        public void DrawCurrentStats()
    {
            spriteBatch.Draw(selectedRaceSprite,new Rectangle(600,50,40,40), new Rectangle(0,0,40,40), Color.White);
            spriteBatch.DrawString(spriteFont, "Race: " + Player.Race, new Vector2(500, 100), Color.White);
            spriteBatch.DrawString(spriteFont, "Class: " + Player.Class, new Vector2(500, 125), Color.White);
            spriteBatch.DrawString(spriteFont, "Level: " + Player.Level, new Vector2(500, 150), Color.White);
            spriteBatch.DrawString(spriteFont, "Health: " + Player.Health[0] + "/" + Player.Health[1], new Vector2(500, 175), Color.White);
            spriteBatch.DrawString(spriteFont, "Mana: " + Player.Mana[0] + "/" + Player.Mana[1], new Vector2(500, 200), Color.White);
            spriteBatch.DrawString(spriteFont, "Strength: " + Player.Strength, new Vector2(500, 225), Color.White);
            spriteBatch.DrawString(spriteFont, "Endurance: " + Player.Defence, new Vector2(500, 250), Color.White);
            spriteBatch.DrawString(spriteFont, "Wisdom: " + Player.Wisdom, new Vector2(500, 275), Color.White);
    }


    }
}
