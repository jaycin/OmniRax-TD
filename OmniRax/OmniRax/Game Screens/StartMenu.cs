using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmniLibrary;
using OmniLibrary.Controls;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;



namespace OmniRax.Game_Screens
{
    public class StartMenu : BaseGameState
    {
        #region Fields
        PictureBox backgroundImage;
        //PictureBox arrowImage;
        PictureBox ThemeImage1;
        ObjectManager objManager;
        //PictureBox midPlaceHolder;
        //PictureBox somethingMid;
        //Label placeHolderTitleScreen;
        LinkLabel startGame;
        LinkLabel loadGame;
        LinkLabel options;
        LinkLabel exitGame;
        SpriteFont xnaFont;
        Label XandYMouse;
        Texture2D arrowTexture;
        PictureBox stainedGlass;
        PictureBox stainedGlass2;
        string GameDescription;
                
        bool isload;

        #endregion

        #region Properties
        #endregion

        #region Constuctors
        public StartMenu(Game1 game,GameStateManager manager):base(game,manager)
        {
            
        }

        
        #endregion

        #region XNA

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            
            isload = true;
            objManager = new ObjectManager(base.menuFont);
            ContentManager Content = Game.Content;
            float scaleR = GameRef.scaleRatio();
            backgroundImage = new PictureBox(Content.Load<Texture2D>(@"UI Content\wood background"),new Rectangle(GameRef.ScreenRectangle.Width,0,GameRef.ScreenRectangle.Width,GameRef.ScreenRectangle.Height), scaleR);
            backgroundImage.Position =new Vector2(GameRef.ScreenRectangle.Width,0);
            ThemeImage1 = new PictureBox(Content.Load<Texture2D>(@"UI Content\OMNIRAX2"),new Rectangle(GameRef.ScreenRectangle.Width/2-250,GameRef.ScreenRectangle.Height/2-350,500,300),1,2,scaleR);
            ThemeImage1.obj_Selected += new EventHandler(menuItem_Selected); 
            arrowTexture = Content.Load<Texture2D>(@"UI Content\Pointer");
            stainedGlass = new PictureBox(Content.Load<Texture2D>(@"UI Content\stained-glass-window"), new Rectangle(0, 0, 270, GameRef.ScreenRectangle.Height), 1);
            stainedGlass2 = new PictureBox(Content.Load<Texture2D>(@"UI Content\stained-glass-window"), new Rectangle(750, 0, 270, GameRef.ScreenRectangle.Height), 1);
            xnaFont = Content.Load<SpriteFont>(@"Fonts\HudFont");
            objManager.Add(new PictureBox(Content.Load<Texture2D>(@"UI Content\BackRound"), GameRef.ScreenRectangle, scaleR));
            objManager.Add(backgroundImage);

            startGame = new LinkLabel(4, Content.Load<Texture2D>(@"UI Content\StartButton"), new Vector2(ThemeImage1.Position.X+30 , ThemeImage1.Position.Y + ThemeImage1.DestinationRectangle.Width/2 +50));
            startGame.Text = "New Game";
            //startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.obj_Selected += new EventHandler(menuItem_Selected);
            startGame.obj_MouseOver += new EventHandler(obj_MouseOver);
            startGame.obj_Leave +=new EventHandler(obj_Leave);

            loadGame = new LinkLabel(4, Content.Load<Texture2D>(@"UI Content\ContinueBtn"),new Vector2(startGame.Position.X, startGame.Position.Y+startGame.Height*2));
            loadGame.Text = "Continue";
            //loadGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            loadGame.obj_Selected+= new EventHandler(menuItem_Selected);
            loadGame.obj_MouseOver += new EventHandler(obj_MouseOver);
            loadGame.obj_Leave +=new EventHandler(obj_Leave);

            options = new LinkLabel(4, Content.Load<Texture2D>(@"UI Content\OptionsButton"), new Vector2(loadGame.Position.X, loadGame.Position.Y + startGame.Height * 2));
            options.Text = "Options";
            //options.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            options.IsLocked = true;
            options.obj_Selected += new EventHandler(menuItem_Selected);
            options.obj_MouseOver+=new EventHandler(obj_MouseOver);
            options.obj_Leave +=new EventHandler(obj_Leave);

            exitGame = new LinkLabel(4, Content.Load<Texture2D>(@"UI Content\ExitBtn"), new Vector2(options.Position.X, options.Position.Y + options.Height * 2));
            exitGame.Text = "Exit";

           // exitGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            exitGame.obj_Selected += new EventHandler(menuItem_Selected);
            exitGame.obj_MouseOver+= new EventHandler(obj_MouseOver);
            exitGame.obj_Leave +=new EventHandler(obj_Leave);

            XandYMouse = new Label("_",base.menuFont);
            XandYMouse.SpriteFont = startGame.SpriteFont;
            XandYMouse.Color = Color.Black;
            
            XandYMouse.Position = new Vector2(0, 0);

            objManager.Add(XandYMouse);

        }

        private void obj_MouseOver(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = true;
        }
        private void obj_Leave(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = false;
        }
        private void menuItem_Selected(object sender, EventArgs e)
        {

                if (sender == startGame)
                {   
                    StateManager.PushState(GameRef.MapScreen);
                    //GameRef.spriteBatch.End();
                }

                if (sender == loadGame)
                {
                    StateManager.PushState(GameRef.MapScreen);
                }
                if (sender == options)
                {
                    //StateManager.PushState(GameRef.OptionScreen);
                }

                if (sender == exitGame)
                {
                    GameRef.Exit();
                } 
            
        }
        public override void Update(GameTime gameTime)
        {

            int time = gameTime.TotalGameTime.Seconds;
                int imageX, imageY;
                imageX = (int)ThemeImage1.Position.X;
                imageY = (int)ThemeImage1.Position.Y;
                imageY++;

                objManager.Update(gameTime);
                
                if (isload)
                {
                    if (backgroundImage.Position.X >= 0)
                    {
                        backgroundImage.Move(-15, 0);
                    }
                    else if (backgroundImage.Position.X <= 0)
                    {
                        objManager.Add(startGame);
                        objManager.Add(loadGame);
                        objManager.Add(options);
                        objManager.Add(exitGame);
                        objManager.Add(ThemeImage1);
                        objManager.Add(stainedGlass);
                        objManager.Add(stainedGlass2);
                        backgroundImage.SetPosition(new Vector2(0, 0));
                        isload = false;
                    }
                }

                XandYMouse.Text = "X =" + Mouse.GetState().X + " Y =" + Mouse.GetState().Y
                + " Time = " + time.ToString() + "Window =" + GraphicsDevice.PresentationParameters.Bounds.ToString();
            
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);
            if (Input_Handler.KeyReleased(Keys.Escape))
                GameRef.Exit();

            objManager.Draw(GameRef.spriteBatch);

            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }
        
        #endregion
    }
}
