using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using OmniLibrary;
using OmniLibrary.Controls;
using OmniLibrary.Structs;

namespace OmniRax.Game_Screens
{
    public class LoginScreen : BaseGameState
    {

        //first page, title screen
        #region Fields
        ContentManager Content;
        Texture2D backgroundImage;
        //Buttons
        Texture2D texCoin;
        Texture2D btnCreateAccount;
        Texture2D btnLogin;
        Texture2D btnPlayGame;
        Texture2D btnLogo;

        //save slots
        Texture2D btnSaveSlot1;
        Texture2D btnSaveSlot2;
        Texture2D btnSaveSlot3;

        Texture2D btnLogonImage;

        Texture2D statusBar;
        
        //end
        StreamReader reader;
        StreamWriter writer;
        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Texture2D button;

        //Changes added
        PictureBox backRound;
        PictureBox pbStartButton;//play button

        PictureBox pbGameLogo;

        PictureBox pbSaveSlot1;
        PictureBox pbSaveSlot2;
        PictureBox pbSaveSlot3;

        PictureBox pbLoginPicture;

        //PictureBox pbCreateAccount;
        //PictureBox pbLogin;
        //PictureBox pbExit;

        Label lblTitle;

        Label lblPlayGame;
        Label lblProfileName;
        Label lblGold;
        Label lblCompletion;
        //Label lblRecommendation;
        Label lblFooterDevelopment;
        Label lblFooterDate;

        //end
        Coin coin;
        LinkLabel startLable;
        LoadingBar loadBar;
        ObjectManager objManager;
        bool KeyDown;

        public LoadingBar LoadBar
        {
            get { return loadBar; }
            set { loadBar = value; }
        }

        #endregion
        //title screen constructor, making use of the objects - Game and GameStateManager
        #region Constructers
        public LoginScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
           // CreatePlayer();
            loadPlayers();
        }
        #endregion

