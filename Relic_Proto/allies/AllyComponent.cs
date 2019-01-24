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
    public class AllyComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int[] position = new int[2];
        public int[] playerposition = new int[2];
        public bool Aggro = false;
        MobComponent targetMob;
        public int offsetX;
        public int offsetY;
        public int formation;
        public int Strength;
        public int Endurance;
        public int Wisdom;
        public bool alive;
        public int[] Health = new int[2];
        pathFinder MoveMe;
        float fTotalElapsedTime;
        public List<AllyComponent> allies;
        public List<MobComponent> mobs;

        //Smoothscrolling
        String direction;
        public Vector2 drawposition;
        public bool moving;
        int moveDistace;
        Vector2 moveingToPosition;
        int[] oldPosition = new int[2];

        public AnimatedSprite aniSprite;

        public AllyComponent(Game game, int x, int y, int Level, int[,] Map, Vector2 position, SpriteBatch spriteBatch, Texture2D sprite)
            : base(game)
        {
            offsetX = x;
            Aggro = false;
            offsetY = y;
            Strength = (int)Math.Truncate(Level * 1.6 + 5);
            Endurance = (int)Math.Truncate(Level * 1.4 + 5);
            Wisdom = (int)Math.Truncate(5 + Level * 1.2);
            Health[0] = Endurance * 15;
            Health[1] = Endurance * 15;
            formation = 1;
            alive = true;
            MoveMe = new pathFinder(Map);
            this.position[0] = ((int)position.X) + 1; //+1 to be in the door of the tent
            this.position[1] = ((int)position.Y) + 1;
            aniSprite = new AnimatedSprite(game, sprite, spriteBatch);

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            formation = 1;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (alive)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                fTotalElapsedTime += elapsed;

                if (fTotalElapsedTime > 0.016)
                {
                    if (!moving)
                    {
                        //moveingFrom = new Vector2(position[0], position[1]);
                        int[] tempNode = new int[2];
                        CheckAggro();
                        if (!Aggro)
                        {
                            MoveMe.playerposition = playerposition;
                        }
                        else
                        {
                            MoveMe.playerposition = targetMob.position;
                        }
                        oldPosition = position;
                        MoveMe.mobs = mobs;
                        MoveMe.allies = allies;
                        tempNode = MoveMe.DoSearch(position);
                        moveingToPosition.X = tempNode[0];
                        moveingToPosition.Y = tempNode[1];
                        direction = MoveMe.chosenDirection;
                        moving = true;
                        
                        //position = tempNode; <- for insta move
                    }
                    fTotalElapsedTime = 0;
                }
            }
            aniSprite.Update(gameTime);
            calculateDraw();
            base.Update(gameTime);
        }

        private void CheckAggro()
        {
            int Count = 0;
            foreach (MobComponent thisMob in mobs)
            {
                if (thisMob.alive == true)
                {
                    if (((Math.Abs(thisMob.position[0] - position[0]) + (Math.Abs(thisMob.position[1] - position[1])))) < 6)
                    {
                        targetMob = thisMob;
                        Count += 1;
                    }
                }
            }
            if (Count > 0)
            {
                Aggro = true;
            }
            else
            {
                Aggro = false;
            }
            if (((Math.Abs(playerposition[0] - position[0]) + (Math.Abs(playerposition[1] - position[1])))) > 5)
            {
                Aggro = false;
            }
        }

        private void calculateDraw()
        {
            if (moving)
            {
                if (moveDistace == 40) //Last move
                {
                 //   aniSprite.Walking = false;
                    moving = false;
                    moveDistace = 0;
                    drawposition = new Vector2(moveingToPosition.X * 40, moveingToPosition.Y * 40);
                }
                else
                {
                    moveDistace += 4;
                    switch (direction)
                    {
                        case "Up":
                            aniSprite.direction = 1;
                            aniSprite.Walking = true;
                            drawposition = new Vector2((position[0] * 40), (position[1] * 40) - moveDistace + 40);
                            break;
                        case "Down":
                            aniSprite.direction = 0;
                            aniSprite.Walking = true;
                            drawposition = new Vector2((position[0] * 40), (position[1] * 40) + moveDistace - 40);
                            break;
                        case "Left":
                            aniSprite.direction = 2;
                            aniSprite.Walking = true;
                            drawposition = new Vector2((position[0] * 40) - moveDistace + 40, (position[1] * 40));
                            break;
                        case "Right":
                            aniSprite.Walking = true;
                            aniSprite.direction = 3;
                            drawposition = new Vector2((position[0] * 40) + moveDistace - 40, (position[1] * 40));
                            break;
                        case "Still":
                            aniSprite.Walking = false;
                            break;

                    }
                }
            }
            if (Aggro)
            {
                if (((Math.Abs(targetMob.position[0] - position[0]) + (Math.Abs(targetMob.position[1] - position[1])))) < 2)
                {
                    aniSprite.Attacking = true;
                    aniSprite.Walking = false;
                }
            }
        }
    }
}