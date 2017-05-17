using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Storage;






using OmniLibrary;
using OmniLibrary.Controls;
using OmniLibrary.Map;
using OmniLibrary.EnemyEngine;
using OmniLibrary.Towers;



namespace OmniRax.Game_Screens
{
    public class MapScreen:BaseGameState
    {
        #region Fields

        LoadingBar loadBar;

        public LoadingBar LoadBar
        {
            get { return loadBar; }
            set { loadBar = value; }
        }
        
        ObjectManager objManager;
        
        ContentManager content;
        
        Plot currentPlot;
        
        Task keepUpdating;
        
        StageBox stagePreview;
        
        Texture2D statusBar;
        Texture2D backgroundImage;
        Texture2D startButton;
        Texture2D Xblock;
        Texture2D Overlay;
        Dictionary<int, Texture2D> previewTower;
        Dictionary<int, Texture2D> previewMap;
        
        Plot back;
        Plot start;

        PlotPosition cPlotPosition;
        Texture2D texExit;
        PictureBox pbExit;
        PictureBox pbOptions;
        PictureBox pbUpgrades;
        PictureBox pbManual;
        PictureBox pbExperienceIndicator;
        PictureBox pbMenuButton;
        PictureBox pbSoundButtonMute;
        PictureBox pbMusicButtonMute;
        PictureBox pbOverlay;
        StreamWriter writer;
        StreamReader reader;
            

        bool placeLock;
        bool plotTexture;
        bool DevelopmentMode;

        #endregion

        #region Properties

        

        #endregion
        
        public MapScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }

        protected override void LoadContent()
        {
            content = GameRef.Content;
            base.LoadContent();
            previewMap = new Dictionary<int, Texture2D>();
            previewTower = new Dictionary<int, Texture2D>();

            previewMap.Add(0,content.Load<Texture2D>(@"MapParts\Map1_2"));
            previewTower.Add(0, content.Load<Texture2D>(@"Towers\TowerIdleFire"));
            previewTower.Add(1, content.Load<Texture2D>(@"Towers\Lt"));
            Overlay = content.Load<Texture2D>(@"UI Content\MapScreen\MapScreenOverLay");
            objManager = new ObjectManager(base.menuFont);
            //stagePreview = new StageBox(content.Load<Texture2D>(@"UI Content\Login\StartMenuBackRound"), new Rectangle(100, 100, GameRef.ScreenRectangle.Width - 200, GameRef.ScreenRectangle.Height - 200)
            //, previewMap, previewTower, "JARRYD PLACE TEXT HERE",content.Load<SpriteFont>(@"Fonts\Descriptionfont"),0,objManager);
            texExit = content.Load<Texture2D>(@"Ui Content\GamePlay\Exit");
            statusBar = content.Load<Texture2D>(@"UI Content\Buttons_2");

            pbExit = new PictureBox(texExit, new Rectangle(GameRef.ScreenRectangle.Width - texExit.Width / 2 - 70, 0, 70, 70), 1, 1, 1);
            pbExit.obj_Selected += new EventHandler(pbExit_obj_Selected);
            pbExit.obj_MouseOver += new EventHandler(pbExit_obj_MouseOver);
            pbExit.obj_Leave += new EventHandler(pbExit_obj_Leave);
            backgroundImage = content.Load<Texture2D>(@"UI Content\MapScreen\EnterNameMap");
            startButton = content.Load<Texture2D>(@"UI Content\BoxStart");
            
            Xblock = content.Load<Texture2D>(@"UI Content\back");


            loadBar = new LoadingBar(statusBar, 3, 1, new Vector2(GameRef.ScreenRectangle.Width / 2 - statusBar.Width / 2, GameRef.ScreenRectangle.Height-statusBar.Height*2), 1);
            loadBar.Visable = false;
            cPlotPosition = new PlotPosition(); 
            cPlotPosition.Position = new List<Vector2>();
           
             LoadPlotPostion();

             pbOverlay = new PictureBox(Overlay, GameRef.ScreenRectangle, 1, 1, 0);
             
             objManager.AddLst.Add(new PictureBox(backgroundImage,new Rectangle(0,0,GameRef.ScreenRectangle.Width ,GameRef.ScreenRectangle.Height-30),0));
             objManager.AddLst.Add(pbOverlay);
             objManager.AddLst.Add(pbExit);
             objManager.AddLst.Add(loadBar);
             
            

            placeLock = false;
            plotTexture = false;
            DevelopmentMode = false;

            keepUpdating = new Task(new Action(LoadingScreen));
            
            AddPlots();
            
        }

        void pbExit_obj_MouseOver(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.HasFocus = true;
            p.MouseOver = true;
        }

