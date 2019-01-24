using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Relic_Proto
{
    class campaignScreen : GameScreen
    {
        public playerComponent Player;
        GraphicsDeviceManager graphics;
        Texture2D[] tiles = new Texture2D[3];
        Rectangle imageRectangle;
        public MapComponent map;
        Allies allies;
        CollisionComponent collisions;
        SpriteFont spriteFont;
        KeyboardState ksKeyboardState;
        public Texture2D sprite;
        Texture2D mobsprite;
        Texture2D menubg;
        Texture2D redTile;
        float fTotalElapsedTime2;
        float fTotalElapsedTime6; 
        int icollisions;
        int tileCollisions;
        Texture2D tophud;
        Texture2D hudbar;
        float fTotalElapsedTime;
        float fTotalElapsedTime3;
        float fTotalElapsedTime7;
        resourceControl tileControl;
        float resourceTimer;
        public EnemyControl mobControl;
        Random random = new Random();
        int[] position = new int[2];
        int[] otherposition = new int[2];
        Texture2D allySprite;
        Texture2D healthbar;
        Texture2D manabar;
        Texture2D hudbg;
        Texture2D expbar;
        Texture2D hud2;
        Texture2D manaPot;
        Texture2D swordIcon;
        Texture2D buffIcon;
        Texture2D healIcon;
        Texture2D aoeIcon;
        Texture2D spellIcon;

        //Item Code
        dropController itemControl;
        Texture2D closedChestSprite;
        Texture2D openChestSprite;
        Texture2D slot1Sprite;
        Texture2D slot2Sprite;
        Texture2D slot3Sprite;
        Texture2D slot4Sprite;
        Texture2D testItemSprtie;//CHANGE!
        //Item Code

        //Reader Code
        List<item> items;
        //Reader Code

        //Building Code
        buildingControl buildings;
        Texture2D tentSprite; //Sprite used for the tents.
        //Building Code

        //Anni Sprites
        Texture2D elfAllySprite;
        Texture2D orcAllySprite;
        Texture2D humanAllySprite;
        Texture2D orc;
        Texture2D human;


        //Skill Code
        bool strBuff;
        bool Bubble;
        float fTotalElapsedTime4; //Buff Timer
        int tempStrength;
        int tempHealth;
        //Skill Code

        public campaignScreen(Game game, SpriteBatch spriteBatch, Texture2D tiles, Texture2D sprite, GraphicsDeviceManager graphics, SpriteFont spriteFont,
           Texture2D bg, Texture2D heads, Texture2D floathud, Texture2D txtmob, Texture2D allymob, playerComponent Player,
           Texture2D health, Texture2D mana, Texture2D hudbg,
           Texture2D closedChestSprite, Texture2D openChestSprite, Texture2D slot1Sprite, Texture2D slot2Sprite, Texture2D slot3Sprite, Texture2D slot4Sprite, Texture2D testItemSprtie, Texture2D redTile, Texture2D tentSprite, Texture2D elfAllySprite, Texture2D orcAllySprite, Texture2D humanAllySprite, Texture2D orcBossSprite, Texture2D humanBossSprite, Texture2D hud2, Texture2D buffIcon, Texture2D spellIcon, Texture2D healIcon, Texture2D aoeIcon, Texture2D swordIcon, 
           List<item> items, mapHolder maps)
            : base(game, spriteBatch)
        {

            //Anni Sprites
            this.elfAllySprite = elfAllySprite;
            this.orcAllySprite = orcAllySprite;
            this.humanAllySprite = humanAllySprite;
            this.human = humanBossSprite;
            this.orc = orcBossSprite;

            healthbar = health;
            manabar = mana;
            expbar = health;
            this.hudbg = hudbg;
            this.Player = Player;
            this.hud2 = hud2;
            allySprite = allymob;
            mobsprite = txtmob;
            position[0] = 10;
            position[1] = 10;
            otherposition[0] = 14;
            otherposition[1] = 12;
            tophud = floathud;
            hudbar = heads;
            menubg = bg;
            this.graphics = graphics;
            this.spriteFont = spriteFont;
            this.sprite = sprite;
           // this.manaPot = manaPot;

            this.swordIcon = swordIcon;
            this.healIcon = healIcon;
            this.buffIcon = buffIcon;
            this.spellIcon = spellIcon;
            this.aoeIcon = aoeIcon;

            //Item Code
            this.closedChestSprite = closedChestSprite;
            this.openChestSprite = openChestSprite;
            itemControl = new dropController(game, openChestSprite, closedChestSprite, spriteBatch, items);
            this.slot1Sprite = slot1Sprite;
            this.slot2Sprite = slot2Sprite;
            this.slot3Sprite = slot3Sprite;
            this.slot4Sprite = slot4Sprite;
            this.redTile = redTile;
            this.testItemSprtie = testItemSprtie;
            //Item Code
            map = new MapComponent(game, tiles, graphics, spriteBatch, spriteFont, sprite, maps, RandomNumber(0, maps.total()));//Modified for map files
            collisions = new CollisionComponent(game, map.iMap, map.position);
            Components.Add(map);
            tileControl = new resourceControl(map.iMap); //Updated for tile based resources!
            Components.Add(itemControl); //Item Code
            Components.Add(Player);
            Components.Add(collisions);
            Texture2D allyToUse;
            if (Player.Race == "Orc")
            { allyToUse = orcAllySprite; }
            else if (Player.Race == "Elf")
            { allyToUse = elfAllySprite; }
            else
            { allyToUse = humanAllySprite; }
            allies = new Allies(game, spriteBatch, allyToUse, Player.Level, map.iMap);
            allies.iMapX = map.iMapX;
            allies.iMapY = map.iMapY;
            Components.Add(allies);
            Texture2D mobToUse;
            Texture2D bossToUse;
            if (Player.Race == "Human")
            {
                mobToUse = orcAllySprite;
                bossToUse = orc;
            }
            else
            {
                mobToUse = humanAllySprite;
                bossToUse = human;
            }
            mobControl = new EnemyControl(game, mobToUse, bossToUse, spriteBatch, Player.Level, map.iMap);
            mobControl.iMapX = map.iMapX;
            mobControl.iMapY = map.iMapY;

            Components.Add(mobControl);
            imageRectangle = new Rectangle(
                0,
                0,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);

            //Building Code
            this.tentSprite = tentSprite;
            buildings = new buildingControl(game, tentSprite, spriteBatch, map.iMap);
            Components.Add(buildings);
            //Building Code
        }


        public override void Update(GameTime gameTime)
        {
            ksKeyboardState = Keyboard.GetState();
            UpdateMobs(gameTime);
            UpdateCombat(gameTime);
            CheckMapInput();

            //Code below so that Allies and Mobs can fight
            allies.mobs = mobControl.mobs;
            mobControl.allies = allies.allies;

            checkResource(gameTime);
            Player.Resource = allies.CheckInput(Player.Resource);

            Regen(gameTime);//Regens stats for the allies and player

            alliesTents();//Updates the allie controller with the correct tent values. 

            //Skills
            buffTimers(gameTime);
            playerSkills(gameTime);
            playerPots(gameTime);

            //Item code
            itemControl.updateIMap(map.iMapX, map.iMapY);
            itemControl.updateOffset(new Vector2(map.iMapXOffset, map.iMapYOffset));
            UpdateDrops();
            UpdatePlayerItems();
            //Item code

            //Building code
            buildings.updateInfo(new Vector2(((map.iMapX * 40) + map.iMapXOffset), ((map.iMapY * 40) + map.iMapYOffset)));
            //Building code

            //SmoothScrolling
            mobControl.updatedOffset(map.iMapXOffset, map.iMapYOffset);
            allies.updatedOffset(map.iMapXOffset, map.iMapYOffset);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            DrawHUD();
        }

        private bool CheckKey(Keys theKey)
        {
            return ksKeyboardState.IsKeyDown(theKey);
        }

        private void checkResource(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            resourceTimer += elapsed;

            if (resourceTimer >= 0.7)
            {
                if (tileControl.checkResource(map.position[1], map.position[0]))
                {
                    Player.Resource++;
                }
                resourceTimer = 0;
            }
        }

        public String[] PlayerData
        {
            get
            {
                return Player.Attributes;
            }

            set
            {
                Player.Attributes = value;
            }
        }

        public void DrawHUD()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(hudbg, new Rectangle(0, 500, 224, 100), Color.White);
            spriteBatch.Draw(healthbar, new Rectangle(118, 509, (int)((95 / (float)Player.Health[1]) * (float)Player.Health[0]), 14), Color.Red);
            spriteBatch.Draw(healthbar, new Rectangle(118, 533, (int)((95 / (float)Player.Mana[1]) * (float)Player.Mana[0]), 14), Color.Blue);
            spriteBatch.DrawString(spriteFont, Player.Mana[0] + "/" + Player.Mana[1], new Vector2(135, 543), Color.Blue);
            spriteBatch.Draw(healthbar, new Rectangle(224, 503, (int)((250 / ((float)Player.Level * 1000) * (float)Player.Experience)), 8), Color.SkyBlue);
            spriteBatch.Draw(hudbar, new Rectangle(0, 500, 800, 100), Color.White);

            spriteBatch.Draw(hud2, new Rectangle(730, 0, 80, 50), Color.White);
            spriteBatch.DrawString(spriteFont, "  -  " + PlayerData[8].ToString(), new Vector2(750, 1), Color.Red);
            spriteBatch.DrawString(spriteFont, "  -  " + PlayerData[9].ToString(), new Vector2(750, 15), Color.Red);
            spriteBatch.DrawString(spriteFont, "  -  " + PlayerData[7].ToString(), new Vector2(750, 30), Color.Red);

            ////////icons colours etc///////////////

            if (Player.Class == "Warrior")
            {
                spriteBatch.Draw(buffIcon, new Vector2(1, 474), Color.White);
                spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.White);
                spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.White);
                {
                    if (Player.Mana[0] < 40) 
                    {
                        spriteBatch.Draw(buffIcon, new Vector2(1, 474), Color.Red);
                    }
                    if (Player.Mana[0] < 40)
                    {
                        spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.Red);
                    }
                    if (Player.Mana[0] < 30)
                    {
                        spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.Red);
                    }
                    if (CheckKey(Keys.D1))
                    {
                        spriteBatch.Draw(buffIcon, new Vector2(1, 474), Color.Yellow);
                    }
                    if (CheckKey(Keys.D2))
                    {
                        spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.Yellow);
                    }
                    if (CheckKey(Keys.D3))
                    {
                        spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.Yellow);
                    }

                }
            }
            else if (Player.Class == "Mage")
            {

                spriteBatch.Draw(healIcon, new Vector2(1, 474), Color.White);
                spriteBatch.Draw(spellIcon, new Vector2(30, 474), Color.White);
                spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.White);

                if (Player.Mana[0] < 50)
                {
                    spriteBatch.Draw(healIcon, new Vector2(1, 474), Color.Red);
                }
                if (Player.Mana[0] < 50)
                {
                    spriteBatch.Draw(spellIcon, new Vector2(30, 474), Color.Red);
                }
                if (Player.Mana[0] < 40)
                {
                    spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.Red);
                }

                if (CheckKey(Keys.D1))
                {
                    spriteBatch.Draw(healIcon, new Vector2(1, 474), Color.Yellow);
                }
                if (CheckKey(Keys.D2))
                {
                    spriteBatch.Draw(spellIcon, new Vector2(30, 474), Color.Yellow);
                }
                if (CheckKey(Keys.D3))
                {
                    spriteBatch.Draw(aoeIcon, new Vector2(59, 474), Color.Yellow);
                }

            }
            else if (Player.Class == "Knight")
            {
                spriteBatch.Draw(aoeIcon, new Vector2(1, 474), Color.White);
                spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.White);

                if (Player.Mana[0] < 60)
                {
                    spriteBatch.Draw(aoeIcon, new Vector2(1, 474), Color.Red);
                }
                if (Player.Mana[0] < 30)
                {
                    spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.Red);
                }
               
                if (CheckKey(Keys.D1))
                {
                    spriteBatch.Draw(aoeIcon, new Vector2(1, 474), Color.Yellow);
                }
                if (CheckKey(Keys.D2))
                {
                    spriteBatch.Draw(swordIcon, new Vector2(30, 474), Color.Yellow);
                }
            }

            ////////icons colours etc///////////////
            


            spriteBatch.DrawString(spriteFont, Player.Health[0] + "/" + Player.Health[1], new Vector2(135, 517), Color.Red);
            spriteBatch.DrawString(spriteFont, Player.Mana[0] + "/" + Player.Mana[1], new Vector2(135, 543), Color.Blue);
            spriteBatch.Draw(map.player, new Rectangle(20, 510, 40, 40), new Rectangle(0,0,40,40), Color.White);
            spriteBatch.DrawString(spriteFont, "Level: " + Player.Level, new Vector2(10, 555), Color.White);
            if (getItemSTRBonus() != 0)
            {
                spriteBatch.DrawString(spriteFont, "STR: " + (Player.Strength + getItemSTRBonus()), new Vector2(10, 580), Color.LightGreen);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "STR: " + Player.Strength, new Vector2(10, 580), Color.White);
            }
            if (getItemENDBonus() != 0)
            {
                spriteBatch.DrawString(spriteFont, "END: " + (Player.Defence + getItemENDBonus()), new Vector2(75, 580), Color.LightGreen);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "END: " + Player.Defence, new Vector2(75, 580), Color.White);
            }
            if (getItemWISBonus() != 0)
            {
                spriteBatch.DrawString(spriteFont, "WIS: " + (Player.Wisdom + getItemWISBonus()), new Vector2(150, 580), Color.LightGreen);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "WIS: " + Player.Wisdom, new Vector2(150, 580), Color.White);
            }

            #region Item Slots
            //Item Code
            //This draws the players items to the screen, for empty slots an empty sprite is used. 
            //Update Else for unique item sprites
            if (Player.items[0] == 0)
            {
                spriteBatch.Draw(slot1Sprite, new Vector2(532, 510), Color.White);
            }
            else
            {
                spriteBatch.Draw(testItemSprtie, new Vector2(532, 510), itemControl.getColour(Player.items[0]));
            }
            if (Player.items[1] == 0)
            {
                spriteBatch.Draw(slot2Sprite, new Vector2(576, 510), Color.White);
            }
            else
            {
                spriteBatch.Draw(testItemSprtie, new Vector2(576, 510), itemControl.getColour(Player.items[1]));
            }
            if (Player.items[2] == 0)
            {
                spriteBatch.Draw(slot3Sprite, new Vector2(532, 555), Color.White);
            }
            else
            {
                spriteBatch.Draw(testItemSprtie, new Vector2(532, 555), itemControl.getColour(Player.items[2]));
            }
            if (Player.items[3] == 0)
            {
                spriteBatch.Draw(slot4Sprite, new Vector2(576, 555), Color.White);
            }
            else
            {
                spriteBatch.Draw(testItemSprtie, new Vector2(576, 555), itemControl.getColour(Player.items[3]));
            }
            //Item Code 
            #endregion

            foreach (MobComponent thisMob in mobControl.mobs)
            {
                if (thisMob.isSelected == true)
                {
                    spriteBatch.DrawString(spriteFont, "Enemy Info: ", new Vector2(625, 505), Color.White);
                    spriteBatch.Draw(thisMob.sprite, new Rectangle(630, 520, 40, 40),new Rectangle(0,0,40,40), Color.White);
                    spriteBatch.DrawString(spriteFont, "Health: " + thisMob.Health[0].ToString() + "/" + thisMob.Health[1].ToString(), new Vector2(690, 520), Color.Red);
                    spriteBatch.DrawString(spriteFont, "STR: " + thisMob.Strength, new Vector2(690, 532), Color.White);
                    spriteBatch.DrawString(spriteFont, "END: " + thisMob.Endurance, new Vector2(690, 544), Color.White);
                    spriteBatch.DrawString(spriteFont, "WIS: " + thisMob.Wisdom, new Vector2(690, 556), Color.White);
                }
            }
            foreach (dropItem thisItem in itemControl.dropList)//Item Code
            {
                if (thisItem.isSelected == true & (thisItem.itemNumber != 0))
                {
                    spriteBatch.DrawString(spriteFont, "Item Info: ", new Vector2(625, 505), Color.White);
                    spriteBatch.DrawString(spriteFont, "Name: " +  itemControl.getName(thisItem.itemNumber), new Vector2(625, 516), Color.White);
                    spriteBatch.Draw(testItemSprtie, new Vector2(630, 535), itemControl.getColour(thisItem.itemNumber));
                    spriteBatch.DrawString(spriteFont, "STR: +" + itemControl.getStr(thisItem.itemNumber), new Vector2(690, 532), Color.White);
                    spriteBatch.DrawString(spriteFont, "END: +" + itemControl.getEnd(thisItem.itemNumber), new Vector2(690, 544), Color.White);
                    spriteBatch.DrawString(spriteFont, "WIS: +" + itemControl.getWis(thisItem.itemNumber), new Vector2(690, 556), Color.White);
                }
            }
            if (itemControl.selected != -1)
            {
                if (Player.items[itemControl.selected] != 0)//New line or you get an index out of range error.
                    {
                        spriteBatch.DrawString(spriteFont, "Item Info: ", new Vector2(625, 505), Color.White);
                        spriteBatch.DrawString(spriteFont, "Name: " + itemControl.getName(Player.items[itemControl.selected]), new Vector2(625, 516), Color.White);
                        spriteBatch.Draw(testItemSprtie, new Vector2(630, 535), itemControl.getColour(Player.items[itemControl.selected]));
                        spriteBatch.DrawString(spriteFont, "STR: +" + itemControl.getStr(Player.items[itemControl.selected]), new Vector2(690, 532), Color.White);
                        spriteBatch.DrawString(spriteFont, "END: +" + itemControl.getEnd(Player.items[itemControl.selected]), new Vector2(690, 544), Color.White);
                        spriteBatch.DrawString(spriteFont, "WIS: +" + itemControl.getWis(Player.items[itemControl.selected]), new Vector2(690, 556), Color.White);
                    }
                }
            spriteBatch.End();
        }

        public void CheckMapInput()
        {
            if ((CheckKey(Keys.W)) & (!CheckKey(Keys.S)) & (!CheckKey(Keys.A)) & (!CheckKey(Keys.D)) & (collisions.Walkable(Keys.W)))
            {
                if (CheckMobCollision(Keys.W) == 0)
                {
                    map.CheckMapMovementKeys(Keys.W);
                }
            }
            else if ((!CheckKey(Keys.W)) & (CheckKey(Keys.S)) & (!CheckKey(Keys.A)) & (!CheckKey(Keys.D)) & (collisions.Walkable(Keys.S)))
            {
                if (CheckMobCollision(Keys.S) == 0)
                {
                    map.CheckMapMovementKeys(Keys.S);
                }
            }
            else if ((!CheckKey(Keys.W)) & (!CheckKey(Keys.S)) & (CheckKey(Keys.A)) & (!CheckKey(Keys.D)) & (collisions.Walkable(Keys.A)))
            {
                if (CheckMobCollision(Keys.A) == 0)
                {
                    map.CheckMapMovementKeys(Keys.A);
                }
            }
            else if ((!CheckKey(Keys.W)) & (!CheckKey(Keys.S)) & (!CheckKey(Keys.A)) & (CheckKey(Keys.D)) & (collisions.Walkable(Keys.D)))
            {
                if (CheckMobCollision(Keys.D) == 0)
                {
                    map.CheckMapMovementKeys(Keys.D);
                }
            }
            else
            {
                map.playerSprite.Walking = false;
            }


        }

        public int CheckMobCollision(Keys theKey)
        {
            icollisions = 0;
            if (theKey == Keys.W)
            {
                foreach (MobComponent thisMob in mobControl.mobs)
                {
                    if (thisMob.position[0] == map.position[0] && thisMob.position[1] == map.position[1] - 1 && thisMob.alive == true)
                    {
                        icollisions++;
                    }
                }
            }

            if (theKey == Keys.S)
            {
                foreach (MobComponent thisMob in mobControl.mobs)
                {
                    if (thisMob.position[0] == map.position[0] && thisMob.position[1] == map.position[1] + 1 && thisMob.alive == true)
                    {
                        icollisions++;
                    }
                }
            }

            if (theKey == Keys.A)
            {
                foreach (MobComponent thisMob in mobControl.mobs)
                {
                    if (thisMob.position[0] == map.position[0] - 1 && thisMob.position[1] == map.position[1] && thisMob.alive == true)
                    {
                        icollisions++;
                    }
                }
            }

            if (theKey == Keys.D)
            {
                foreach (MobComponent thisMob in mobControl.mobs)
                {
                    if (thisMob.position[0] == map.position[0] + 1 && thisMob.position[1] == map.position[1] && thisMob.alive == true)
                    {
                        icollisions++;
                    }
                }
            }


            return icollisions;
        }

        private void UpdateMobs(GameTime gameTime)
        {
            allies.playerposition = map.position;
            allies.iMapX = map.iMapX;
            allies.iMapY = map.iMapY;
            //allies.iTileOffsetX = map.iPlayerAvatarSubTileX;
            //allies.iTileOffsetY = map.iPlayerAvatarSubTileY;
            mobControl.iMapX = map.iMapX;
            mobControl.iMapY = map.iMapY;
            mobControl.playerposition = map.position;
            foreach (MobComponent thisMob in mobControl.mobs)
            {
                if (thisMob.alive == true)
                {
                    if (thisMob.Health[0] <= 0)
                    {
                        thisMob.isSelected = false;
                        thisMob.alive = false;
                        Player.Experience += thisMob.Experience;
                        if (!thisMob.isBoss)
                        {
                            if (RandomNumber(1, 5) == 1)
                            {
                                itemControl.dropNewItem(thisMob.position[0], thisMob.position[1], RandomNumber(1, 130));
                            }
                        }
                        else
                        {
                            itemControl.dropNewItem(thisMob.position[0], thisMob.position[1], RandomNumber(130,159));
                        }
                    }
                }
            }
        }

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public int getItemSTRBonus()
        {
            int str = 0;
            str += itemControl.getStr(Player.items[0]);
            str += itemControl.getStr(Player.items[1]);
            str += itemControl.getStr(Player.items[2]);
            str += itemControl.getStr(Player.items[3]);
            return str;
        }

        public int getItemENDBonus()
        {
            int end = 0;
            end += itemControl.getEnd(Player.items[0]);
            end += itemControl.getEnd(Player.items[1]);
            end += itemControl.getEnd(Player.items[2]);
            end += itemControl.getEnd(Player.items[3]);
            return end;
        }

        public int getItemWISBonus()
        {
            int wis = 0;
            wis += itemControl.getWis(Player.items[0]);
            wis += itemControl.getWis(Player.items[1]);
            wis += itemControl.getWis(Player.items[2]);
            wis += itemControl.getWis(Player.items[3]);
            return wis;
        }

        public void UpdateCombat(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fTotalElapsedTime += elapsed;

            if (fTotalElapsedTime >= 0.5)
            {
                foreach (MobComponent thisMob in mobControl.mobs)
                {
                    foreach (AllyComponent thisAlly in allies.allies)
                    {
                        if ((thisMob.position[0] <= thisAlly.position[0] + 1 && thisMob.position[0] >= thisAlly.position[0] - 1) &&
                            (thisMob.position[1] <= thisAlly.position[1] + 1 && thisMob.position[1] >= thisAlly.position[1] - 1) &&
                            thisMob.alive == true && thisAlly.alive == true)
                        {
                            int Damage = (int)((thisMob.Strength * 1.5 + 5) - (thisAlly.Endurance * 0.2 - 2));
                            thisAlly.Health[0] -= Damage;

                            
                            Damage = (int)((thisAlly.Strength * 1.5 + 5) - (thisMob.Endurance * 0.2 - 2));
                            thisMob.Health[0] -= Damage;
                        }
                    }

                    if (thisMob.position[0] <= map.position[0] + 1 && thisMob.position[0] >= map.position[0] - 1 &&
                        thisMob.position[1] <= map.position[1] + 1 && thisMob.position[1] >= map.position[1] - 1 &&
                        thisMob.alive == true)
                    {
                        int Damage = (int)((thisMob.Strength * 1.5 + 5) - (Player.Defence + getItemENDBonus() * 0.2 - 2));
                        Player.Health[0] -= Damage;
                        if (thisMob.isSelected == true)
                        {
                            map.playerSprite.Attacking = true;
                            Damage = (int)(((Player.Strength + getItemSTRBonus()) * 1.5 + 5) - (thisMob.Endurance * 0.2 - 2));
                            thisMob.Health[0] -= Damage;
                        }
                    }
                }
                fTotalElapsedTime = 0;
            }

        }

        public void UpdateDrops()//Item Code
        {
            int i = 0;
            int removePlace = -1;
            foreach (dropItem thisItem in itemControl.dropList)
            {
                if (thisItem.isPickedUp == true)
                {
                    int loop = 0;
                    bool freeSpace = false;
                    while (true)
                    {
                        if (Player.items[loop] == 0)
                        {
                            Player.items[loop] = thisItem.itemNumber;
                            freeSpace = true;
                            removePlace = i;
                            break;
                        }
                        else if (loop == 3)
                        {
                            break;
                        }
                        loop++;
                    }
                    if (freeSpace)
                    {
                        Player.items[loop] = thisItem.itemNumber;
                    }
                }
                if(thisItem.getX() <= map.position[0] + 1 && thisItem.getX() >= map.position[0] -1 &&
                        thisItem.getY() <= map.position[1] + 1 && thisItem.getY() >= map.position[1] -1)
                {
                    thisItem.isNextTo = true;
                }
                else
                {
                    thisItem.isNextTo = false;
                    thisItem.isSelected = false;
                }
                i++;
            }
            if (removePlace != -1)
            {
                itemControl.dropList.RemoveAt(removePlace);
            }
        }

        public void UpdatePlayerItems() //Item Code
        {
            if (itemControl.HUDSelected())
            {
                Player.removeItem(itemControl.selected);
                itemControl.selected = -1;
            }
        }

        public void Regen(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fTotalElapsedTime3 += elapsed;

            if (fTotalElapsedTime3 >= 1.0)
            {
                if (((float)(Player.Health[0] + (float)(Player.Health[1] / 100))) <= (Player.Health[1]))
                {
                    Player.Health[0] = (int)((float)Player.Health[0] + ((float)Player.Health[1] / 100));
                    if (Player.Health[0] > Player.Health[1])
                    {
                        Player.Health[0] = Player.Health[1];
                    }
                }
                else
                {
                    Player.Health[0] = Player.Health[1];
                }

                if (((float)(Player.Mana[0] + (float)(Player.Mana[1] / 100))) <= (Player.Mana[1]))
                {
                    Player.Mana[0] = (int)((float)Player.Mana[0] + ((float)Player.Mana[1] / 100));
                }
                else
                {
                    Player.Mana[0] = Player.Mana[1];
                }

                foreach (AllyComponent thisAlly in allies.allies)
                {
                    if (thisAlly.alive)
                    {
                        if (((float)(thisAlly.Health[0] + (float)(thisAlly.Health[1] / 100))) <= (thisAlly.Health[1]))
                        {
                            thisAlly.Health[0] = (int)((float)thisAlly.Health[0] + ((float)thisAlly.Health[1] / 100));
                        }
                        else
                        {
                            thisAlly.Health[0] = thisAlly.Health[1];
                        }
                    }

                }

                fTotalElapsedTime3 = 0;
            }
        }

        public void playerPots(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fTotalElapsedTime6 += elapsed;
            if (fTotalElapsedTime6 >= 0.5)
            {
                if (CheckKey(Keys.Q))
                {
                    if (Player.healthPot > 0)
                    {
                        if (Player.Health[0] < Player.Health[1])
                        {
                            Player.Health[0] += (Player.Health[1] / 100) * 20;
                            Player.healthPot -= 1;
                            fTotalElapsedTime6 = 0;

                        }
                        if (Player.Health[0] > Player.Health[1])
                        {
                            Player.Health[0] = Player.Health[1];
                        }
                    }
                }
                if (CheckKey(Keys.E))
                {
                    if (Player.manaPot > 0)
                    {
                        if (Player.Mana[0] < Player.Mana[1])
                        {
                            Player.Mana[0] += (Player.Mana[1] / 100) * 20;
                            Player.manaPot -= 1;
                            fTotalElapsedTime6 = 0;

                        }
                        if (Player.Mana[0] > Player.Mana[1])
                        {
                            Player.Mana[0] = Player.Mana[1];
                        }
                    }
                }


            }
        }


        public void playerSkills(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fTotalElapsedTime7 += elapsed;
            if (fTotalElapsedTime7 >= 0.25)
            {
                fTotalElapsedTime7 = 0;
                if (CheckKey(Keys.D1))
                {
                    if (Player.Class == "Warrior")
                    {
                        if (Player.Mana[0] >= 40)
                        {
                            if (!strBuff)
                            {
                                strBuff = true;
                                tempStrength = Player.Strength;
                                Player.Mana[0] -= 40;
                            }
                        }


                    }
                    else if (Player.Class == "Knight")
                    {
                        if (Player.Mana[0] >= 60)
                        {
                            if (!Bubble)
                            {
                                Bubble = true;
                                tempHealth = Player.Health[0];
                                Player.Mana[0] -= 60;
                            }
                        }
                    }
                    else if (Player.Class == "Mage")
                    {
                        if (Player.Health[0] != Player.Health[1])
                        {
                            if (Player.Mana[0] >= 50)
                            {
                                Player.Mana[0] -= 50;
                                if (((float)(Player.Health[0] + (float)((Player.Health[1] / 100) * 25))) <= Player.Health[1])
                                {
                                    Player.Health[0] = (int)((float)(Player.Health[0] + (float)((Player.Health[1] / 100) * 25)));
                                }
                                else
                                {
                                    Player.Health[0] = Player.Health[1];
                                }

                            }
                        }
                    }

                }
                else if (CheckKey(Keys.D2))
                {
                    int Damage;
                    if (Player.Class == "Warrior")
                        foreach (MobComponent thisMob in mobControl.mobs)
                        {
                            if (Player.Mana[0] >= 40)
                            {
                                if (thisMob.position[0] <= map.position[0] + 1 && thisMob.position[0] >= map.position[0] - 1 &&
                                     thisMob.position[1] <= map.position[1] + 1 && thisMob.position[1] >= map.position[1] - 1 &&
                                     thisMob.alive == true)
                                {
                                    if (thisMob.isSelected == true)
                                    {
                                        map.playerSprite.Attacking = true;
                                        Damage = (int)(((Player.Strength + getItemSTRBonus()) * 4.5 + 5) - (thisMob.Endurance * 0.2 - 2));
                                        Player.Mana[0] -= 40;
                                        thisMob.Health[0] -= Damage;
                                    }
                                }
                            }
                        }
                    else if (Player.Class == "Knight")
                        foreach (MobComponent thisMob in mobControl.mobs)
                        {
                            if (Player.Mana[0] >= 30)
                            {
                                if (thisMob.position[0] <= map.position[0] + 1 && thisMob.position[0] >= map.position[0] - 1 &&
                                     thisMob.position[1] <= map.position[1] + 1 && thisMob.position[1] >= map.position[1] - 1 &&
                                     thisMob.alive == true)
                                {
                                    if (thisMob.isSelected == true)
                                    {
                                        map.playerSprite.Attacking = true;
                                        Damage = (int)(((Player.Strength + getItemSTRBonus()) * 3 + 5) - (thisMob.Endurance * 0.2 - 2));
                                        Player.Mana[0] -= 30;
                                        thisMob.Health[0] -= Damage;
                                    }

                                }
                            }
                        }

                    else if (Player.Class == "Mage")
                    {
                        foreach (MobComponent thisMob in mobControl.mobs)
                        {
                            if (Player.Mana[0] >= 50)
                            {
                                if (thisMob.position[0] <= map.position[0] + 10 && thisMob.position[0] >= map.position[0] - 10 &&
                                     thisMob.position[1] <= map.position[1] + 10 && thisMob.position[1] >= map.position[1] - 10 &&
                                     thisMob.alive == true)
                                {
                                    if (thisMob.isSelected == true)
                                    {
                                        map.playerSprite.Attacking = true;
                                        Damage = (int)(((Player.Wisdom + getItemWISBonus()) * 7 + 5) - (thisMob.Wisdom * 0.2 - 4));
                                        Player.Mana[0] -= 50;
                                        thisMob.Health[0] -= Damage;
                                    }
                                }
                            }
                        }
                    }

                }
                else if (CheckKey(Keys.D3))
                {
                    int Damage;
                    if (Player.Class == "Warrior")
                    {
                        foreach (MobComponent thisMob in mobControl.mobs)
                        {
                            if (Player.Mana[0] >= 30)
                            {
                                if (thisMob.position[0] <= map.position[0] + 1 && thisMob.position[0] >= map.position[0] - 1 &&
                                     thisMob.position[1] <= map.position[1] + 1 && thisMob.position[1] >= map.position[1] - 1 &&
                                     thisMob.alive == true)
                                {
                                    map.playerSprite.Attacking = true;
                                    Damage = (int)(((Player.Strength + getItemSTRBonus()) * 2 + 3) - (thisMob.Strength * 0.2 - 4));
                                    Player.Mana[0] -= 30;
                                    thisMob.Health[0] -= Damage;
                                }
                            }
                        }
                    }

                    //else if (Player.Class == "Knight")
                    //{
                    //    Player.Health[0] = 0;
                    //}
                    else if (Player.Class == "Mage")
                    {
                        foreach (MobComponent thisMob in mobControl.mobs)
                        {
                            if (Player.Mana[0] >= 40)
                            {
                                if (thisMob.position[0] <= map.position[0] + 2 && thisMob.position[0] >= map.position[0] - 2 &&
                                     thisMob.position[1] <= map.position[1] + 2 && thisMob.position[1] >= map.position[1] - 4 &&
                                     thisMob.alive == true)
                                {
                                    map.playerSprite.Attacking = true;
                                    Damage = (int)(((Player.Wisdom + getItemWISBonus()) * 1.2 + 6) - (thisMob.Wisdom * 0.2 - 4));
                                    Player.Mana[0] -= 40;
                                    thisMob.Health[0] -= Damage;
                                }

                            }
                        }
                    }
                }
            }
        }

        public void buffTimers(GameTime gameTime)
        {
            if (strBuff || Bubble)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                fTotalElapsedTime4 += elapsed;
                if (strBuff)
                {
                    Player.Strength = (int)(tempStrength * 1.5);
                }
                if (Bubble)
                {
                    if (Player.Health[0] < tempHealth)
                    {
                        Player.Health[0] = tempHealth;
                    }
                }
                if (fTotalElapsedTime4 >= 15.0)
                {
                    if (strBuff)
                    {
                        strBuff = false;
                        Player.Strength = tempStrength;
                        
                    }

                    if (Bubble)
                    {
                        Bubble = false;
                        if (Player.Health[0] < tempHealth)
                        {
                            Player.Health[0] = tempHealth;
                            
                        }
                    }

                    fTotalElapsedTime4 = 0;
                }


            }
            

        }

        //Updates the allie controller with the correct tent values. 
        public void alliesTents()
        {
            allies.nextTo = buildings.nextToTent(map.position[0], map.position[1]);
            if (allies.nextTo)
            {
                allies.nearestTent = buildings.nearestTent(map.position[0], map.position[1]);
            }
        }

    }
}