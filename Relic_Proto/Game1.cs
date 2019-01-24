using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        
        GameScreen activeScreen;
        winScreen winScreen;
        deathScreen deathScreen;
        startScreen startScreen;
        campaignScreen actionScreen;
        characterScreen statScreen;
        characterCreateScreen createcharScreen;
        LevelUpScreen levelScreen;

        Texture2D elf;
        Texture2D orc;
        Texture2D human;
        Texture2D tileset;
        Texture2D HUD;
        Texture2D floatHUD;
        Texture2D redTile;
        Texture2D mob;
        Texture2D sword;
        Texture2D shield;
        Texture2D hat;
        Texture2D ally;
        Texture2D health;
        Texture2D mana;
        Texture2D titleBG;
        Texture2D generalBG;
        Texture2D HUDBG;
        Texture2D currentSprite;
        Texture2D hud2;
        Texture2D swordIcon;
        Texture2D healIcon;
        Texture2D buffIcon;
        Texture2D spellIcon;
        Texture2D aoeIcon;
        
        
        playerComponent Player;
        
        SpriteFont normal;
        SpriteFont menu;

        //Item Code
        Texture2D closedChestSprite;
        Texture2D openChestSprite;
        Texture2D slot1Sprite;
        Texture2D slot2Sprite;
        Texture2D slot3Sprite;
        Texture2D slot4Sprite;
        Texture2D testItemSprtie;
        //Item Code

        //Building Code
        Texture2D tentSprite; //Sprite used for the tents.
        //Building Code

        //Anni Sprites
        Texture2D elfAllySprite;
        Texture2D orcAllySprite;
        Texture2D humanAllySprite;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            tileset = Content.Load<Texture2D>("smallertileset");
            elf = Content.Load<Texture2D>("elfsprite");
            orc = Content.Load<Texture2D>("orcsheet");
            human = Content.Load<Texture2D>("humansheet");
            ally = Content.Load<Texture2D>("humanally");
            HUD = Content.Load<Texture2D>("hud");
            hud2 = Content.Load<Texture2D>("hud2");
            mob = Content.Load<Texture2D>("mobsprite");
            sword = Content.Load<Texture2D>("shield");
            shield = Content.Load<Texture2D>("sword");
            hat = Content.Load<Texture2D>("hat");
            normal = Content.Load<SpriteFont>("hudfont");
            menu = Content.Load<SpriteFont>("default");
            titleBG = Content.Load<Texture2D>("menubg");
            generalBG = Content.Load<Texture2D>("menubackground");
            HUDBG = Content.Load<Texture2D>("hudbg");
            health = Content.Load<Texture2D>("health");
            mana = Content.Load<Texture2D>("health");
            redTile = Content.Load<Texture2D>("redTile");
            swordIcon = Content.Load<Texture2D>("swordicon");
            healIcon = Content.Load<Texture2D>("healicon");
            buffIcon = Content.Load<Texture2D>("bufficon");
            aoeIcon = Content.Load<Texture2D>("aoeicon");
            spellIcon = Content.Load<Texture2D>("spellicon");

            //Item Code
            closedChestSprite = Content.Load<Texture2D>("closedChest");
            openChestSprite = Content.Load<Texture2D>("openChest");
            slot1Sprite = Content.Load<Texture2D>("slot1");
            slot2Sprite = Content.Load<Texture2D>("slot2");
            slot3Sprite = Content.Load<Texture2D>("slot3");
            slot4Sprite = Content.Load<Texture2D>("slot4");
            testItemSprtie = Content.Load<Texture2D>("testItem");
            //Item Code

            //Building Code
            tentSprite = Content.Load<Texture2D>("tent");
            //Building Code

            //Anni Sprites
            elfAllySprite = Content.Load<Texture2D>("elfallysheet");
            orcAllySprite = Content.Load<Texture2D>("orcallysheet");
            humanAllySprite = Content.Load<Texture2D>("humanallysheet");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new startScreen(
                this,
                spriteBatch,
                menu,
                titleBG);
            Components.Add(startScreen);
            startScreen.Hide();

            winScreen = new winScreen(
                this,
                spriteBatch,
                menu,
                titleBG);
            Components.Add(winScreen);
            winScreen.Hide();

            deathScreen = new deathScreen(
                this,
                spriteBatch,
                menu,
                titleBG);
            Components.Add(deathScreen);
            deathScreen.Hide();

            createcharScreen = new characterCreateScreen(this, spriteBatch, normal, generalBG,
                human, elf, orc, sword, shield, hat);
            Components.Add(createcharScreen);
            createcharScreen.Hide();

            //Reader
            reader read;
            read = new reader();

            actionScreen = new campaignScreen(this, spriteBatch, tileset, elf, graphics, normal, generalBG,
                HUD, floatHUD, mob, ally, createcharScreen.Player, health, mana, HUDBG,
                openChestSprite, closedChestSprite, slot1Sprite, slot2Sprite, slot3Sprite, slot4Sprite, testItemSprtie, redTile, tentSprite, elfAllySprite, orcAllySprite, humanAllySprite, orc, human, hud2, buffIcon, spellIcon, healIcon, aoeIcon, swordIcon,
                read.itemFile(false), read.mapFile(false));//change the true based on the descision from the user. 
            Components.Add(actionScreen);
            actionScreen.Hide();

            statScreen = new characterScreen(this,    spriteBatch,    normal,    generalBG,    actionScreen.PlayerData);
            Components.Add(statScreen);
            statScreen.Hide();

            levelScreen = new LevelUpScreen(this, spriteBatch, normal, generalBG, actionScreen.Player);
            Components.Add(levelScreen);
            levelScreen.Hide();
            
            activeScreen = startScreen;
            activeScreen.Show();
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            string test;
            test = Environment.CurrentDirectory; 
            //Reader
            reader read;
            read = new reader();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (activeScreen == startScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (startScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        activeScreen = createcharScreen;
                        activeScreen.Show();
                    }
                    if (startScreen.SelectedIndex == 1)
                    {
                        this.Exit();
                    }
                }
            }
            else if (activeScreen == levelScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    activeScreen.Hide();
                    actionScreen = new campaignScreen(this, spriteBatch, tileset, actionScreen.sprite,
                            graphics, normal, generalBG, HUD, floatHUD, mob, ally, levelScreen.Player, health, mana, HUDBG, openChestSprite, closedChestSprite, slot1Sprite, slot2Sprite, slot3Sprite, slot4Sprite, testItemSprtie, redTile, tentSprite, elfAllySprite, orcAllySprite, humanAllySprite, orc, human, hud2, buffIcon, spellIcon, healIcon, aoeIcon, swordIcon,
                            read.itemFile(false), read.mapFile(true));
                    Components.Add(actionScreen);
                    actionScreen.Player = levelScreen.Player;
                    activeScreen = actionScreen;
                    activeScreen.Show();
                }
            }
            else if (activeScreen == winScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (winScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        actionScreen = new campaignScreen(this, spriteBatch, tileset, actionScreen.sprite, graphics, normal, generalBG,
                HUD, floatHUD, mob, ally, actionScreen.Player, health, mana, HUDBG,
                openChestSprite, closedChestSprite, slot1Sprite, slot2Sprite, slot3Sprite, slot4Sprite, testItemSprtie, redTile, tentSprite, elfAllySprite, orcAllySprite, humanAllySprite, orc, human, hud2, buffIcon, spellIcon, healIcon, aoeIcon, swordIcon,
                read.itemFile(false), read.mapFile(true)); 
                        Components.Add(actionScreen);
                        actionScreen.Player.Health[0] = actionScreen.Player.Health[1];
                        activeScreen = actionScreen;
                    }
                    if (winScreen.SelectedIndex == 2)
                    {
                        activeScreen.Hide();
                        levelScreen = new LevelUpScreen(this, spriteBatch, normal, generalBG, actionScreen.Player);
                        Components.Add(levelScreen);
                        activeScreen = levelScreen;
                        activeScreen.Show();
                    }

                    if (winScreen.SelectedIndex == 3)
                    {
                        activeScreen.Hide();
                        activeScreen = startScreen;
                        activeScreen.Show();
                    }
                }
            }
            else if (activeScreen == deathScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (deathScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        actionScreen = new campaignScreen(this, spriteBatch, tileset, actionScreen.sprite, graphics, normal, generalBG,
                HUD, floatHUD, mob, ally, actionScreen.Player, health, mana, HUDBG,
                openChestSprite, closedChestSprite, slot1Sprite, slot2Sprite, slot3Sprite, slot4Sprite, testItemSprtie, redTile, tentSprite, elfAllySprite, orcAllySprite, humanAllySprite, orc, human, hud2, buffIcon, spellIcon, healIcon, aoeIcon, swordIcon,
                read.itemFile(false), read.mapFile(true)); 
                        Components.Add(actionScreen);
                        actionScreen.Player.Level -= actionScreen.Player.LevelGain;
                        actionScreen.Player.LevelGain = 0;
                        actionScreen.Player.Experience = 0;
                        actionScreen.Player.Health[0] = actionScreen.Player.Health[1];

                        activeScreen = actionScreen;
                        activeScreen.Show();
                    }
                    if (deathScreen.SelectedIndex == 1)
                    {
                        activeScreen.Hide();
                        activeScreen = startScreen;
                        activeScreen.Show();
                    }
                }
            }
            else if (activeScreen == actionScreen)
            {
                int count = 0;
                if (actionScreen.Player.Health[0] <= 0)
                {
                    activeScreen.Hide();
                    activeScreen = deathScreen;
                    activeScreen.Show();
                }
                if (CheckKey(Keys.Escape))
                {
                    activeScreen.Hide();
                    activeScreen = statScreen;
                    activeScreen.Visible = true;
                    activeScreen.Show();
                }
                foreach (MobComponent thisMob in actionScreen.mobControl.mobs)
                {
                    if (thisMob.alive)
                    {
                        count += 1;
                    }

                }
                if (count == 0)
                {
                    Player = actionScreen.Player;
                    currentSprite = actionScreen.sprite;
                    activeScreen.Hide();
                    activeScreen = winScreen;
                    activeScreen.Show();
                }
            }

            else if (activeScreen == statScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (statScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        activeScreen = actionScreen;
                        activeScreen.Show();
                    }
                    else
                    {
                        activeScreen.Hide();
                        activeScreen = startScreen;
                        activeScreen.Show();
                    }

                }
            }

            else if (activeScreen == createcharScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if ((createcharScreen.selectedClass != "None") & (createcharScreen.selectedRace != "None"))
                    {
                        activeScreen.Hide();
                        actionScreen = new campaignScreen(this, spriteBatch, tileset, createcharScreen.selectedRaceSprite, graphics, normal, generalBG,
                HUD, floatHUD, mob, ally, createcharScreen.Player, health, mana, HUDBG,
                openChestSprite, closedChestSprite, slot1Sprite, slot2Sprite, slot3Sprite, slot4Sprite, testItemSprtie, redTile, tentSprite, elfAllySprite, orcAllySprite, humanAllySprite, orc, human, hud2, buffIcon, spellIcon, healIcon, aoeIcon, swordIcon,
                read.itemFile(false), read.mapFile(true)); Components.Add(actionScreen);
                        activeScreen = actionScreen;
                        activeScreen.Show();
                    }
                }
                
            }

            base.Update(gameTime);
               
                oldKeyboardState = keyboardState;
            
        }
        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
            
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }
    }
}

