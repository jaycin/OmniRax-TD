using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Map
{
    public class RuneEffect : GameObject
    {

        #region fields

        Texture2D image;
        List<Rectangle> imageSource;
        public int state;
        public int Facing;
        int frame;
        Rectangle sourceRect;
        Rectangle destRect;

        int currentFrame;




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
        public RuneEffect(Texture2D image, Vector2 position, int imageRows, int imageCollumns, float scale, int State, int faceing)
        {
            Facing = faceing;
            state = State;
            state = 0;
            frame = 0;
            Image = image;
            Position = position;
            SourceRect = new Rectangle(0, 0, image.Width / imageCollumns, image.Height / imageRows);
            Color = Color.White;
            Scale = scale;
            imageSource = new List<Rectangle>();
            Location = new Rectangle((int)position.X, (int)position.Y, SourceRect.Width, SourceRect.Height);
            DestRect = new Rectangle((int)Position.X, (int)Position.Y, image.Width / imageCollumns, image.Height / imageRows);

            for (int i = 0; i < imageRows; i++)
            {
                for (int x = 0; x < imageCollumns; x++)
                {
                    imageSource.Add(new Rectangle(Image.Width / imageCollumns * x, Image.Height / imageRows * i, Image.Width / imageCollumns, Image.Height / imageRows));
                }
            }
            obj_Leave += new EventHandler(GraveStone_MouseLeave);
            obj_MouseOver += new EventHandler(GraveStone_MouseOver);
            obj_Selected += new EventHandler(GraveStone_Selected);
            HasFocus = false;
            Size = new Vector2(image.Width, image.Height);
            base.Type = "GraveStone";
            currentFrame = 0;
            Time = 0;
            AnimationTime = 150;
        }

        void GraveStone_Selected(object sender, EventArgs e)
        {

        }

        void GraveStone_MouseOver(object sender, EventArgs e)
        {
            HasFocus = true;
            MouseOver = true;
        }

        void GraveStone_MouseLeave(object sender, EventArgs e)
        {
            HasFocus = false;
            MouseOver = false;
        }
        #endregion

        #region Abstract Method Region
        public override void Update(GameTime gameTime)
        {

            Time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Time > AnimationTime)
            {
               if (state == 1)
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
                    else if (frame < 16)
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
                    currentFrame = frame + Facing * 24;
                }
                Time = 0;
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable)
            {

                spriteBatch.Draw(image, destRect, imageSource[currentFrame], Color);
            }
           

        }
        #endregion
    }
}