        #region XNA
        protected override void LoadContent()
        {
            KeyDown = false;
            objManager = new ObjectManager(base.menuFont);
            //inheriting the base structure of the gameplay screen from Game1.cs
            Content = GameRef.Content;
            //load this content
            base.LoadContent();



            #region login screen

            labelTitle = Content.Load<SpriteFont>(@"Fonts\LoginPage\Title");
            //labelRecommendation = Content.Load<SpriteFont>(@"Fonts\LoginPage\Recommendation");
            labelFooters = Content.Load<SpriteFont>(@"Fonts\LoginPage\Footer");

            #endregion

            menuFont = Content.Load<SpriteFont>(@"Fonts\ControlFont");
            #region Label
            //added in a overload to set the spritefont you can load in base.menuFont
            //base is basegamestate
            //add more fonts to basegamestate if you want them then call them as follows
            lblTitle = new Label("OmniRax Kingdom\nDefence", labelTitle);
            lblTitle.Color = Color.White;

            lblPlayGame = new Label("Arcade", labelTitle);

            lblProfileName = new Label("Profile",labelFooters);
            lblGold = new Label("Treasury ",labelFooters);
            lblCompletion = new Label("Exit", labelFooters);
            //lblRecommendation = new Label("New here?\nPlease create an\naccount to get the\nfull OmniRax Experience", base.labelRecommendation);
            lblFooterDevelopment = new Label("A Synergy Development Game", labelFooters);
            lblFooterDate = new Label("2012", labelFooters);
            

            ////objects position set
            lblTitle.Position = new Vector2(GameRef.ScreenRectangle.Width / 18, GameRef.ScreenRectangle.Height / 18);

            lblPlayGame.Position = new Vector2((GameRef.ScreenRectangle.Width / 18)*11, (GameRef.ScreenRectangle.Height / 20)*7);

            lblProfileName.Position = new Vector2((GameRef.ScreenRectangle.Width / 18) * 11, (GameRef.ScreenRectangle.Height / 22) * 12);
            lblGold.Position = new Vector2((GameRef.ScreenRectangle.Width / 18) * 11, (GameRef.ScreenRectangle.Height / 21) * 14);
            lblCompletion.Position = new Vector2((GameRef.ScreenRectangle.Width / 18) * 11, (GameRef.ScreenRectangle.Height / 25) * 20);

            //lblRecommendation.Position = new Vector2((GameRef.ScreenRectangle.Width / 10 * 7), (GameRef.ScreenRectangle.Height / 4));
            lblFooterDevelopment.Position = new Vector2(GameRef.ScreenRectangle.Width / 7, (GameRef.ScreenRectangle.Height / 10) * 9);
            lblFooterDate.Position = new Vector2((GameRef.ScreenRectangle.Width / 10) * 8, (GameRef.ScreenRectangle.Height / 10) * 9);

            #endregion

            backgroundImage = Content.Load<Texture2D>(@"UI Content\Login\StartMenuBackRound");
            texCoin = Content.Load<Texture2D>(@"UI Content\185px-Shine_Sprite");
            backRound = new PictureBox(backgroundImage, GameRef.ScreenRectangle, 1,1,0);
            objManager.Add(backRound);
            ////objects added to list
            //objManager.Add(lblTitle);


            ////objManager.Add(lblRecommendation);
            objManager.Add(lblFooterDevelopment);
            //objManager.Add(lblFooterDate);
            //end

            statusBar = Content.Load<Texture2D>(@"UI Content\2d_bars");
            loadBar = new LoadingBar(statusBar, 4, 1, new Vector2(GameRef.ScreenRectangle.Width / 2 - statusBar.Width / 2,
                GameRef.ScreenRectangle.Height / 2 + statusBar.Height), 0);
            loadBar.Visable = false;
            //all content for the display of the title page
           
            //btnCreateAccount = content.Load<Texture2D>(@"UI Content\Login\CreateAccount");
            //btnLogin = content.Load<Texture2D>(@"UI Content\Login\LoginButton");;
            btnPlayGame = Content.Load<Texture2D>(@"UI Content\buttons_2");

            #region Save slots
            //Save slot image load
            btnSaveSlot1 = Content.Load<Texture2D>(@"UI Content\buttons_2");

            #endregion
            
            //logo "button"
            btnLogo = Content.Load<Texture2D>(@"UI Content\Login\loginLogo");

            //loginPicture Display
            btnLogonImage = Content.Load<Texture2D>(@"UI Content\Login\ForcesCollide");

            //logo
            pbGameLogo = new PictureBox(btnLogo, new Rectangle((GameRef.ScreenRectangle.Width/10)*4+2,5, 180, 150),1,1,0);
            objManager.Add(pbGameLogo);

            //login Picture
            pbLoginPicture = new PictureBox(btnLogonImage, new Rectangle(GameRef.ScreenRectangle.Width / 18, (GameRef.ScreenRectangle.Height / 19)*5, 500, 400), 1, 1, 0);
            objManager.Add(pbLoginPicture);

            //progress "button"
            pbSaveSlot3 = new PictureBox(btnSaveSlot1, new Rectangle((GameRef.ScreenRectangle.Width / 11) * 6, (GameRef.ScreenRectangle.Height / 16) * 12, 300, 75), 3, 1, 0);
            objManager.Add(pbSaveSlot3);

           
            pbSaveSlot3.obj_Selected += new EventHandler(exit_selected);
            pbSaveSlot3.obj_MouseOver += new EventHandler(startLable_obj_MouseOver);
            pbSaveSlot3.obj_Leave += new EventHandler(startLable_obj_Leave);

            #region Play Game

            pbStartButton = new PictureBox(btnPlayGame, new Rectangle((GameRef.ScreenRectangle.Width / 11) * 6, (GameRef.ScreenRectangle.Height / 16) * 5, 400, 125), 3, 1, 0);
            pbStartButton.obj_Selected += new EventHandler(startLable_Selected);
            pbStartButton.obj_MouseOver += new EventHandler(startLable_obj_MouseOver);
            pbStartButton.obj_Leave += new EventHandler(startLable_obj_Leave);
            objManager.Add(pbStartButton);

            #endregion

            objManager.Add(lblPlayGame);
            objManager.Add(lblCompletion);
            coin = new Coin(texCoin,new Rectangle(780, 490,50,50),1,1);
            

        }
        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }


        public override void Update(GameTime gameTime)
        {
            //whenever the title screen updates, it updates the ControlManager
            if (Enabled)
            {
                objManager.Update(gameTime);
                base.Update(gameTime);
                if (Input_Handler.KeyDown(Microsoft.Xna.Framework.Input.Keys.F11) && KeyDown == false)
                {
                    KeyDown = true;

                }
                if (KeyDown == true && Input_Handler.KeyReleased(Microsoft.Xna.Framework.Input.Keys.F11))
                {
                    KeyDown = false;
                }
                if (loadBar.CurrentLoad == 100)
                {
                    StateManager.AddLoaded(GameRef.GamePlayScreen);

                }
            }


        }
        public override void Draw(GameTime gameTime)
        {
            
            //Make the screen transparent
            GameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.Identity);
            objManager.Draw(GameRef.spriteBatch);
            
            GameRef.spriteBatch.End();
            base.Draw(gameTime);


        }

        #endregion

        #region Title Screen Methods
        //when the startLable_Selected
        private void startLable_Selected(object sender, EventArgs e)
        {
             PictureBox s = (PictureBox)sender;
             s.btnDwn = true;
            this.Hide();
            StateManager.PushState(GameRef.MapScreen);
            s.btnDwn = false;
            
        }
        private void exit_selected(object sender, EventArgs e)
        {
            PictureBox s = (PictureBox)sender;
            s.btnDwn = true;
            this.Hide();
            GameRef.Exit();
            s.btnDwn = false;

        }
        void startLable_obj_Leave(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = false;
        }

        void startLable_obj_MouseOver(object sender, EventArgs e)
        {
            GameObject send = (GameObject)sender;
            send.MouseOver = true;
        }
        public void Loading()
        {
            while (loadBar.CurrentLoad != 100)
            {
                loadBar.Visable = true;
                StateManager.loadState(GameRef.GamePlayScreen);
            }
        }

        void pbSaveSlot1_obj_Selected(object sender, EventArgs e)
        {
            StateManager.PushState(GameRef.Achievements);

        }

        #endregion

        #region DataLoad
        public void loadPlayers()
        {
            reader = new StreamReader("PlayerData/Save.txt");
            
            while (!reader.EndOfStream)
            {
                string[] Spiltter = (reader.ReadLine()).Split('#');
                char[] charHolder;//coverts characters to be used as int

                #region switch menu items

                switch (Spiltter[0])
                {
                    case"Name":GameRef.player.Name = Spiltter[1];
                        break;
                    case "Gold"
                    : GameRef.player.gold = int.Parse(Spiltter[1]);
                        break;
                    case "Achievements"
                    : GameRef.player.achievementsCompleted = new int[Spiltter[1].ToArray().Length];
                        charHolder = Spiltter[1].ToArray();
                        for (int i = 0; i < Spiltter[1].ToArray().Length; i++)
                        {

                            GameRef.player.achievementsCompleted[i] = int.Parse(charHolder[i].ToString());
                        }
                        break;
                    case "Score": GameRef.player.Score = int.Parse(Spiltter[1]);
                        break;
                    case "Enemies":
                        GameRef.player.enemiesKilled = new int[Spiltter[1].ToArray().Length];
                        charHolder = Spiltter[1].ToArray();
                        for (int i = 0; i < Spiltter[1].ToArray().Length; i++)
                        {

                            GameRef.player.enemiesKilled[i] = int.Parse(charHolder[i].ToString());
                        }
                        break;
                    case "Challenges":
                        GameRef.player.challengesCompleted = new int[Spiltter[1].ToArray().Length];
                        charHolder = Spiltter[1].ToArray();
                        for (int i = 0; i < Spiltter[1].ToArray().Length; i++)
                        {


                            GameRef.player.challengesCompleted[i] = int.Parse(charHolder[i].ToString());
                        }
                        break;
                    case "TowersUnlocked":
                        GameRef.player.TowersUnlocked = new int[Spiltter[1].ToArray().Length];
                        charHolder = Spiltter[1].ToArray();
                        for (int i = 0; i < Spiltter[1].ToArray().Length; i++)
                        {


                            GameRef.player.TowersUnlocked[i] = int.Parse(charHolder[i].ToString());
                        }
                        break;

                #endregion

                }
            }
        }
        public void CreatePlayer()
        {
            string playerFile = "";
            writer = new StreamWriter("PlayerData/Save.txt", true);

            #region Player Stats

            player1 = new Player();


            player1.gold = 5000;
            writer.WriteLine("Gold#"+player1.gold.ToString());
            player1.achievementsCompleted = new int[10];
            writer.Write("Achivements#");

            for (int i = 0; i < player1.achievementsCompleted.GetLowerBound(0); i++)
            {
                //zero is default
                player1.achievementsCompleted[i] = 0;
                playerFile += player1.achievementsCompleted[i].ToString();
            }
            writer.WriteLine(playerFile);
            playerFile += "";
            player1.Score = 0;
            writer.WriteLine("Score#" + player1.Score.ToString());
           


            player1.enemiesKilled = new int[23];
            writer.Write("Enemies#");

            for (int i = 0; i < player1.enemiesKilled.Length; i++)
            {
                player1.enemiesKilled[i] = 0;
                playerFile += player1.enemiesKilled[i].ToString() ;
            }
            writer.WriteLine(playerFile);
            playerFile += "";
            player1.challengesCompleted = new int[16];

           writer.Write("Challenges#");
            for (int i = 0; i < player1.challengesCompleted.Length; i++)
            {
                player1.enemiesKilled[i] = 0;
                playerFile += player1.enemiesKilled[i].ToString() ;
            }
            writer.WriteLine(playerFile);
            player1.TowersUnlocked = new int[3];
            writer.Write("TowersUnlocked#");

            for (int i = 0; i < player1.TowersUnlocked.Length; i++)
            {
                player1.TowersUnlocked[i] = 0;
                playerFile += player1.TowersUnlocked[i].ToString();
            }
            playerFile += "";
            writer.WriteLine(playerFile);
            #endregion 
            writer.Close();

        }
        public void DeletePlayer()
        {

        }

        #endregion

    }
}

