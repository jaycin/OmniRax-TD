using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OmniLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OmniLibrary.Controls;

namespace OmniRax.Game_Screens
{
    public abstract partial class BaseGameState : GameState
    {

        #region Fields Region
        protected Game1 GameRef;

        #region Login SpriteFonts

        protected SpriteFont labelTitle;
        //protected SpriteFont labelRecommendation;
        protected SpriteFont labelFooters;

        #endregion

        protected SpriteFont menuFont;
        protected PlayerIndex playerIndexControl;
        public LoadingBar Loadbar;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public BaseGameState(Game game, GameStateManager manager)
            :base(game,manager)
        {
            GameRef = (Game1)game;

            playerIndexControl = PlayerIndex.One;
        }
        #endregion

        #region XNA
        protected override void LoadContent()
        {


            base.LoadContent();
            
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);  
        }
        public override void Draw(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion
       
    }
}
