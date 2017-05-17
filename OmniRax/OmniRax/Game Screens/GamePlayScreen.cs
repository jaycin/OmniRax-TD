using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


using OmniLibrary;
using OmniLibrary.Controls;
using OmniLibrary.Map;
using OmniLibrary.EnemyEngine;
using OmniLibrary.Towers;

namespace OmniRax.Game_Screens
{

    public class GamePlayScreen :BaseGameState
    {
        #region Fields
        float StoryTime;
        float DeadTime;
        float Counter;
        int StartingGold;
        Dictionary<string, Texture2D> dicEnemyTextures;
        ObjectManager ObjManager;
        MapLandscape LandScape;
        //Container container;
        Texture2D texContainer;
        //Enemies
        List<Texture2D> lstTexAntlion;
        List<Texture2D> lstTexOrcs;
        List<Texture2D> lstTexSpiders;
        List<Texture2D> lstTexGoblin;
        List<Texture2D> lstTexWerebear;
        Texture2D texSkeleton;
        Texture2D texMinotaur;
        
        //Reinforcements
        List<Texture2D> lstTexReinforcements;
        //Hero's
        List<Texture2D> lstTexHero;
        List<Texture2D> lstTexHeroine;
        //Projectiles and Spells
        List<Texture2D> lstTexCoins;
        List<Texture2D> lstTexRuned;
        List<Texture2D> lstTexMageProjectile;
        List<Texture2D> lstTexMaps;
        Texture2D texExplosion;
        Texture2D texFreeze;
        Texture2D texArrows;
        Texture2D texQuake;
        Texture2D texSparks;
        Texture2D texShock;
        //User Interface
        Texture2D texStatusBar;
        Texture2D texHPBar;
        Texture2D texTeleport;
        Texture2D texMessageBox;
        MessageBox towerSelector;
        //towers
        Texture2D FireIdle;
        Texture2D IceIdle;
        StatusBar StatusBar;
        Texture2D texBarracks;
        Texture2D texTurorial;
        Tutorial tutTutorial;
        //Screens that contain loadbars
        MapScreen mapScreen;
        Texture2D MapGridBlock;
        Texture2D texGameOver;
        PictureBox pbGameOver;
        Texture2D texExit;
        PictureBox pbExit;
        Texture2D texVictory;
        PictureBox pbVictory;
        Texture2D texUiTutor;
        PictureBox pbUiTutorial;
        DropEffect greenGlow;
        Texture2D texGraveStones;
        List<GraveStone> GraveStones;
        MovementGrid Grid;
        List<Rectangle> BaseGrid;


        
        



        StreamWriter writer;
        StreamReader reader;
        ContentManager Content;
        List<Plot> plots = new List<Plot>();
        bool DevelopmentMode = false;
        bool plotTexture = false;
        bool placeLock = false;
        bool hasPopped = false;
        double time;
        int counter;
        int Damage;
        int Gold;
        int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        Queue<Vector2>[] waypoints = new Queue<Vector2>[3];
        public Queue<Vector2>[] Waypoints
        {
            get { return waypoints; }
            set { waypoints = value; }
        }

        Vector2[] spawn = new Vector2[3];
        private EventHandler GameOver;
        private EventHandler Back;


        
        
        #endregion

        #region Properties

        #endregion


        #region Constructors


        public GamePlayScreen(Game1 game, GameStateManager manager) 
            : base(game, manager) 
        {

        }

        #endregion

