using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Controls
{
    public class LinkLabel : GameObject
    {
        #region Fields and Properties
        Color selectedColor = Color.Red;
        Texture2D ButtonImage;
        int width;
        int height;

        string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }



        List<Rectangle> destRectangle;

        bool isImage;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Texture2D ButtonImage1
        {
            get { return ButtonImage; }
            set { ButtonImage = value; }
        }
        Rectangle destRect;
        int currentFrame;

        List<Rectangle> sourceList;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }
        #endregion

        #region Constructor Region
        public LinkLabel(string text,Vector2 pos)
        {
            Position = pos;
            Location = new Rectangle((int)Position.X,(int)Position.Y,(int)SpriteFont.MeasureString(text).X,(int)SpriteFont.MeasureString(text).Y);
            isImage = false;
            HasFocus = false;
            Text = text;
            IsEnabled = true;

        }
        public LinkLabel(int NumFrames, Texture2D btnImage, Vector2 pos)
        {
            IsEnabled = true;
            Position = pos;
            isImage = true;
            
            currentFrame = 0;
            HasFocus = false;
            sourceList = new List<Rectangle>();
            ButtonImage = btnImage;
            destRect = new Rectangle((int)Position.X, (int)Position.Y, ButtonImage.Width - 50, ButtonImage.Height / 6);
            Width = destRect.Width;
            Height = destRect.Height;
            for (int i = 0; i <= NumFrames; i++)
            {
                sourceList.Add(new Rectangle(0, ButtonImage.Height / 4 * i, ButtonImage.Width, ButtonImage.Height / 4));
            }
            Size = new Vector2((int)destRect.Width, (int)destRect.Height);
            Location = destRect;
        }
        #endregion

        #region Abstract Methods
        public override void Update(GameTime gameTime)
        {
            if (isImage)
            {
                destRect = new Rectangle((int)Position.X, (int)Position.Y, ButtonImage.Width, ButtonImage.Height / 4);

                if (HasFocus && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    currentFrame = 1;

                }
                else if (MouseOver)
                {
                    currentFrame = 2;
                }
                if (!MouseOver)
                {
                    currentFrame = 0;
                }
                if (IsLocked)
                {
                    currentFrame = 3;
                }
            }

           

        }
        public override void Draw(SpriteBatch spriteBatch)
        {


            if (isImage)
            {
                //if (Mouse.GetState().LeftButton == ButtonState.Pressed && HasFocus)
                //{
                spriteBatch.Draw(ButtonImage, destRect, sourceList[currentFrame], Color.White);
                //}
                //if (HasFocus)
                //{
                //    spriteBatch.Draw(ButtonImage, destRect, sourceList[1], Color.White);
                //}
                //if (!HasFocus)
                //{
                //    spriteBatch.Draw(ButtonImage, destRect, sourceList[2], Color.White);
                //}
                //if (IsLocked)
                //{
                //    spriteBatch.Draw(ButtonImage, destRect, sourceList[3], Color.White);
                //}
            }

            else
            {
                spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
            }





        }
    }
        #endregion
}
