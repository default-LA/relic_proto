using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    class winScreen : GameScreen
    {
        MenuComponent winscreencomponent;
        Texture2D image;
        Rectangle imageRectangle;
        public int SelectedIndex
        {
            get
            { return winscreencomponent.selectedIndex; }
            set { winscreencomponent.selectedIndex = value; }
        }
        public winScreen(Game game,
SpriteBatch spriteBatch,
SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Continue", "Buy Items", "Spend Points", "Quit" };
            winscreencomponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            Components.Add(winscreencomponent);
            this.image = image;
            imageRectangle = new Rectangle(
                0,
                0,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);
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
