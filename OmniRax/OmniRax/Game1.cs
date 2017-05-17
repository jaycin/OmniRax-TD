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
using OmniLibrary;
using OmniLibrary.Structs;

namespace OmniRax
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //variables used throughout the game
        #region Variables
        Rectangle drawRectangle;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        GameStateManager stateManager;
        public Player player;
        //mouse pointer used thoughout the game
        private Texture2D mousePointer;

        public Texture2D MousePointer
        {
            get { return mousePointer; }
            set { mousePointer = value; }
        }
        

        //Game screens used for the game
        public Game_Screens.LoginScreen TitleScreen;
        public Game_Screens.StartMenu StartMenuScreen;
        public Game_Screens.GamePlayScreen GamePlayScreen;
        public Game_Screens.MapScreen MapScreen;

        public Game_Screens.PlayerStatistics Achievements;
        
        
        #endregion

        #region Screen Field Region
        //default screen position
        public static Vector2 baseScreenSize;
        
        //mouse position and state of the mouse, for moving through options and
        //registering onclick events
        private Vector2 mousePosition;

        public Vector2 MousePosition
        {
            get { return mousePosition; }
            set { mousePosition = value; }
        }

        private MouseState mouseState;

        public MouseState MouseState
        {
            get { return mouseState; }
            set { mouseState = value; }
        }
        //test resolution
        static int screenWidth = 1024;
        static int screenHeight = 768;

        //The base screen for all screens, fits the size of the resolution given
        public readonly Rectangle ScreenRectangle;
        #endregion

        public Game1()
        {
            //load graphics according to the device manager and setting
            //the resolution
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            graphics.ApplyChanges();
            player = new Player();
            //screen size developed at
            baseScreenSize = new Vector2(screenWidth,screenHeight);
            //the screen size it is scaled to
            ScreenRectangle = new Rectangle(0, 0,screenWidth, screenHeight);
             
            //base directory where all the content is stored, content is a reference
            //content pipeline - content loaded
            Content.RootDirectory = "Content";
            //adds the input handler component, keeps track of what is inputted
            Components.Add(new Input_Handler(this));
            //keeps track of the states - the different pages
            stateManager = new GameStateManager(this);

            //game screens used for the game
            TitleScreen = new Game_Screens.LoginScreen(this, stateManager);
            StartMenuScreen = new Game_Screens.StartMenu(this, stateManager);
            GamePlayScreen = new Game_Screens.GamePlayScreen(this,stateManager);
            MapScreen = new Game_Screens.MapScreen(this,stateManager);

            Achievements = new Game_Screens.PlayerStatistics(this,stateManager);

            //intial state starts at title screen
            stateManager.ChangeState(TitleScreen);
            
        }


        protected override void Initialize()
        {
            
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //creates the instance of the spritebatch(used to draw our animations and images) 
            //and used throughout the game
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //the texture for the mouse pointer for the game screens
            MousePointer = Content.Load<Texture2D>(@"UI Content\Pointer");
            drawRectangle = new Rectangle(0, 0, 32, 32);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

  
        protected override void Update(GameTime gameTime)
        {
            //xbox controller, if the back button on controller is pressed - exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //logic for actions done with mouse
            UpdateMouse();
            drawRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 32, 32);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            //base
            GraphicsDevice.Clear(Color.Black);

            //draw at gametime (60fps - frames per second)
            base.Draw(gameTime);

            spriteBatch.Begin();
            //draws the mouse pointer
            //spriteBatch.Draw(this.MousePointer, this.mousePosition, Color.White);
            spriteBatch.Draw(mousePointer, drawRectangle, Color.White);
            spriteBatch.End();
        }
        // scaling with different types of resolutions
        public float scaleRatio()
        {
            float ratioX = screenWidth / (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float ratioY = screenHeight / (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            return ratioX + ratioY  /2;

        }

        //Mouse movements and click events
        private void UpdateMouse()
        {
            //keeps mouse within game state
            Mouse.WindowHandle = Window.Handle;

            this.mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
            
        }

       
    }
}