        #region XNA
        public override void Initialize()
        {
            

                mapScreen = (MapScreen)Game.Components[Game.Components.Count-2];
            

            base.Initialize();
            
            
            
        }
        protected override void LoadContent()
        {
            
            GameOver += new EventHandler(YouLose);
            Content = Game.Content;
            base.LoadContent();
            time = 0;

            mapScreen.Loadbar.CurrentLoad = 10; 

            menuFont = Content.Load<SpriteFont>(@"Fonts/ControlFont");
            lstTexMaps = new List<Texture2D>();
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\Map1_2"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Entarya"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Top_Entarya"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Sempridge"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Top_Sempridge"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Snodom"));
            lstTexMaps.Add(Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Top_Snodom"));

            dicEnemyTextures = new Dictionary<string, Texture2D>();
            //enemies
            lstTexAntlion = new List<Texture2D>();
            lstTexAntlion.Add(Content.Load<Texture2D>(@"Enemies\antlion"));
            dicEnemyTextures.Add("ant",lstTexAntlion[0]);
            lstTexAntlion.Add(Content.Load<Texture2D>(@"Enemies\fire_ant"));
            dicEnemyTextures.Add("fireAnt",lstTexAntlion[1]);
            lstTexAntlion.Add(Content.Load<Texture2D>(@"Enemies\ice_ant"));
            dicEnemyTextures.Add("iceAnt",lstTexAntlion[2]);
            lstTexOrcs = new List<Texture2D>();
            lstTexOrcs.Add(Content.Load<Texture2D>(@"Enemies\orc_regular_0"));
            dicEnemyTextures.Add("bandit",lstTexOrcs[0]);
            lstTexOrcs.Add(Content.Load<Texture2D>(@"Enemies\orc_heavy_1"));
            dicEnemyTextures.Add("banditHeavy", lstTexOrcs[1]);
            lstTexOrcs.Add(Content.Load<Texture2D>(@"Enemies\orc_elite_0"));
            dicEnemyTextures.Add("banditElite", lstTexOrcs[2]);
            lstTexOrcs.Add(Content.Load<Texture2D>(@"Enemies\orc_archer_0"));
            dicEnemyTextures.Add("banditArcher", lstTexOrcs[3]);
            lstTexSpiders = new List<Texture2D>();
            lstTexSpiders.Add(Content.Load<Texture2D>(@"Enemies\spider_large"));
            lstTexSpiders.Add(Content.Load<Texture2D>(@"Enemies\spider_0"));
            lstTexSpiders.Add(Content.Load<Texture2D>(@"Enemies\spider_giant"));
            dicEnemyTextures.Add("spiderLarge", lstTexSpiders[0]);            
            dicEnemyTextures.Add("spider", lstTexSpiders[1]);
            dicEnemyTextures.Add("spiderRegular", lstTexSpiders[2]);
            lstTexGoblin = new List<Texture2D>();
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_black"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_blue"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_cyan"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_green"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_orange"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_pink"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_purple"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_red"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_white"));
            lstTexGoblin.Add(Content.Load<Texture2D>(@"Enemies\goblin_lumberjack_yellow"));
            dicEnemyTextures.Add("goblinBlack", lstTexSpiders[0]);

            dicEnemyTextures.Add("goblinBlue", lstTexGoblin[1]);
            dicEnemyTextures.Add("goblinCyan", lstTexGoblin[2]);
            dicEnemyTextures.Add("goblinGreen", lstTexGoblin[3]);
            dicEnemyTextures.Add("goblinOrange", lstTexGoblin[4]);
            dicEnemyTextures.Add("goblinPink", lstTexGoblin[5]);
            dicEnemyTextures.Add("goblinPurple", lstTexGoblin[6]);
            dicEnemyTextures.Add("goblinRed", lstTexGoblin[7]);
            dicEnemyTextures.Add("goblinWhite", lstTexGoblin[8]);
            dicEnemyTextures.Add("goblinYellow", lstTexGoblin[9]);
            lstTexWerebear = new List<Texture2D>();
            lstTexWerebear.Add(Content.Load<Texture2D>(@"Enemies\wereBear_0"));
            dicEnemyTextures.Add("werebear",lstTexWerebear[0]);
            lstTexWerebear.Add(Content.Load<Texture2D>(@"Enemies\wereBear_white"));
            dicEnemyTextures.Add("werebearWhite", lstTexWerebear[1]);

                mapScreen.Loadbar.CurrentLoad = 20;
            
            
            //lstTexDragon = new List<Texture2D>();
            //lstTexDragon.Add(Content.Load<Texture2D>(@"Enemies\wyvern_noshadow"));
            //lstTexDragon.Add(Content.Load<Texture2D>(@"Enemies\wyvern_water"));
            //lstTexDragon.Add(Content.Load<Texture2D>(@"Enemies\wyvern_fire"));
            //lstTexDragon.Add(Content.Load<Texture2D>(@"Enemies\wyvern_air"));
            //lstTexDragon.Add(Content.Load<Texture2D>(@"Enemies\wyvern_shadow"));
            texMinotaur = Content.Load<Texture2D>(@"Enemies\minotaur");
            dicEnemyTextures.Add("minoraur", texMinotaur);
            texSkeleton = Content.Load<Texture2D>(@"Enemies\skeleton_0");
            dicEnemyTextures.Add("skeleton", texSkeleton);
            //reinforcements
            lstTexReinforcements = new List<Texture2D>();
            mapScreen.Loadbar.CurrentLoad = 30;
            dicEnemyTextures.Add("skeleton", Content.Load<Texture2D>(@"Characters\male_unarmored"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_unarmored"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_light"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_heavy"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\magician"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_longsword"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_longbow"));
            lstTexReinforcements.Add(Content.Load<Texture2D>(@"Characters\male_staff"));
                mapScreen.Loadbar.CurrentLoad =40;

            
            //Hero
            lstTexHero = new List<Texture2D>();
           

          
            
            //Projectiles and Spells
            lstTexCoins = new List<Texture2D>(); 
            lstTexCoins.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\coins5"));
            lstTexCoins.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\coins25"));
            lstTexCoins.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\coins100"));
            lstTexRuned = new List<Texture2D>();

                mapScreen.Loadbar.CurrentLoad = 60;

            lstTexRuned.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\heal"));
            
            lstTexRuned.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\berzerk_rune"));
            lstTexRuned.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\teleport_rune"));
            lstTexRuned.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\shield"));
            
                mapScreen.Loadbar.CurrentLoad = 80;
            

            lstTexMageProjectile = new List<Texture2D>();
            
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\Fireball"));
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\explosion"));
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\icicle_0"));
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\shock"));
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\quake_0"));
            lstTexMageProjectile.Add(Content.Load<Texture2D>(@"SpellAndProjectiles\freeze"));
            
            
            texFreeze = Content.Load<Texture2D>(@"SpellAndProjectiles\freeze");
            texArrows = Content.Load<Texture2D>(@"SpellAndProjectiles\projectiles");
            texQuake = Content.Load<Texture2D>(@"SpellAndProjectiles\quake_0");
            texSparks = Content.Load<Texture2D>(@"SpellAndProjectiles\sparks");
            texShock = Content.Load<Texture2D>(@"SpellAndProjectiles\shock");
            texStatusBar = Content.Load<Texture2D>(@"UI Content\StatusBar1");
            texTeleport = Content.Load<Texture2D>(@"UI Content\teleport_128");
            texMessageBox = Content.Load<Texture2D>(@"UI Content\messageBox");
            texGraveStones = Content.Load<Texture2D>(@"UI Content\cursed_grave");

            FireIdle = Content.Load<Texture2D>(@"Towers\TowerIdleFire");
            IceIdle = Content.Load<Texture2D>(@"Towers\Lt");
            texBarracks = Content.Load<Texture2D>(@"Towers\Barracks");
            texContainer = Content.Load<Texture2D>(@"UI Content\ButtonsSprite");
            texHPBar = Content.Load<Texture2D>(@"UI Content\2D_Bars");
            texTurorial = Content.Load<Texture2D>(@"Ui Content\tutorial");
            texVictory=Content.Load<Texture2D>(@"Ui Content\Victory");
            texUiTutor=Content.Load<Texture2D>(@"Ui Content\wood block"); 
            

           
            //texRetry = Content.Load<Texture2D>(@"")
            //Grid.writeTofFile();


                mapScreen.Loadbar.CurrentLoad = 100;
            


            StartingGold = GameRef.player.gold;
            loadObjMan();
            tutTutorial = new Tutorial(texTurorial, new Vector2(50, 50), 4, 4, 1);

            tutTutorial.Visable = false;

            ObjManager.AddLst.Add(tutTutorial);


            Skeleton s = new Skeleton(dicEnemyTextures["ant"], 8, 32, new Vector2(50, 50), new Vector2(55, 50), 1, new HeathBar(HpBar, 4, 1), Grid, "ant");


            
           
        }

