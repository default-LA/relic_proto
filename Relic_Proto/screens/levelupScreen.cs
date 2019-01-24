using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    class LevelUpScreen : GameScreen
    {
        Texture2D image;
        Rectangle imageRectangle;
        SpriteFont spriteFont;
        public int LevelsGained;
        public int Level;
        public int[] Health = new int[2];
        public int Endurance;
        public int Wisdom;
        public int Strength;
        public int Points;
        public String Class;
        public String Race;
        public int[] Mana = new int[2];
        public playerComponent Player;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;


        public LevelUpScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image, playerComponent PlayerData)
            : base(game, spriteBatch)
        {
            Player = PlayerData;
            this.spriteFont = spriteFont;
            this.image = image;
            imageRectangle = new Rectangle(
                00,
                00,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);
            LevelsGained = Player.LevelGain;
            Points = Player.LevelGain * 2;
            if (Class == "Warrior")
            {
                Player.Strength += LevelsGained;
            }
            else if (Class == "Knight")
            {
                Player.Defence += LevelsGained;
            }
            else
            {
                Player.Wisdom += LevelsGained;
            }

            if (Race == "Orc")
            {
                Player.Strength += LevelsGained;
            }
            else if (Race == "Human")
            {
                Player.Defence += LevelsGained;
            }
            else
            {
                Player.Wisdom += LevelsGained;
            }

            Player.Mana[0] = Player.Mana[1];
            Player.Health[0] = Player.Health[1];

        }
        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (Points > 0)
            {
                if (CheckKey(Keys.E))
                {
                    Points -= 1;
                    Player.Strength += 1;
                }
                else if (CheckKey(Keys.Q))
                {
                    Points += 1;
                    Player.Strength -= 1;
                }
                else if (CheckKey(Keys.A))
                {
                    Points += 1;
                    Player.Defence -= 1;
                }
                else if (CheckKey(Keys.D))
                {
                    Points -= 1;
                    Player.Defence += 1;
                }
                else if (CheckKey(Keys.Z))
                {
                    Points += 1;
                    Player.Wisdom -= 1;
                }
                else if (CheckKey(Keys.C))
                {
                    Points -= 1;
                    Player.Strength += 1;
                }
            }

            Player.Health[1] = 100 + (15 * Player.Defence);
            Player.Health[0] = Player.Health[1];
            Player.Mana[1] = Player.Wisdom * 15;
            Player.Mana[0] = Player.Mana[1];
            base.Update(gameTime);
            oldKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, imageRectangle, Color.White);
            spriteBatch.DrawString(spriteFont, "Level: " + Player.Level, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(spriteFont, "Health: " + Player.Health[0] + "/" + Player.Health[1], new Vector2(10, 35), Color.White);
            spriteBatch.DrawString(spriteFont, "Strength: " + Player.Strength, new Vector2(10, 60), Color.White);
            spriteBatch.DrawString(spriteFont, "Endurance: " + Player.Defence, new Vector2(10, 85), Color.White);
            spriteBatch.DrawString(spriteFont, "Wisdom: " + Player.Wisdom, new Vector2(10, 110), Color.White);
            spriteBatch.DrawString(spriteFont, "Points Available: " + Points, new Vector2(10, 150), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }
    }
}