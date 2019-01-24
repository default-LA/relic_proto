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
    public class MapComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public AnimatedSprite playerSprite;
        

        // An array of "Texture2D" objects to hold our Tile Set
        Texture2D[] t2dTiles = new Texture2D[3];
        const int iMapWidth = 100;
        const int iMapHeight = 100;
        public int[,] iMap = new int[iMapHeight, iMapWidth];


        // Variable we will need for Keyboard Input
        KeyboardState ksKeyboardState;
        //Map coordinates for upper left corner
        public int iMapX = 0;
        public int iMapY = 0;
        // How far from the Upper Left corner of the display do we want our map to start
        public int iMapDisplayOffsetX = 0;
        public int iMapDisplayOffsetY = 0;
        // How many tiles should we display at a time
        public int iMapDisplayWidth = 21;
        public int iMapDisplayHeight = 14;
        // The size of an individual tile in pixels
        public int iTileWidth = 40;
        public int iTileHeight = 40;
        // How rapidly do we want the map to scroll?
        // Sub-tile coordinates for Smooth Scrolling
        public int iMapXOffset = 0;
        public int iMapYOffset = 0;
        // Determines how fast the map will scroll (pixels per arrow key press)
        int iMapXScrollRate = 4;
        int iMapYScrollRate = 4;
        // Variable to determine if we are still moving from the previous directional command
        int iMoveCount = 0;
        // What direction are we moving (0=up, 1=down, 2=left, 3=right; same order: 4,5,6,7 for unlocked camera)
        int iMoveDirection = 0;

        Texture2D t2dTileSet;
        // Tileset Size Information
        int iTileSetXCount = 6;


        int[] Time = new int[4];

        public Texture2D player;
        public int[] position = new int[2];

        // Player Avatar Information
        int iPlayerAvatarXOffset = 0;
        int iPlayerAvatarYOffset = 0;
        public int iPlayerAvatarSubTileX = 0;
        public int iPlayerAvatarSubTileY = 0;
        int iPlayerAvatarCenterX = 10;
        int iPlayerAvatarCenterY = 6;

        SpriteFont spriteFont;
        float fTotalElapsedTime = 0;

        mapHolder maps;

        public MapComponent(Game game, Texture2D tileset, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font, Texture2D player)
            : base(game)
        {
            this.player = player;
            playerSprite = new AnimatedSprite(game, player, spriteBatch);
            
            t2dTileSet = tileset;
            spriteFont = font;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            iMapX = 0;
            iMapY = 0;
            position[0] = 0;
            position[1] = 0;
        }

        public MapComponent(Game game, Texture2D tileset, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font, Texture2D player, mapHolder maps, int random)
            : base(game)
        {
            this.player = player;
            playerSprite = new AnimatedSprite(game, player, spriteBatch);
            t2dTileSet = tileset;
            spriteFont = font;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            iMapX = 0;
            iMapY = 0;
            this.maps = maps;
            map temp;
            temp = maps.newMap(random);
            iMap = temp.returnMap();
            position = temp.returnPosition();
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

        public void newMap(int random)
        {
            map newMap;
            newMap = maps.newMap(random);
            iMap = newMap.returnMap();
            position = newMap.returnPosition();
        }

        private bool CheckKey(Keys theKey)
        {
            return ksKeyboardState.IsKeyDown(theKey);
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            {
                // The time since Update was called last
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                fTotalElapsedTime += elapsed;

                    // Read and save the current Keyboard State
                    ksKeyboardState = Keyboard.GetState();
                    // Check to see if the Escape key has been pressed.  Exit the program if so.

                    // If we AREN'T in the process of completing a smooth-scroll move...
                    if (iMoveCount <= 0)
                    {
                    }
                    else
                    {
                        // If we ARE in the middle of a smooth-scroll move, update the 
                        // Offsets and decrement the move count.
                        if (iMoveDirection == 0)
                        {
                            iMapYOffset -= iMapYScrollRate;
                            iMoveCount -= iMapYScrollRate;
                        }
                        if (iMoveDirection == 1)
                        {
                            iMapYOffset += iMapYScrollRate;
                            iMoveCount -= iMapYScrollRate;
                        }
                        if (iMoveDirection == 2)
                        {
                            iMapXOffset -= iMapXScrollRate;
                            iMoveCount -= iMapXScrollRate;
                        }
                        if (iMoveDirection == 3)
                        {
                            iMapXOffset += iMapXScrollRate;
                            iMoveCount -= iMapXScrollRate;
                        }

                        if (iMoveDirection == 4)
                        {
                            iPlayerAvatarSubTileY -= iMapYScrollRate;
                            iMoveCount -= iMapYScrollRate;
                        }
                        if (iMoveDirection == 5)
                        {
                            iPlayerAvatarSubTileY += iMapYScrollRate;
                            iMoveCount -= iMapYScrollRate;
                        }
                        if (iMoveDirection == 6)
                        {
                            iPlayerAvatarSubTileX -= iMapXScrollRate;
                            iMoveCount -= iMapXScrollRate;
                        }
                        if (iMoveDirection == 7)
                        {
                            iPlayerAvatarSubTileX += iMapXScrollRate;
                            iMoveCount -= iMapXScrollRate;
                        }

                        if (iPlayerAvatarSubTileX < 0) { iPlayerAvatarSubTileX = iTileWidth; iPlayerAvatarXOffset--; }
                        if (iPlayerAvatarSubTileX > iTileWidth) { iPlayerAvatarSubTileX = 0; iPlayerAvatarXOffset++; }
                        if (iPlayerAvatarSubTileY < 0) { iPlayerAvatarSubTileY = iTileHeight; iPlayerAvatarYOffset--; }
                        if (iPlayerAvatarSubTileY > iTileHeight) { iPlayerAvatarSubTileY = 0; iPlayerAvatarYOffset++; }

                        if (iMapXOffset < 0) { iMapXOffset = iTileHeight; iMapX--; }
                        if (iMapXOffset > iTileHeight) { iMapXOffset = 0; iMapX++; }
                        if (iMapYOffset < 0) { iMapYOffset = iTileHeight; iMapY--; }
                        if (iMapYOffset > iTileHeight) { iMapYOffset = 0; iMapY++; }
                    }

                    playerSprite.Update(gameTime);
                    base.Update(gameTime);
                }
            
        }



        public override void Draw(GameTime gameTime)
        {
            // Draw the map
            for (int y = 0; y < iMapDisplayHeight; y++)
            {
                for (int x = 0; x < iMapDisplayWidth; x++)
                {

                    int iTileToDraw = iMap[y + iMapY, x + iMapX];
                    Rectangle recSource = new Rectangle((iTileToDraw % iTileSetXCount) * iTileWidth,
                                                        (iTileToDraw / iTileSetXCount) * iTileHeight,
                                                        40, 40);
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                    spriteBatch.Draw(t2dTileSet,
                                     new Rectangle((((x * iTileWidth) - iMapXOffset) + iMapDisplayOffsetX),
                                     (((y * iTileWidth) - iMapYOffset) + iMapDisplayOffsetY),
                                     40, 40),
                                     recSource, Color.White);
                    spriteBatch.End();
                }
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, position[0].ToString() + "," + position[1].ToString(), new Vector2(400, 0), Color.White);
            spriteBatch.Draw(playerSprite.spritesheet,
                              new Rectangle(((iPlayerAvatarXOffset * iTileWidth)
                                  + iMapDisplayOffsetX + iPlayerAvatarSubTileX),
                              ((iPlayerAvatarYOffset * iTileHeight) + iMapDisplayOffsetY
                                  + iPlayerAvatarSubTileY),
                              40, 40), playerSprite.imageToDraw,
                              Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public int CheckMapMovementKeys(Keys theKey)
        {
                if (iMoveCount <= 0)
                {

            if (theKey == Keys.W)
            {
                playerSprite.Walking = true;
                playerSprite.direction = 1;

                if ((iMapY + iPlayerAvatarYOffset - 1) >= 0)
                {

                    if ((iMapY > 0) & (iPlayerAvatarYOffset <= iPlayerAvatarCenterY))
                    {
                        iMoveDirection = 0;
                        iMoveCount = iMapYScrollRate + iTileHeight;
                        position[1] -= 1;
                        return 1;
                    }
                    else
                    {

                        if (iPlayerAvatarYOffset > 0)
                        {
                            iMoveDirection = 4;
                            position[1] -= 1;
                            iMoveCount = iMapYScrollRate + iTileWidth;
                        }
                    }
                }
            }


            else if (theKey ==Keys.S)
            {
                playerSprite.Walking = true;
                playerSprite.direction = 0;

                if ((iMapY + iPlayerAvatarYOffset + 1) <= iMapHeight)
                {

                    if ((iMapY < (iMapHeight - iMapDisplayHeight)) & (iPlayerAvatarYOffset >= iPlayerAvatarCenterY))
                    {
                        iMoveDirection = 1;
                        position[1] += 1;
                        iMoveCount = iMapYScrollRate + iTileHeight;
                        return 1;
                    }
                    else
                    {

                        if ((iPlayerAvatarYOffset + iMapY + 2) < iMapHeight)
                        {
                            iMoveDirection = 5;
                            position[1] += 1;
                            iMoveCount = iMapYScrollRate + iTileHeight;
                        }
                    }
                }
            }

            else if (theKey == Keys.A)
            {
                playerSprite.Walking = true;
                playerSprite.direction = 2;


                if ((iMapX + iPlayerAvatarXOffset - 1) >= 0)
                {

                    if ((iMapX > 0) & (iPlayerAvatarXOffset <= iPlayerAvatarCenterX))
                    {
                        iMoveDirection = 2;
                        position[0] -= 1;
                        iMoveCount = iMapXScrollRate + iTileWidth;
                        return 1;
                    }
                    else
                    {

                        if (iPlayerAvatarXOffset > 0)
                        {
                            iMoveDirection = 6;
                            position[0] -= 1;
                            iMoveCount = iMapXScrollRate + iTileWidth;
                        }
                    }
                }
            }

            else if (theKey == Keys.D)
            {
                playerSprite.Walking = true;
                playerSprite.direction = 3;

                {
                    if ((iMapX + iPlayerAvatarXOffset + 1) <= iMapWidth)
                    {

                        if ((iMapX < (iMapWidth - iMapDisplayWidth)) & (iPlayerAvatarXOffset >= iPlayerAvatarCenterX))
                        {
                            iMoveDirection = 3;
                            position[0] += 1;
                            iMoveCount = iMapXScrollRate + iTileWidth;
                            return 1;
                        }
                        else
                        {

                            if ((iPlayerAvatarXOffset + iMapX + 2) < iMapWidth)
                            {
                                iMoveDirection = 7;
                                position[0] += 1;
                                iMoveCount = iMapXScrollRate + iTileWidth;

                            }

                        }
                    }
                }
            }


            else
            {
                playerSprite.Walking = false;
            }
            return 0;

        }
            return 0;
    }
        
    }
}