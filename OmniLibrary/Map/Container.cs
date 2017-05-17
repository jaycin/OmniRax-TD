using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Map
{
    public class Container : GameObject
    {
        #region Fields And Regions
        Texture2D image;
        int selection;
        public Plot sender;
        bool btnDown;

        public bool BtnDown
        {
            get { return btnDown; }
            set { btnDown = value; }
        }
        public Plot Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        double time;
        
        string type;
        
        
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
        Rectangle destRectangle;

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
        enum Archer{idle =1,hovered =0,locked =11}
        enum Artillery{idle =4,hovered =3,locked =11}
        enum MageTower { idle = 10, hovered = 9, locked = 11 }
        enum Barracks { idle = 7, hovered = 6, locked = 11 }
        enum Wave { idle = 8, hovered = 2, locked = 5 } 
        
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
        public Container(Texture2D image,bool locked,string type ,int posX,int posY,Plot sender)
        {
            BtnDown = false;
            if (sender == null)
                SetPos(posX, posY);
            else
            {
                Sender = sender;
                Position = Sender.Position;

            }
            
            IsEnabled = locked;
            base.Type = type;
            Name = "Container";
            Visable = true;
            obj_MouseOver += new EventHandler(Container_obj_MouseOver);
            obj_Leave += new EventHandler(Container_obj_Leave);
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

        void Container_obj_Leave(object sender, EventArgs e)
        {
            HasFocus = false;
            MouseOver = false;
        }

        void Container_obj_MouseOver(object sender, EventArgs e)
        {
            HasFocus = true;
            MouseOver = true;
        }

        public override void Update(GameTime gameTime)
        {
             
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (Type == "FireTower")
            {
                if (HasFocus)
                {
                    selection = (int)MageTower.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)MageTower.idle;
                }
                if (!IsEnabled)
                {
                    selection = (int)MageTower.locked;
                }
               

            }
            if (Type == "IceTower")
            {
                if (HasFocus)
                {
                    selection = (int)Archer.hovered;
                }
                else if (!HasFocus)
                {
                    selection = (int)Archer.idle;
                }
                if (!IsEnabled)
                {
                    selection = (int)Archer.locked;
                }


            }


            Location = destRectangle;


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visable)
            {
                spriteBatch.Draw(image, destRectangle, sourceList[selection], Color.White);
            }
            
              

        }
        public void SetPos(int x , int y)
        {
            Position = new Vector2(x, y);
        }
        

    }
}
