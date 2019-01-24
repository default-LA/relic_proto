using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    class deathScreen : GameScreen
    {
        MenuComponent deathscreencomponent;
        Texture2D image;
        Rectangle imageRectangle;
        public int SelectedIndex
        {
            get
            { return deathscreencomponent.selectedIndex; }
            set { deathscreencomponent.selectedIndex = value; }
        }
        public deathScreen(Game game,
SpriteBatch spriteBatch,
SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Retry", "Quit" };
            deathscreencomponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            Components.Add(deathscreencomponent);
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