        void pbExit_obj_Leave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.HasFocus = false;
            p.MouseOver = false;
        }

        void pbExit_obj_MouseOver(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.HasFocus = true;
            p.MouseOver = true;
        }

        void controlManager_FocusChanged(object sender, EventArgs e)
        {
        
        }

        public override void Update(GameTime gameTime)
        {
            if(hasPopped)
            {
                Reset();
                hasPopped = false;
            }
            
            StoryTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (StatusBar.Health <= 0)
            {
                GameOver(this, null);
            }
            if(Input_Handler.KeyReleased(Keys.Escape))
                GameRef.Exit();

            if (ObjManager.MoneyGain > 0)
            {
                Gold += ObjManager.TakeMoney();
                StatusBar.Gold += Gold;
                GameRef.player.gold += Gold;
                Gold = 0;
            }
            if (ObjManager.DamageGiven > 0)
            {
                Damage = ObjManager.TakeDamage();
                StatusBar.Health -= Damage;
                Damage = 0;
            }
            if (counter != 5)
            {
                time += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if(Input_Handler.KeyPressed(Keys.F11))
            {
                if(DevelopmentMode == false)
                {
                    DevelopmentMode = true;
                }
                else 
                    DevelopmentMode = false;
            }

            #region Development
            if (DevelopmentMode)
            {
                if(Input_Handler.KeyPressed(Keys.I))
                {
                    //Waypoints.Enqueue(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                }
                if (Input_Handler.KeyPressed(Keys.P))
                {
                    if (plotTexture == false)
                    {
                        GameRef.MousePointer = texTeleport;
                        //gamePlayMouse.Update(gameTime, arrowTexture);
                        plotTexture = true;
                    }
                    else
                    {
                        GameRef.MousePointer = Game.Content.Load<Texture2D>(@"UI Content\Pointer");
                        //gamePlayMouse.Update(gameTime, arrowTexture);
                        plotTexture = false;
                    }

                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && placeLock == false)
                {
                    //save plot position
                    if (level == 0)
                    {
                        writer = new StreamWriter(@"GameData\Plot.txt", true);
                    }
                    else if (level == 1)
                    {

                        writer = new StreamWriter(@"GameData\Plot1.txt", true);
                    }
                    else if (level == 2)
                    {

                        writer = new StreamWriter(@"GameData\Plot2.txt", true);
                    }
                    else if (level == 3)
                    {
                        writer = new StreamWriter(@"GameData\Plot3.txt", true);
                    }
                   
                    writer.WriteLine(Mouse.GetState().X.ToString() + "_" + Mouse.GetState().Y.ToString());
                    writer.Close();

                    ObjManager.AddLst.Add(new Plot(Content.Load<Texture2D>(@"UI Content\teleport_128"), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 5, 1, 0f,0,0,0));

                    placeLock = true;
                }
                if (placeLock == true && Mouse.GetState().LeftButton == ButtonState.Released)
                    placeLock = false;
                if (Input_Handler.KeyPressed(Keys.O))
                {
                    writer = new StreamWriter(@"GameData\Plot.txt", true);
                    writer.Dispose();
                    writer.Close();
                    for (int x = 0; x < ObjManager.Count - 1; x++)
                    {
                        if (ObjManager[x].Type == "Plot")
                        {
                            ObjManager.RemoveAt(x);
                            
                        }
                    }

                }  
            #endregion
                
            }

            ObjManager.Update(gameTime);
            

            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
#region OldDraw
            //GameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.Identity);
            //for (int y = 0; y < map.GetLength(0); y++)
            //{
            //    for (int x = 0; x < map.GetLength(1); x++)
            //    {
            //        GameRef.spriteBatch.Draw(
            //            tileset.Texture,
            //            new Rectangle(
            //                x * Engine.TileWidth,
            //                y * Engine.TileHeight,
            //                Engine.TileWidth,
            //                Engine.TileHeight),
            //                tileset.SourceRectangle[map[x,y]],
            //                Color.White);


            //        base.Draw(gameTime);
            //        GameRef.spriteBatch.End();
            //    }
#endregion

            GameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.Identity);
            
            ObjManager.Draw(GameRef.spriteBatch);
            if (DevelopmentMode)
            {
                GameRef.spriteBatch.DrawString(menuFont, "Development Mode Active", new Vector2(20, 20), Color.Red);
                GameRef.spriteBatch.DrawString(menuFont,"Position x ="+Mouse.GetState().X.ToString()+"Position Y =  "+Mouse.GetState().Y.ToString()+"",new Vector2(40,40),Color.White);
                GameRef.spriteBatch.DrawString(menuFont, "Time"+StoryTime.ToString(), new Vector2(60, 60), Color.Red);
               

                GridLine.renderGrid(GameRef.spriteBatch);
                Grid.Draw(GameRef.spriteBatch);
                

            }

            GameRef.spriteBatch.End();
        }
        public void AddPlots(ContentManager content)
        {
            try
            {
                if (level == 0)
                {
                    reader = new StreamReader(@"GameData/Plot.txt");
                }
                else if (level == 1)
                {
                    reader = new StreamReader(@"GameData/Plot1.txt");
                }
                else if (level == 2)
                {
                    reader = new StreamReader(@"GameData/Plot2.txt");
                }
                else if (level == 3)
                {
                    reader = new StreamReader(@"GameData/Plot3.txt");
                }
            }
            catch (DirectoryNotFoundException)
            {
                
                reader = new StreamReader(@"GameData/Plot.txt");
            }

            string[] XnY;
            int counter = 1;
            while (reader.EndOfStream == false)
            {
                XnY = reader.ReadLine().Split('_');
                if(XnY[0] != "")
                {
                    Plot plot = new Plot(content.Load<Texture2D>(@"UI Content\teleport_128"), new Vector2(int.Parse(XnY[0]), int.Parse(XnY[1])), 5, 1, 0f, 0, 0, counter);
                    plot.obj_Selected += new EventHandler(Plot_Selected);
                    ObjManager.AddLst.Add(plot);
                    counter++;
                }
            }
            reader.Close();
        }
        
        #endregion

        #region Abstract Methods
        List<Wave> lstWave;
        public void LoadWave()
        {
            int counter=0;

            lstWave = new List<Wave>();
            
            GraveStone grave;

            Wave wave = new Wave(dicEnemyTextures, 0, Content, texHPBar, Grid, ObjManager,counter);
            wave.setWave(0,0,0,0,0,0,0,0,0,0,6,2,4);
            wave.setTime(0,0,0,0,0,0,0,0,0,0, 5, 30, 60);
            wave.Wave_Over+= new EventHandler(waveCompleted);
            lstWave.Add(wave);
            counter++;
            Wave wave2 = new Wave(dicEnemyTextures, 2, Content, texHPBar, Grid, ObjManager,counter);
            wave2.setWave(0, 6, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            wave2.setTime(0, 5, 30, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            wave2.Wave_Over += new EventHandler(waveCompleted);
            lstWave.Add(wave2);
            counter++;
            Wave wave3 = new Wave(dicEnemyTextures, 4, Content, texHPBar, Grid, ObjManager,counter);
            wave3.setWave(10, 0, 0, 0, 0, 0, 0, 3, 2, 1, 0, 0, 0);
            wave3.setTime(5, 0, 0, 0, 0, 0, 0, 60, 120, 150, 0, 0, 0);
            wave3.Wave_Over += new EventHandler(waveCompleted);           
            counter++;
            Wave wave4 = new Wave(dicEnemyTextures, 6, Content, texHPBar, Grid, ObjManager,counter);
            wave4.setWave(0, 0, 0, 0, 10, 15, 20, 0, 0, 0, 0, 0, 0);
            wave4.setTime(0, 0, 0, 0, 5, 45, 110, 0, 0, 0, 0, 0, 0);
            wave4.Wave_Over += new EventHandler(waveCompleted);
            greenGlow = new DropEffect(lstTexRuned[0], new Vector2(300, 75), 1, 6, 1, 0, 0);
            grave = new GraveStone(texGraveStones, new Vector2(275, 50), 8, 24, 0, 1, 2, greenGlow, counter, wave4);
            grave.obj_Selected += new EventHandler(grave_obj_MouseOver);
            ObjManager.AddLst.Add(grave);
            lstWave.Add(wave4);
            counter = 0;
        }

        void grave_obj_MouseOver(object sender, EventArgs e)
        {
            GraveStone g = (GraveStone)sender;
            if (!g.HasSpawned)
            {
                g.GreenGlow.Color = Color.Red;
                foreach (Wave w in lstWave)
                {
                    g.HasSpawned = true;
                    ObjManager.AddLst.Add(w);
                }
                
            }
            else
            {
            }
        }
        void Container_selected(object sender , EventArgs e)
        {
            Container c = (Container)sender;
            if (c.Type == "FireTower")
            {
                Vector2 spawnPoint;
                spawnPoint = c.Sender.Position;
                spawnPoint.X += 74;
                spawnPoint.Y += 00;
                MageTower tower = new MageTower(FireIdle,1,18, 1f,spawnPoint,MapGridBlock,false);
                if (GameRef.player.gold >= tower.Cost)
                {
                    ObjManager.AddLst.Add(tower);
                    GameRef.player.gold -= tower.Cost;
                    StatusBar.Gold -= tower.Cost;
                    c.sender.HasTower = true;
                    c.IsEnabled = true;
                }
                else
                {
                    MessageBox cantAfford = new MessageBox(Content.Load<Texture2D>(@"Ui Content\messageBox") , menuFont, "You Cannot Afford This Tower",new Vector2(0,600),2);
                    cantAfford.Color = Color.Red;
                    ObjManager.AddLst.Add(cantAfford);
                }
                

                
               

            }
            else if (c.Type == "IceTower")
            {
                
                Vector2 spawnPoint;
                spawnPoint = c.Sender.Position;
                spawnPoint.X += 70;
                spawnPoint.Y -= 23;
                
                MageTower tower = new MageTower(IceIdle, 2, 10, 1f, spawnPoint, MapGridBlock,true);
                if(tower.Cost <= GameRef.player.gold)
                {
                    ObjManager.AddLst.Add(tower);
                    GameRef.player.gold -= tower.Cost;
                    StatusBar.Gold -= tower.Cost;
                    c.sender.HasTower = true;
                    c.IsEnabled = true;
                }
               
                 else
                {
                    MessageBox cantAfford = new MessageBox(Content.Load<Texture2D>(@"Ui Content\messageBox"), menuFont, "You Cannot Afford This Tower", new Vector2(0, 600), 2);
                    cantAfford.Color = Color.Red;
                    ObjManager.AddLst.Add(cantAfford);
                  
                }

                


            }
            else if (c.Type == "ArcherTower")
            {


            }
            else if (c.Type == "Artilerty")
            {



            }
            else if (c.Type == "Sell")
            {

            }
            
            ObjManager.removeConainers();
        }
        void waveCompleted(object sender, EventArgs e)
        {
            Wave w = (Wave)sender;
            ObjManager.RemoveGrave(w.GraveId);
        }
     

       
        public void Plot_Selected(object sender, EventArgs e)
        {

            Container fire = new Container(texContainer, true, "FireTower", 140, 0, (Plot)sender);
            fire.obj_Selected += new EventHandler(Container_selected);
            Container ice = new Container(texContainer, true, "IceTower", 240, 0, (Plot)sender);
            ice.obj_Selected += new EventHandler(Container_selected);
            List<Container>Towers;
            Towers = new List<Container>();
            Towers.Add(fire);
            Towers.Add(ice);
            towerSelector = new MessageBox(texMessageBox, menuFont, Towers, new Vector2(Mouse.GetState().X, Mouse.GetState().Y),ObjManager);
            ObjManager.AddLst.Add(towerSelector);
            ObjManager.lockPlots();
            //ObjManager.LockTowers();
        }
        public void YouLose(object sender, EventArgs e)
        {
            
            hasPopped = true;
            StateManager.PopState();
           
        }
        public void Reset()
        {
            GameRef.player.gold = StartingGold;
            ObjManager.Clear();
            loadObjMan();

        }
        public void loadObjMan()
        {
            MapGridBlock = Content.Load<Texture2D>(@"MapParts\OmniRax-Map_Entarya");
            Grid = new MovementGrid(GameRef.ScreenRectangle, 32, 32, MapGridBlock,Level);
            BaseGrid = Grid.SourceList;
            ObjManager = new OmniLibrary.ObjectManager(base.menuFont, lstTexMageProjectile, Grid, lstTexCoins);
            if (Level == 0)
            {
                //LandScape = new MapLandscape(lstTexMaps[0], GameRef.ScreenRectangle, 0, spawn, waypoints);
            }
            else if (Level == 1)
            {
                //LandScape = new MapLandscape(lstTexMaps[1], lstTexMaps[2], GameRef.ScreenRectangle, 0, spawn, waypoints);
            }
            else if (Level == 2)
            {
                //LandScape = new MapLandscape(lstTexMaps[3], lstTexMaps[4], GameRef.ScreenRectangle, 0, spawn, waypoints);
            }
            else if (Level == 3)
            {
                //LandScape = new MapLandscape(lstTexMaps[5], lstTexMaps[6], GameRef.ScreenRectangle, 0, spawn, waypoints);
            }
           
            texGameOver = Content.Load<Texture2D>(@"Ui Content\GameOver 1");
            pbGameOver = new PictureBox(texGameOver, GameRef.ScreenRectangle, 1, 1, 0);
            pbGameOver.obj_Selected += new EventHandler(YouLose);
            pbGameOver.obj_MouseOver += new EventHandler(pbExit_obj_MouseOver);
            pbGameOver.obj_Leave += new EventHandler(pbExit_obj_Leave);
            texExit = Content.Load<Texture2D>(@"Ui Content\GamePlay\Exit");
            pbExit = new PictureBox(texExit, new Rectangle(GameRef.ScreenRectangle.Width - texExit.Width / 2 - 70, 0, 70, 70), 1, 1, 1);
            pbExit.obj_Selected += new EventHandler(pbExit_obj_Selected);
            pbExit.obj_MouseOver += new EventHandler(pbExit_obj_MouseOver);
            pbExit.obj_Leave += new EventHandler(pbExit_obj_Leave);
            pbVictory = new PictureBox(texVictory, GameRef.ScreenRectangle, 1, 1, 0);
            pbVictory.obj_Win += new EventHandler(pbVictory_obj_Win);
            pbVictory.obj_Selected += new EventHandler(YouLose);
            pbVictory.obj_MouseOver += new EventHandler(pbExit_obj_MouseOver);
            pbVictory.obj_Leave += new EventHandler(pbExit_obj_Leave);
            pbVictory.Visable = false;
            pbVictory.IsEnabled = false;
            pbUiTutorial = new PictureBox(texUiTutor, new Rectangle(GameRef.ScreenRectangle.Width - texExit.Width / 2 , 0, 70, 70),1,1, 1);
            pbUiTutorial.obj_MouseOver += new EventHandler(pbExit_obj_MouseOver);
            pbUiTutorial.obj_Leave += new EventHandler(pbExit_obj_Leave);
            pbUiTutorial.obj_Selected += new EventHandler(pbUiTutorial_obj_Selected);
            

            StatusBar = new StatusBar(texStatusBar, 5, 1, new Rectangle(0, GameRef.ScreenRectangle.Height - 50, 400, 50));
            StatusBar.Gold = GameRef.player.gold;
            StatusBar.Health = 10;
            StatusBar.Font = Content.Load<SpriteFont>(@"Fonts\HudFont");



            ObjManager.AddLst.Add(LandScape);
            ObjManager.AddLst.Add(StatusBar);
            ObjManager.AddLst.Add(pbExit);
            ObjManager.AddLst.Add(pbUiTutorial);
            //line = Content.Load<Texture2D>(@"MapParts\line");
            AddPlots(Content);
            //LoadWave();
            ObjManager.AddLst.Add(pbVictory);
        }

        void pbUiTutorial_obj_Selected(object sender, EventArgs e)
        {
            tutTutorial = new Tutorial(texTurorial, new Vector2(50, 50), 4, 4, 1);

            tutTutorial.Visable = true;

            ObjManager.AddLst.Add(tutTutorial);
        }

   

        void pbVictory_obj_Win(object sender, EventArgs e)
        {
            ObjManager.removeWaves();
            pbVictory.Visable = true;
            pbVictory.IsEnabled = true;
        }

        void pbExit_obj_Selected(object sender, EventArgs e)
        {

            ObjManager.removeWaves();
            ObjManager.AddLst.Add(pbGameOver);
        }



        #endregion

    }
}
