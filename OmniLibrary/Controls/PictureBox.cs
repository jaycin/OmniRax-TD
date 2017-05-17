using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using OmniLibrary;

namespace OmniLibrary.Controls
{
    public class PictureBox : GameObject
    {

        #region Fields
        Texture2D image;
        List<Rectangle> sourceRect;

        int Frame;
        Rectangle destRect;
        public bool btnDwn;
        #endregion

        #region Properties
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public List<Rectangle> SourceRectangle
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public Rectangle DestinationRectangle
        {
            get { return destRect; }
            set { destRect = value; }
        }
        #endregion

        #region Constructors
        public PictureBox(Texture2D image, Rectangle destination, float scale)
        {

            Image = image;
            DestinationRectangle = destination;
            sourceRect = new List<Rectangle>();
            SourceRectangle.Add(new Rectangle(0, 0, image.Width, image.Height));
            Color = Color.White;
            Scale = scale;
        }
        public PictureBox(Texture2D image, Rectangle destination, int rows, int collunms, float scale)
        {
            btnDwn = false;
            IsEnabled = true;
            Visable = true;
            Image = image;
            DestinationRectangle = destination;
            Location = destination;
            Size = new Vector2(destination.Width, destination.Height);
            sourceRect = new List<Rectangle>();
            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < collunms; x++)
                {
                    sourceRect.Add(new Rectangle(image.Width / collunms * x, image.Height / rows * i, image.Width / collunms, image.Height / rows));
                }
            }
            Color = Color.White;
            Scale = scale;
            Frame = 0;
            Position = new Vector2(destination.X, destination.Y);
            SetPosition(Position);
        }

        #endregion

        #region Abstract Method Region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            
            if(sourceRect.Count() > 1)
            {
                if (MouseOver)
                {
                    Frame = 1;
                }
                else if (!MouseOver)
                    Frame = 0;
                else if (btnDwn)
                {
                    Frame = 2;
                }
            }
            
            
            Position = new Vector2(destRect.X, destRect.Y);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (Visable)
                {
                    spriteBatch.Draw(Image, DestinationRectangle, sourceRect[Frame], Color);
                }
            }
            catch (ObjectDisposedException)
            {
                //dont really know why this is firing 
            }
        }

        #endregion

        #region Picture Box Methods
        public void SetPosition(Vector2 newPosition)
        {
            destRect = new Rectangle((int)newPosition.X, (int)newPosition.Y, destRect.Width, destRect.Height);
        }
        /// <summary>
        ///ok , negative x = left.
        ///Positve x = Right;
        ///negative Y = Up
        ///Positve y = Down
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Move(int x, int y)
        {
            destRect = new Rectangle((int)destRect.X + x, (int)destRect.Y + y, destRect.Width, destRect.Height);


        }
        #endregion
    }
}
