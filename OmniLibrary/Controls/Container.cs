using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Controls
{
    public class Container : Control
    {
        #region Fields And Regions
        
        Texture2D image;
        int selection;
        double time;
        string type;
        Rectangle destRectangle;
        
        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        List<Rectangle> sourceList;

        public List<Rectangle> SourceList
        {
            get { return sourceList; }
            set { sourceList = value; }
        }

        
        public Rectangle DestRectangle
        {
            get { return destRectangle; }
            set { destRectangle = value; }
        }

        int rows;

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        int columns;

        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        } 
        #endregion
        enum Archer{idle =1,hovered =2,locked =11}
        enum Artillery{idle =4,hovered =3,locked =11}
        enum MageTower { idle = 10, hovered = 9, locked = 11 }
        enum Barracks { idle = 7, hovered = 6, locked = 11 }
        enum Wave { idle = 8, hovered = 3, locked = 5 } 
        
        /// <summary>
        /// Types
        /// =========
        /// MageTower
        /// Barracks
        /// ArcherTower
        /// Artilerty
        /// Wave
        /// =========
        /// </summary>
        /// <param name="image"></param>
        /// <param name="locked"></param>
        /// <param name="type"></param>
        public Container(Texture2D image,bool locked,string type ,int posX,int posY)
        {
            SetPos(posX, posY);
            
            IsLocked = locked;
            this.type = type;
            Name = "Container";
            Visible = true;
 

            Image = image;
            Rows = 4;
            Columns = 3;
            DestRectangle = new Rectangle((int)Position.X, (int)Position.Y, (image.Width / Columns)/6, (image.Height / Rows)/6);
            SourceList = new List<Rectangle>();

            
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    sourceList.Add(new Rectangle(image.Width / Columns * x, image.Height/rows * y, image.Width / Columns, image.Height / Rows));
                }
            }
            selection = 0;
            Size = new Vector2(DestRectangle.Width,DestRectangle.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if (DestRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                IsBtnDown = true;
            }
            else
                IsBtnDown = false;
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (type == "MageTower")
            {
                if (HasFocus)
                {
                    selection = (int)MageTower.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)MageTower.idle;
                }
                if (IsLocked)
                {
                    selection = (int)MageTower.locked;
                }
               

            }
            else if (type == "Barracks")
            {
                if (HasFocus)
                {
                    selection = (int)Barracks.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)Barracks.idle;
                }
                if (IsLocked)
                {
                    selection = (int)Barracks.locked;
                }
                else if (HasFocus && IsBtnDown && !IsLocked)
                {

                }

            }
            else if (type == "ArcherTower")
            {
                if (HasFocus)
                {
                    selection = (int)Archer.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)Archer.idle;
                }
                if (IsLocked)
                {
                    selection = (int)Archer.locked;
                }
              

            }
            else if (type == "Artilerty")
            {
                if (HasFocus)
                {
                    selection = (int)Artillery.hovered;
                }
                else if (!HasFocus)
                {
                    selection =(int)Artillery.idle;
                }
                if (IsLocked)
                {
                    selection = (int)Artillery.locked;
                }
           

            }
            else if (type == "Wave")
            {
                if (HasFocus)
                {
                    selection = (int)Wave.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)Wave.idle;
                    //need to get wave number and lenght here
                }
                 if (IsLocked)
                {
                    selection = (int)Wave.locked;
                }

            }

            Location = destRectangle;


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(image, destRectangle, sourceList[selection], Color.White);
            }
            
              

        }
        public override void HandleInput(PlayerIndex playerIndex)
        {

        }
        public void SetPos(int x , int y)
        {
            Position = new Vector2(x, y);
        }
        

    }
}
