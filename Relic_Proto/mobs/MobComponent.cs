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
    public class MobComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D sprite;
        public Color colour;
        private Random random;
        pathFinder MoveMe;
        public List<MobComponent> mobs;
        public List<AllyComponent> allies = new List<AllyComponent>();
        int[,] iMap;
        public bool targetAlly;
        AllyComponent targetedAlly;
        public bool isBoss;
        public int[] playerposition = new int[2];
        public int[] position;
        public int Strength;
        public int Endurance;
        public int Wisdom;
        public int[] Health = new int[2];
        public int iMapX;
        public int iMapY;
        public bool alive;
        public bool isSelected;
        public int Level;
        float fTotalElapsedTime;
        public int Experience;
        MouseState oldMouse;
        public AnimatedSprite aniSprite; 

        String direction;
        public Vector2 drawposition;
        public bool moving;
        int moveDistace;
        Vector2 moveingToPosition;


        public MobComponent(Game game, int[] mobposition, int Level, int[,] Map, bool Boss, SpriteBatch spriteBatch, Texture2D sprite)
            : base(game)
        {
            isBoss = Boss;
            iMap = Map;
            MoveMe = new pathFinder(iMap);
            isSelected = false;
            targetAlly = false;
            this.position = mobposition;
            alive = true;
            random = new Random();
            colour = Color.White;
            if (!isBoss)
            {
                Strength = (int)Math.Truncate(Level * 1.6 + 5);
                Endurance = (int)Math.Truncate(Level * 1.4 + 5);
                Wisdom = (int)Math.Truncate(5 + Level * 1.2);
            }
            else
            {
                Strength = (int)Math.Truncate(Level * 10.2 + 8);
                Endurance = (int)Math.Truncate(Level * 10.6 + 8);
                Wisdom = (int)Math.Truncate(8 + Level * 9.4);
            }
            aniSprite = new AnimatedSprite(game, sprite, spriteBatch);
            playerposition[0] = 0;
            playerposition[1] = 0;

            Health[0] = Endurance * 25;
            Health[1] = Endurance * 25;

            this.Level = Level;
            if (isBoss)
            {
                Experience = Level * 500;
            }
            else
            {
                Experience = Level * 100;
            }
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
            if (alive)
            {
                CheckSelected();

                CheckSelected();
                if (!targetAlly)
                {
                    MoveMe.playerposition = playerposition;
                }
                else
                {
                    MoveMe.playerposition = targetedAlly.position;
                }

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                fTotalElapsedTime += elapsed;

                if (!isBoss)
                {
                    if (fTotalElapsedTime > 0.025)
                    {
                        if (!moving)
                        {
                            Movement(gameTime);
                        }
                    }
                }
                else
                {
                    if (fTotalElapsedTime > 0.1)
                    {
                        if (!moving)
                        {
                            Movement(gameTime);
                        }
                    }
                }
            }

            calculateDraw();
            aniSprite.Update(gameTime);
            base.Update(gameTime);
        }


        public void Movement(GameTime gameTime)
        {
            
            if (hasAggro(position, playerposition))
            {
                    int[] tempNode = new int[2];
                    MoveMe.mobs = mobs;
                    MoveMe.allies = allies;
                    tempNode = MoveMe.DoSearch(position);

                    moveingToPosition.X = tempNode[0];
                    moveingToPosition.Y = tempNode[1];
                    direction = MoveMe.chosenDirection;
                    moving = true;

                    fTotalElapsedTime = 0;
            }
            if (targetAlly)
            {
                if (((Math.Abs(targetedAlly.position[0] - position[0]) + (Math.Abs(targetedAlly.position[1] - position[1])))) < 2)
                {
                    aniSprite.Attacking = true;
                    aniSprite.Walking = false;
                }
            }
            else
            {
                if (((Math.Abs(playerposition[0] - position[0]) + (Math.Abs(playerposition[1] - position[1])))) < 2)
                {
                    aniSprite.Attacking = true;
                    aniSprite.Walking = false;
                }
            }
        }
        
        


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        public void CheckSelected()
        {
            MouseState curMouseState = Mouse.GetState();
            if (curMouseState.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                if (((position[0] * 40) - (iMapX * 40)) < curMouseState.X &&
                 (((position[0] * 40) - (iMapX * 40)) + 40 > curMouseState.X &&
                 ((position[1] * 40) - (iMapY * 40)) < curMouseState.Y &&
                 (((position[1] * 40) - (iMapY * 40)) + 40) > curMouseState.Y))
                {
                    colour = Color.SlateGray;
                    isSelected = true;
                }

                else
                {
                    isSelected = false;
                    colour = Color.White;
                }
            }

            oldMouse = curMouseState;
        }


        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        private bool hasAggro(int[] position, int[] playerposition)
        {
            int Count = 0;
            bool nearPlayer = false;

            if (((Math.Abs(playerposition[0] - position[0]) + (Math.Abs(playerposition[1] - position[1])))) < 8)
            {
                nearPlayer = true;
            }

            int playerdistance = (((Math.Abs(playerposition[0] - position[0]) + (Math.Abs(playerposition[1] - position[1])))));
            foreach (AllyComponent thisAlly in allies)
            {
                if (thisAlly.alive)
                {
                    if (((Math.Abs(thisAlly.position[0] - position[0]) + (Math.Abs(thisAlly.position[1] - position[1]))) < playerdistance))
                    {
                        targetedAlly = thisAlly;
                        Count += 1;
                    }
                }
            }


            if (Count > 0)
            {
                targetAlly = true;
            }
            else
            {
                targetAlly = false;
            }

            return nearPlayer;
        }

        private void calculateDraw()
        {
            if (moving)
            {
                if (moveDistace == 40) //Last move
                {
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
                            aniSprite.direction = 3;
                            aniSprite.Walking = true;
                            drawposition = new Vector2((position[0] * 40) + moveDistace - 40, (position[1] * 40));
                            break;
                    }
                }
            }

            else
            {
                aniSprite.Walking = false;
                drawposition = new Vector2(position[0] * 40,position[1]* 40);
            }
        }

       
    }



}