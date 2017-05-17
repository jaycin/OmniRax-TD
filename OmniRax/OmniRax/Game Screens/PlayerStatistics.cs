using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

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
    public class PlayerStatistics:BaseGameState
    {

        Texture2D btnAchievementIcon;
        
        Texture2D btnBackgroundImage;

        PictureBox pbBackground;
        PictureBox pbAchievements;

        ObjectManager objManager;
        ContentManager content;

        public PlayerStatistics(Game game, GameStateManager manager) 
            : base(game, manager) 
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            objManager = new ObjectManager(base.menuFont, null, null);
            ContentManager content = GameRef.Content;
            
            base.LoadContent();

            btnBackgroundImage = content.Load<Texture2D>(@"UI Content\Login\LoginPicture");

            btnAchievementIcon = content.Load<Texture2D>(@"UI Content\Achievements\AchievementIcon");

            pbAchievements = new PictureBox(btnAchievementIcon,new Rectangle(GameRef.ScreenRectangle.Width /2, GameRef.ScreenRectangle.Height/2, 150,150), 1,1,0);
            pbAchievements = new PictureBox(btnBackgroundImage, GameRef.ScreenRectangle, 0);
            objManager.AddLst.Add(pbAchievements);
            objManager.Add(pbAchievements);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            objManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameRef.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            objManager.Draw(GameRef.spriteBatch);
            GameRef.spriteBatch.End();




        }
    }
}
