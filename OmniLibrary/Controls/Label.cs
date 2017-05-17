using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OmniLibrary.Controls
{
    public class Label : GameObject
    {
        #region Constructor Region
        string text;
        public Color lblColor;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public Label(string text,SpriteFont spriteFont)
        {
            HasFocus = false;
            this.text = text;
            SpriteFont = spriteFont;
            lblColor = Color.DarkGoldenrod;
        }
        #endregion

        #region Abstract Methods
        public override void Update(GameTime gameTime)
        {
            if (MouseOver)
            {
                Color = Color.White;
            }
            else
            {
                Color = Color.Red;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HasFocus)
            {
                spriteBatch.DrawString(SpriteFont, Text, Position,lblColor);
            }
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, lblColor);
        }
        #endregion
    }
}