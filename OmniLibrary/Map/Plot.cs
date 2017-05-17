using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Map
{
    public class Plot : GameObject
    {

        #region fields
        public int PlotId;
        Texture2D image;
        List<Rectangle> imageSource;
        public int state;
        public int Facing;
        int frame;
        Rectangle sourceRect;
        Rectangle destRect;
        
        int currentFrame;
        bool hasTower;
        MageTower tower;

        public MageTower Tower
        {
            get { return tower; }
            set { tower = value; }
        }
        Barracks barracks;

        public Barracks Barracks
        {
            get { return barracks; }
            set { barracks = value; }
        }
        ArcherTower archerTower;

        public ArcherTower ArcherTower
        {
            get { return archerTower; }
            set { archerTower = value; }
        }
        public bool HasTower
        {
            get { return hasTower; }
            set { hasTower = value; }
        }

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

        #endregion

        #region Constructors
        public Plot(Texture2D image,Vector2 position,int imageRows,int imageCollumns, float scale,int State,int faceing,int Id)
        {
            PlotId = Id;
            Facing = faceing;
            state = State;
            state = 0;
            frame = 0;
            Image = image;
            Position = position;
            SourceRect = new Rectangle(0, 0, image.Width/imageCollumns, image.Height/imageRows);
            Color = Color.White;
            Scale = scale;
            imageSource = new List<Rectangle>();
            Location = new Rectangle((int)position.X, (int)position.Y, SourceRect.Width, SourceRect.Height);
            DestRect = new Rectangle((int)Position.X, (int)Position.Y, image.Width / imageCollumns, image.Height / imageRows);

            for (int i = 0; i < imageRows; i++)
            {
                for (int x = 0; x < imageCollumns; x++)
                {
                    imageSource.Add(new Rectangle(Image.Width / imageCollumns * x, Image.Height / imageRows * i, Image.Width/imageCollumns, Image.Height/imageRows));
                }
            }
            obj_Leave += new EventHandler(Plot_obj_Leave);
            obj_MouseOver += new EventHandler(Plot_obj_MouseOver);
            obj_Selected += new EventHandler(Plot_obj_Selected);
            HasFocus = false;
            Size = new Vector2(image.Width, image.Height);
            base.Type = "Plot";
            currentFrame = 0;
            Time = 0;
            AnimationTime = 150;
        }

        void Plot_obj_Selected(object sender, EventArgs e)
        {
            
        }

        void Plot_obj_MouseOver(object sender, EventArgs e)
        {
            HasFocus = true;
            MouseOver = true;
        }

        void Plot_obj_Leave(object sender, EventArgs e)
        {
            HasFocus = false;
            MouseOver = false;
        }
        public Plot(Texture2D image, Rectangle destination, Rectangle source, float scale)
        {
           
            IsEnabled = true;
            Image = image;
            destRect = destination;
            Location = destination;
            SourceRect = source;
            imageSource = new List<Rectangle>();
            imageSource.Add(source);
            obj_Leave += new EventHandler(Plot_obj_Leave);
            obj_MouseOver += new EventHandler(Plot_obj_MouseOver);
            Color = Color.White;
            Scale = scale;
            Size = new Vector2(image.Width, image.Height);
            HasFocus = false;
            base.Type = "Plot";
            currentFrame = 0;
            Time = 0;
        }
        #endregion

        #region Abstract Method Region
        public override void Update(GameTime gameTime)
        {

            Time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Time > AnimationTime)
            {
            if (state == 0)
            {
                
                    if (HasFocus)
                    {
                        if (currentFrame < imageSource.Count - 1)
                        {
                            currentFrame++;

                        }
                        if (currentFrame == imageSource.Count - 1)
                        {
                            currentFrame = 1;
                        }
                    }
                    else
                    {
                        currentFrame = 0;
                    }
                    
                
            }
            else if (state == 1)
            {
                if (frame >= 8)
                {
                    frame = 0;
                }
                else if (frame < 8)
                {
                    frame++;
                }
                currentFrame = frame + Facing * 24;
            }
            else if (state == 2)
            {
                if (frame < 9)
                {
                    frame = 9;
                }
                else if(frame< 16)
                {
                    frame++;
                }
                if (frame == 16)
                {
                    state = 0;
                }
                currentFrame = frame + Facing * 24;
            }
            else if (state == 3)
            {
                if (frame < 16)
                {
                    frame = 16;
                }
                else if (frame < 23)
                {
                    frame++;
                }
                if (frame == 24)
                {
                    this.IsDead = true;
                }
                currentFrame = frame + Facing*24;
            }
            Time = 0;
            }
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (imageSource.Count >1)
            {
                
                    spriteBatch.Draw(image, destRect, imageSource[currentFrame], Color); 
            }
            else
                spriteBatch.Draw(image, destRect, imageSource[0], Color);

        }
        #endregion
    }
}
