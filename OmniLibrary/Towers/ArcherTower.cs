using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Map
{
    public class ArcherTower : GameObject
    {
        #region fields
        Texture2D image;
        int selection;
        List<Rectangle> sourceList;
        int rows;
        int columns;

        Rectangle sourceRect;
        Rectangle destRect;
        #endregion

        #region Properties
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public Rectangle DestRect
        {
            get { return destRect; }
            set { destRect = value; }
        }
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }
        
        #endregion

        #region Constructors
        public ArcherTower(Texture2D image, Rectangle destination, float scale)
        {
            selection = 0;
            rows = 2;
            columns = 5;
            Image = image;
            destRect = new Rectangle(destination.X,destination.Y,image.Width/columns,image.Height/rows);
            SourceRect = new Rectangle(0, 0, image.Width, image.Height);
            sourceList = new List<Rectangle>();
            Color = Color.White;
            Scale = scale;
            HasFocus = false;
            Size = new Vector2(image.Width, image.Height);
            base.Type = "ArcherTower";
            Position = new Vector2(destRect.X, destRect.Y);
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < Rows; x++)
                {
                    sourceList.Add(new Rectangle(destRect.Width * x, destRect.Height * y ,destRect.Width ,destRect.Height));
                }
            }
            base.Location = new Rectangle((int)Position.X,(int)Position.Y,destRect.Width,destRect.Height);
            
        }
        public ArcherTower(Texture2D image, Rectangle destination, Rectangle source, float scale)
        {
            Image = image;
            destRect = destination;
            SourceRect = source;
            Color = Color.White;
            Scale = scale;
            HasFocus = false;
            base.Type = "ArcherTower";
        }
        #endregion

        #region Abstract Method Region
        public override void Update(GameTime gameTime)
        {
            if (HasFocus)
            {
                selection = 1;
            }
            else
                selection = 0;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(image,DestRect, sourceList[selection], Color.White);
           
            
        }
        #endregion




    }
}
