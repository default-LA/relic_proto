using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    class characterScreen : GameScreen
    {
        Texture2D image;
        Rectangle imageRectangle;
        String[] PlayerAttributes = new String[6];
        SpriteFont spriteFont;
        public int SelectedIndex
        {
            get
            { return menu.selectedIndex; }
            set { menu.selectedIndex = value; }
        }
        MenuComponent menu;
                
        public characterScreen(Game game,SpriteBatch spriteBatch,SpriteFont spriteFont,Texture2D image, String[] PlayerData)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Resume", "Quit" };
            menu = new MenuComponent(game,
              spriteBatch,
              spriteFont,
              menuItems);
             Components.Add(menu);
            this.spriteFont = spriteFont;
            PlayerAttributes = PlayerData;
            this.image = image;
            imageRectangle = new Rectangle(
                00,
                00,
                Game.Window.ClientBounds.Width * 2 ,
                Game.Window.ClientBounds.Height * 2);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, imageRectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}