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
    public class AnimatedSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D spritesheet;
        public int[] position = new int[2];
        public int direction;
        int iTileToDraw;
        int iTileSetXCount = 8;
        public bool Walking;
        public bool Attacking;
        float fTotalAttackTime;
        public Rectangle imageToDraw;

        float fTotalElapsedTime;
        SpriteBatch spriteBatch;

        public AnimatedSprite(Game game, Texture2D sprite, SpriteBatch spriteB)
            : base(game)
        {
            Walking = false;
            Attacking = false;
            spriteBatch = spriteB;
            spritesheet = sprite;
            direction = 0;
            position[0] = 100;
            position[1] = 200;
            iTileSetXCount = 8;
            iTileToDraw = 0;
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
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fTotalElapsedTime += elapsed;

            if (!Attacking)
            {
                if (fTotalElapsedTime >= 0.2)
                {
                    fTotalElapsedTime = 0;
                    iTileToDraw += iTileSetXCount;
                    if (iTileToDraw >= (iTileSetXCount * 3))
                        iTileToDraw = direction + iTileSetXCount;
                }
                if (!Walking)
                {
                    iTileToDraw = direction;
                }
            }
            else
            {
                fTotalAttackTime += elapsed;
                if (direction < 4)
                    direction += 4;

                if (fTotalAttackTime > 0.1)
                {
                    iTileToDraw = direction + iTileSetXCount;
                }
                if (fTotalAttackTime > 0.2)
                {
                    iTileToDraw = direction + (2 * iTileSetXCount);
                }
                if (fTotalAttackTime > 0.3)
                {
                    Attacking = false;
                    fTotalAttackTime = 0;
                    direction -= 4;
                    iTileToDraw = direction;
                }
            }

            imageToDraw = new Rectangle(direction * 40, (iTileToDraw / iTileSetXCount) * 40, 40, 40);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}