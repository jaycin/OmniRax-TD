using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OmniLibrary;
using OmniLibrary.Map;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;
namespace OmniLibrary.EnemyEngine
{
    public class Wave:GameObject 
    {
        Queue<Vector2>[] waypoints = new Queue<Vector2>[3];
        StreamReader reader;
        int spawnPoint;
        int spawnPointClone;
        Texture2D HpBar;
        Dictionary<string,int> WaveDictionary;
        ContentManager Content;
        ObjectManager objManager;
        MovementGrid Grid;
        Dictionary<string, Texture2D> dicTextures;
        Vector2 vecSpawnPoint;
        List<int> amountLst;
        double timeTotal;
        double timeElasped;
        double delayTime;
        public bool completed;
        public EventHandler Wave_Over;
        
        int skeleton, ant, antFire, antIce, spider, spiderRegular, spiderLarge, werebear, werebearwhite, minotaur, bandit, banditLarge, banditRegular;
        double skeletonT ,antT, antFireT, antIceT, spiderT, spiderRegularT, spiderLargeT, werebearT, werebearwhiteT, minotaurT, banditT, banditLargeT, banditRegularT;
        int  goblinRed,goblinBlue, goblinBlack, goblinCyan, goblinGreen, goblinOrange, goblinPink, goblinPurple, goblinWhite, goblinYellow;
        int endPoint;
        int Id;
        double goblinRedT, goblinBlueT, goblinBlackT, goblinCyanT, goblinGreenT, goblinOrangeT, goblinPinkT, goblinPurpleT, goblinWhiteT, goblinYellowT;
        public int GraveId;
        public double DelayTime
        {
            get { return delayTime; }
            set { delayTime = value; }
        }
        public Wave(Dictionary<string,Texture2D> dicEnemytextures,int spawnpoint, ContentManager contentManager,Texture2D hpBar,MovementGrid grid,ObjectManager objMan,int graveId)
        {
            GraveId = graveId;
            Id = GraveId *50;
            amountLst = new List<int>();
            
            HpBar = hpBar;
            spawnPoint = spawnpoint;
            spawnPointClone = spawnPoint;
            endPoint = 0;
            dicTextures = dicEnemytextures;
            objManager = objMan;
            Grid = grid;
            vecSpawnPoint = new Vector2(Grid.SourceList[Grid.lstStart[spawnPoint]].X, Grid.SourceList[Grid.lstStart[spawnPoint]].Y);
            DelayTime = 3000;
            ant = 0;
            antFire= 0;
            antIce= 0;
            spider= 0;
            spiderRegular= 0;
            spiderLarge= 0;
            werebear= 0;
            werebearwhite= 0;
            minotaur= 0;
            bandit= 0;
            banditLarge= 0;
            banditRegular = 0;
            goblinBlue= 0;
            goblinBlack= 0;
            goblinCyan= 0;
            goblinGreen= 0;
            goblinOrange= 0;
            goblinPink= 0; 
            goblinPurple= 0;
            goblinWhite = 0; 
            goblinYellow = 0;
            
            base.Type = "Wave";

        }
        /// <summary>
        /// Updates the Wave
        /// </summary>
        /// <param name="gametime"></param>
        public override void Update(GameTime gametime)
        {
            timeTotal += gametime.ElapsedGameTime.TotalMilliseconds;
            timeElasped += gametime.ElapsedGameTime.TotalMilliseconds;
           
            if (timeElasped> 500)
            {
                if (spawnPoint != spawnPointClone)
                {
                    spawnPoint = spawnPointClone;
                    endPoint = 1;
                    vecSpawnPoint = new Vector2(Grid.SourceList[Grid.lstSpawn[spawnPoint]].X, Grid.SourceList[Grid.lstSpawn[spawnPoint]].Y);
                }
                else
                {
                    spawnPoint += 1;
                    vecSpawnPoint = new Vector2(Grid.SourceList[Grid.lstSpawn[spawnPoint]].X, Grid.SourceList[Grid.lstSpawn[spawnPoint]].Y);
                }
                if (timeTotal > antT&&antT !=0&&ant !=0)
                {
                    Skeleton s  = new Skeleton(dicTextures["ant"], 8, 32, vecSpawnPoint,endPoint,Id, new HeathBar(HpBar, 4, 1), Grid,"ant");
                    
                    s.obj_Expire+=new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    ant--;
                    Id++;
                    antT += DelayTime;
                }
                if (timeTotal > antFireT && antFireT != 0 && antFire != 0)
                {
                    Skeleton s  =new Skeleton(dicTextures["fireAnt"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "fireAnt");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    antFireT += DelayTime;
                    antFire--;
                    Id++;
                }
                if (timeTotal > antIceT && antIceT != 0&&antIce!= 0)
                {
                    Skeleton s = new Skeleton(dicTextures["iceAnt"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "iceAnt");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    antIceT += DelayTime;
                    antIce--;
                    Id++;
                }
                if (timeTotal > skeletonT && skeletonT != 0 && skeleton != 0)
                {
                    Skeleton s = new Skeleton(dicTextures["skeleton"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "skeleton");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    skeletonT += DelayTime;
                    skeleton--;
                    Id++;
                }
                if (timeTotal > spiderT && spiderT != 0 && spider != 0 )
                {
                     Skeleton s = new Skeleton(dicTextures["spider"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "spider");
                     s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    spiderT += DelayTime;
                    spider--;
                    Id++;
                }
                if (timeTotal > spiderRegularT && spiderRegularT != 0&&spiderRegular!=0)
                {
                    Skeleton s =new Skeleton(dicTextures["spiderRegular"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "spiderRegular");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    spiderRegularT += DelayTime;
                    spiderRegular--;
                    Id++;
                }
                if (timeTotal > spiderLargeT && spiderLargeT != 0&&spiderLarge!= 0)
                {
                   Skeleton s =new Skeleton(dicTextures["spiderLarge"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "spiderLarge");
                   s.obj_Expire += new EventHandler(s_obj_Expire);
                   objManager.AddLst.Add(s);
                    spiderLargeT += DelayTime;
                    spiderLarge--;
                    Id++;
                }
                if (timeTotal > banditLargeT && banditLargeT != 0&&banditLarge!=0)
                {
                    Skeleton s = new Skeleton(dicTextures["banditHeavy"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "banditHeavy");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    banditLargeT += DelayTime;
                    banditLarge--;
                    Id++;
                }
                if (timeTotal > banditRegularT && banditRegularT != 0&&banditRegular!=0)
                {
                    Skeleton s = new Skeleton(dicTextures["banditElite"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "banditElite");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    banditRegularT += DelayTime;
                    banditRegular--;
                    Id++;
                }
                if (timeTotal > banditT && banditT != 0&&bandit!=0)
                {
                    Skeleton s= new Skeleton(dicTextures["bandit"], 8, 32, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "bandit");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    banditT += DelayTime;
                    bandit--;
                    Id++;
                }
                if (timeTotal > werebearT && werebearT != 0&&werebear!=0)
                {
                    Skeleton s =new Skeleton(dicTextures["werebear"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "werebear");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    werebearT += DelayTime;
                    werebear--;
                    Id++;
                }
                if (timeTotal > werebearwhiteT && werebearwhiteT != 0 && werebearwhite!=0)
                {
                    Skeleton s = new Skeleton(dicTextures["werebearWhite"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "werebearWhite");
                    s.obj_Expire += new EventHandler(s_obj_Expire);
                    objManager.AddLst.Add(s);
                    werebearwhiteT += DelayTime;
                    werebearwhite--;
                    Id++;
                }
                if (timeTotal > goblinBlackT && goblinBlackT != 0 && goblinBlack != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinBlack"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinBlack"));
                    goblinBlackT += DelayTime;
                    goblinBlack--;
                    Id++;
                }
                if (timeTotal > goblinBlueT && goblinBlueT != 0&& goblinBlue != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinBlue"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinBlue"));
                    goblinBlueT += DelayTime;
                    goblinBlue--;
                    Id++;
                }
                if (timeTotal > goblinCyanT && goblinCyanT != 0&& goblinCyanT!= 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinCyan"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinCyan"));
                    goblinCyanT += DelayTime;
                     goblinCyan--;
                     Id++;
                }
                if (timeTotal > goblinGreenT && goblinGreenT != 0&& goblinGreen != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinGreen"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinGreen"));
                    goblinGreenT += DelayTime;
                    goblinGreen--;
                    Id++;
                }
                if (timeTotal > goblinOrangeT && goblinOrangeT != 0
                    && goblinOrange != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinOrange"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinOrange"));
                    goblinOrangeT += DelayTime;
                    goblinOrange--;
                    Id++;
                }
                if (timeTotal > goblinPinkT && goblinPinkT != 0 && goblinPink != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinPink"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinPink"));
                    goblinPinkT += DelayTime;
                    goblinPink--;
                    Id++;
                }
                if (timeTotal > goblinPurpleT && goblinPurpleT != 0 && goblinPurple != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinPurple"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinPurple"));
                    goblinPurpleT += DelayTime;
                    goblinPurple--;
                    Id++;
                }
                if (timeTotal > goblinRedT && goblinRedT != 0 && goblinRed != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinRed"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinRed"));
                    goblinRedT += DelayTime;
                    goblinRed--;
                    Id++;
                }
                if (timeTotal > goblinWhiteT && goblinWhiteT != 0 && goblinWhite != 0
                    )
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinWhite"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinWhite"));
                    goblinWhiteT += DelayTime;
                    goblinWhite--;
                    Id++;
                }
                if (timeTotal > goblinYellowT && goblinYellowT != 0 && goblinYellow != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["goblinYellow"], 8, 28, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "goblinYellow"));
                    goblinYellowT += DelayTime;
                    goblinYellow--;
                    Id++;
                }
                if (timeTotal > minotaurT && minotaurT != 0 && minotaur != 0)
                {
                    objManager.AddLst.Add(new Skeleton(dicTextures["minoraur"], 8, 24, vecSpawnPoint, endPoint, Id, new HeathBar(HpBar, 4, 1), Grid, "minoraur"));
                    minotaurT += DelayTime;
                    minotaur--;
                    Id++;
                }
                updateList();

                timeElasped = 0;
            }
        }

        void s_obj_Expire(object sender, EventArgs e)
        {
            
           
           
        }
        public override void Draw(SpriteBatch spriteBatch)
            {
               
            }

        #region Abstract Methods
        public virtual void Wave_completed(object sender, EventArgs e)
        {
            if (Wave_Over != null)
            {
                this.Wave_Over(this, null);
            }
            else
            {
                this.IsDead = true;
            }
        }
        /// <summary>
        /// sets the amount of enemies for the current wave
        /// </summary>
        /// <param name="Skeleton"></param>
        /// <param name="Ant"></param>
        /// <param name="AntFire"></param>
        /// <param name="AntIce"></param>
        /// <param name="Spider"></param>
        /// <param name="SpiderRegular"></param>
        /// <param name="SpiderLarge"></param>
        /// <param name="Werebear"></param>
        /// <param name="WerebearWhite"></param>
        /// <param name="Minotaur"></param>
        /// <param name="Bandit"></param>
        /// <param name="BanditLarge"></param>
        /// <param name="BanditRegular"></param>
        public void setWave(int Skeleton, int Ant, int AntFire, int AntIce, int Spider, int SpiderRegular
            , int SpiderLarge, int Werebear, int WerebearWhite, int Minotaur, int Bandit, int BanditLarge, int BanditRegular)
        {
            skeleton = Skeleton;
            ant = Ant;
            antFire = AntFire;
            antIce = AntIce;
            spider = Spider;
            spiderRegular = SpiderRegular;
            spiderLarge = SpiderLarge;
            werebear = Werebear;
            werebearwhite = WerebearWhite;
            minotaur = Minotaur;
            bandit = Bandit;
            banditLarge = BanditLarge;
            banditRegular = BanditRegular;
            updateList();
        }
        /// <summary>
        /// sets the amount of goblins for the current wave
        /// </summary>
        /// <param name="GoblinRed"></param>
        /// <param name="GoblinBlue"></param>
        /// <param name="GoblinBlack"></param>
        /// <param name="GoblinCyan"></param>
        /// <param name="GoblinGreen"></param>
        /// <param name="GoblinOrange"></param>
        /// <param name="GoblinPink"></param>
        /// <param name="GoblinPurple"></param>
        /// <param name="GoblinWhite"></param>
        /// <param name="GoblinYellow"></param>
        public void setGoblins(int GoblinRed, int GoblinBlue, int GoblinBlack, int GoblinCyan, int GoblinGreen, int GoblinOrange,
            int GoblinPink, int GoblinPurple, int GoblinWhite, int GoblinYellow)
        {
            goblinRed = GoblinRed;
            goblinBlue = GoblinBlue;
            goblinBlack = GoblinBlack;
            goblinCyan = GoblinCyan;
            goblinGreen = GoblinGreen;
            goblinOrange = GoblinOrange;
            goblinPink = GoblinPink;
            goblinPurple = GoblinPurple;
            goblinWhite = GoblinWhite;
            goblinYellow = GoblinYellow;
        }
        /// <summary>
        /// sets the time delay until they start spawning
        /// enemies will spawn periodically afterwards
        /// </summary>
        /// <param name="skeleton"></param>
        /// <param name="Ant"></param>
        /// <param name="AntFire"></param>
        /// <param name="AntIce"></param>
        /// <param name="Spider"></param>
        /// <param name="SpiderRegular"></param>
        /// <param name="SpiderLarge"></param>
        /// <param name="Werebear"></param>
        /// <param name="WerebearWhite"></param>
        /// <param name="Minotaur"></param>
        /// <param name="Bandit"></param>
        /// <param name="BanditLarge"></param>
        /// <param name="BanditRegular"></param>
        public void setTime(double skeleton,double Ant, double AntFire, double AntIce, double Spider, double SpiderRegular
            , double SpiderLarge, double Werebear, double WerebearWhite, double Minotaur, double Bandit, double BanditLarge, double BanditRegular)
        {
            skeletonT = skeleton*1000;
            antT = Ant * 1000;
            antFireT = AntFire * 1000;
            antIceT = AntIce * 1000;
            spiderT = Spider * 1000;
            spiderRegularT = SpiderRegular * 1000;
            spiderLargeT = SpiderLarge * 1000;
            werebearT = Werebear * 1000;
            werebearwhiteT = WerebearWhite * 1000;
            minotaurT = Minotaur * 1000;
            banditT = Bandit * 1000;
            banditLargeT = BanditLarge * 1000;
            banditRegularT = BanditRegular * 1000;
        }
        /// <summary>
        /// sets the time delay until they start spawning
        /// enemies will spawn periodically afterwards
        /// </summary>
        /// <param name="GoblinRed"></param>
        /// <param name="GoblinBlue"></param>
        /// <param name="GoblinBlack"></param>
        /// <param name="GoblinCyan"></param>
        /// <param name="GoblinGreen"></param>
        /// <param name="GoblinOrange"></param>
        /// <param name="GoblinPink"></param>
        /// <param name="GoblinPurple"></param>
        /// <param name="GoblinWhite"></param>
        /// <param name="GoblinYellow"></param>
        public void setGoblinTimes(double GoblinRed, double GoblinBlue, double GoblinBlack, double GoblinCyan, double GoblinGreen, double GoblinOrange,
            double GoblinPink, double GoblinPurple, double GoblinWhite, double GoblinYellow)
        {
            goblinRedT = GoblinRed;
            goblinBlueT = GoblinBlue;
            goblinBlackT = GoblinBlack;
            goblinCyanT = GoblinCyan;
            goblinGreenT = GoblinGreen;
            goblinOrangeT = GoblinOrange;
            goblinPinkT = GoblinPink;
            goblinPurpleT = GoblinPurple;
            goblinWhiteT = GoblinWhite;
            goblinYellowT = GoblinYellow;
        }
        public void updateList()
        {
            amountLst.Clear();
            amountLst.Add(skeleton);
            amountLst.Add(ant);
            amountLst.Add(antFire);
            amountLst.Add(antIce);
            amountLst.Add(spider);
            amountLst.Add(spiderLarge);
            amountLst.Add(spiderRegular);
            amountLst.Add(werebear);
            amountLst.Add(werebearwhite);
            amountLst.Add(minotaur);
            amountLst.Add(bandit);
            amountLst.Add(banditLarge);
            amountLst.Add(banditRegular);
        }
        public bool checkEnd()
        {
            bool end = false;
            foreach (int x in amountLst)
            {
                if (x == 0)
                {
                    end = true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion


    }
}