        void pbExit_obj_Leave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.HasFocus = false;
            p.MouseOver = false;
        }

        void pbExit_obj_Selected(object sender, EventArgs e)
        {
            StateManager.PopState();
        }
        protected override void UnloadContent()
        {
            content.Unload();
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.isPop)
            {
                if (!keepUpdating.IsCompleted)
                {
                    keepUpdating = new Task(new Action(LoadingScreen));
                    
                }
                Reset();
            }
            if (Input_Handler.KeyPressed(Keys.F11))
            {
                if (!DevelopmentMode)
                    DevelopmentMode = true;
                else
                    DevelopmentMode = false;
            }
            if (DevelopmentMode)
            {
                if (Input_Handler.KeyPressed(Keys.P))
                {
                    if (plotTexture == false)
                    {
                        GameRef.MousePointer = GameRef.Content.Load<Texture2D>(@"UI Content\Flag");
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
                    
                   cPlotPosition.Position.Add(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                    
                    

                    placeLock = true;
                }
                if(Keyboard.GetState().IsKeyDown(Keys.O))
                {

                    writer = new StreamWriter(@"Txt\Map.txt", true);
                    foreach (Vector2 pos in cPlotPosition.Position)
                    {
                        writer.Write(pos.X.ToString()+'#'+pos.Y.ToString()+'-');

                    }

                    writer.Close();
                }
                if (placeLock == true && Mouse.GetState().LeftButton == ButtonState.Released)
                    placeLock = false;
             
              
            }
            if (keepUpdating.IsCompleted)
            {
                //this.Hide();
               
                StateManager.AddLoaded(GameRef.GamePlayScreen);

            }
            objManager.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);


            objManager.Draw(GameRef.spriteBatch);
            
            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }
        public void currentPlot_Selected(object sender, EventArgs e)
        {
            start = new Plot(startButton, new Vector2(GameRef.ScreenRectangle.Width - startButton.Width -100,GameRef.ScreenRectangle.Height-startButton.Height-100), 1, 1, 1,0,0,0);
            start.obj_Selected += new EventHandler(start_Selected);
            back = new Plot(Xblock, new Vector2(100 + GameRef.ScreenRectangle.Width - 200 -Xblock.Width, 100),1,1,1,0,0,0);
            back.obj_Selected += new EventHandler(back_Selected);
            objManager.AddLst.Add(stagePreview);

            objManager.AddLst.Add(start);
            objManager.AddLst.Add(back);
           
        }
        public void HudSelected(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            if (send == pbExperienceIndicator)
            {
                
            }
            else if(send == pbManual)
            {
                 
            }
            else if (send == pbMenuButton)
	        {
	    	 
	        }
            else if(send == pbMusicButtonMute)
            {
                 
            }
            else if (send == pbOptions)
	        {
	    	 
	        }
            else if(send == pbSoundButtonMute)
            {
                 
            }
            else if (send == pbUpgrades)
	        {
	    	 
	        }
        }
        public void HudMouseOver(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = true;
        }
        public void HudMouseLeave(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = false;
        }
        public void back_Selected(object sender,EventArgs e)
        {
            stagePreview.IsDead = true;
            back.IsDead = true;
            start.IsDead = true;
           
            
        }
        public void start_Selected(object sender, EventArgs e)
        {
            GraveStone g =(GraveStone)sender;
            GameRef.GamePlayScreen.Level = g.GraveStoneId;
            loadBar.Visable = true;
            if (keepUpdating.Status != TaskStatus.Running)
            {
                //stagePreview.IsDead = true;
                //back.IsDead = true;
                //start.IsDead = true;
            }
            
            
            if (keepUpdating.Status != TaskStatus.Running)
            {
                loadBar.Visable = true;
                base.Loadbar = loadBar;


                if (!keepUpdating.IsCompleted)
                {
                    keepUpdating.Start();
                }
                else
                {
                    keepUpdating = new Task(new Action(LoadingScreen));
                    keepUpdating.Start();
                }
                
                    
                    
                
              
            }
        }
        public void AddPlots()
        {
            int counter = 0;
                
                    foreach (Vector2 v2 in cPlotPosition.Position)
                    {
                      
                            Vector2 v3 = new Vector2(v2.X + 25, v2.Y + 25);
                            DropEffect greenGlow = new DropEffect(content.Load<Texture2D>(@"SpellAndProjectiles\heal"), v3, 1, 6, 1, 0, 0);

                            GraveStone p = new GraveStone(content.Load<Texture2D>(@"UI Content\cursed_grave"), v2, 8, 24, 0, 1, 2, greenGlow,counter);

                            p.obj_Selected += new EventHandler(start_Selected);
                            objManager.AddLst.Add(p);
                            counter++;
                        
                    
                    } 
                
               
           
        }
        public void LoadingScreen()
        {
            //GameTime gameTime = new GameTime();
            if (Loadbar.CurrentLoad != 100)
            {
                //    this.Update(gameTime);
                StateManager.loadState(GameRef.GamePlayScreen);
                //    base.Update(gameTime);
                //    base.Draw(gameTime);
                //    this.Draw(gameTime);
            }
            //StateManager.AddLoaded(GameRef.GamePlayScreen);
            
        }
        public void Reset()
        {
            LoadBar.Visable = false;
            LoadBar.CurrentLoad = 0;
            this.isPop = false;
            keepUpdating = new Task(new Action(LoadingScreen));
        }
        public void LoadPlotPostion()
        {
            string[] splitter;
            //using (var file = TitleContainer.OpenStream(@"GameData/MapPlotPositions"))
            //{

            try
            {
                using (var sReader = new StreamReader(@"GameData/MapPlotPositions.txt"))
                {

                    splitter = sReader.ReadLine().Split('-');
                }
            }
            catch (DirectoryNotFoundException)
            {
                
                using (var sReader = new StreamReader(@"GameData/MapPlotPositions.txt"))
                {

                    splitter = sReader.ReadLine().Split('-');
                }
            }
            //}
                    string PostitionsString = "";
                    string[] splitter2;

                    for (int i = 0; i < splitter.Length - 1; i++)
                    {




                        PostitionsString += splitter[i];
                        PostitionsString += "#";


                    }
                    splitter2 = PostitionsString.Split('#');
                    Vector2[] vectors = new Vector2[splitter.Length];
                    int x = 0;
                    int j = 0;
                    for (int i = 0; i < splitter2.Length - 2; i++)
                    {
                        x = i + 1;
                        vectors[j].X = int.Parse(splitter2[i]);
                        vectors[j].Y = int.Parse(splitter2[x]);
                        j++;
                        i++;

                    }
                    for (int i = 0; i < vectors.Length - 1; i++)
                    {
                        cPlotPosition.Position.Add(vectors[i]);
                    }
                
            
            
            

        }
    }
}
