using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Relic_Proto
{
    class startScreen : GameScreen
    {
         MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;
        public int SelectedIndex
        { get { return menuComponent.selectedIndex; }
            set { menuComponent.selectedIndex = value; }
        }
        public startScreen(Game game, 
SpriteBatch spriteBatch, 
SpriteFont spriteFont, 
Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Start Demo", "Quit" };
            menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            Components.Add(menuComponent);
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