////comments

//#region Create an Account

////pbCreateAccount = new PictureBox(btnCreateAccount, new Rectangle((GameRef.ScreenRectangle.Width / 10) * 3, (GameRef.ScreenRectangle.Height / 16)*7, 400, 125), 4, 1, 0);
////pbCreateAccount.obj_Selected += new EventHandler(startLable_Selected);
////pbCreateAccount.obj_MouseOver += new EventHandler(startLable_obj_MouseOver);
////pbCreateAccount.obj_Leave += new EventHandler(startLable_obj_Leave);
////objManager.Add(pbCreateAccount);

//#endregion

//#region Login

////pbLogin = new PictureBox(btnLogin, new Rectangle((GameRef.ScreenRectangle.Width / 10) * 3, (GameRef.ScreenRectangle.Height / 16) * 10, 400, 125), 4, 1, 0);
////pbLogin.obj_Selected += new EventHandler(startLable_Selected);
////pbLogin.obj_MouseOver += new EventHandler(startLable_obj_MouseOver);
////pbLogin.obj_Leave += new EventHandler(startLable_obj_Leave);
////objManager.Add(pbLogin);

//#endregion

//#region Exit Game

////pbExit = new PictureBox(button, new Rectangle((GameRef.ScreenRectangle.Width / 2) - 100, (GameRef.ScreenRectangle.Height / 2) +100, 200, 100), 4, 1, 0);
////pbExit.obj_Selected += new EventHandler(startLable_Selected);
////pbExit.obj_MouseOver += new EventHandler(startLable_obj_MouseOver);
////pbExit.obj_Leave += new EventHandler(startLable_obj_Leave);
////objManager.Add(pbExit);

//#endregion
