using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OmniLibrary.Towers;


namespace OmniLibrary.Map
{
    public class MageTower : GameObject
    {
        #region fields
        public int Cost;
        int level;
        double lingerDamage;
        bool IsFireDamage;
        public int shots;
        float shotTime;
        public int Damage;
        Texture2D image;
        PrimitiveLine line;
        List<Rectangle> imageSource;
        int iSelected = 0;
        double coolDown;
        int fireTime;
        public Vector2 FirePoint;
        public int FireTime
        {
            get { return fireTime; }
            set { fireTime = value; }
        }
        
        bool developmentMode;
        bool btnDown;
        public double CoolDown
        {
            get { return coolDown; }
            set { coolDown = value; }
        }
        float time = 0;
        int Range;
        bool isRun;
        public bool isIce;
        Rectangle sourceRect;
        Rectangle destRect;
        Texture2D CircleImage;
        public Rectangle CircleRectangle;
        List<Rectangle> CircleSource;
        #endregion

        #region Properties
        int circleSelection;
        
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

        int BuildMin = 0,
            BuildMax = 3,
            SelectedMin = 4,
            SelectedMax = 23,
            IdleMin = 0,
            IdleMax = 41;

        #endregion

        #region Constructors
        public MageTower(Texture2D image,int imageRows,int imageCollumns, float scale,Vector2 position,Texture2D sircle,bool IsIce)
        {
            level = 1;
            isIce = IsIce;
            IsElementCheck();
            IsTower = true;
            Image = image;
            isRun = false;
            Color = Color.White;
            Scale = scale;
            obj_MouseOver += new EventHandler(MageTower_obj_MouseOver);
            obj_Leave += new EventHandler(MageTower_obj_Leave);
            
            if (IsIce)
            {
                Position = new Vector2(position.X - 153, position.Y - 107);
                circleSelection = 2;
                CoolDown = 5000;
                fireTime = 5000;
            }
            else
            {
                Position = new Vector2(position.X - 50, position.Y - 70);
                circleSelection = 1;
                CoolDown = 4000;
                fireTime = 4000;
            }
            AnimationTime = 150;
            CircleImage = sircle;
            Range = 500;
            
            imageSource = new List<Rectangle>();
            CircleSource = new List<Rectangle>();

            if (CircleImage!= null)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        CircleSource.Add(new Rectangle(CircleImage.Width / 2 * j, CircleImage.Height / 2 * i, CircleImage.Width / 2, CircleImage.Height / 2));
                    }
                } 
            }
            DestRect = new Rectangle((int)Position.X, (int)Position.Y, (int)((image.Width / imageCollumns)), (int)((image.Height / imageRows)));
            if (IsIce)
                Location = new Rectangle((int)Position.X+destRect.Width/4, (int)Position.Y+destRect.Height/4, destRect.Width-destRect.Width/2, destRect.Height-destRect.Height/2);
            else
                Location = new Rectangle((int)Position.X, (int)Position.Y, destRect.Width, destRect.Height);
            for (int i = 0; i < imageRows; i++)
            {
                for (int x = 0; x < imageCollumns; x++)
                {
                    imageSource.Add(new Rectangle(image.Width / imageCollumns * x, image.Height / imageRows * i, image.Width/imageCollumns,image.Height/imageRows));
                }
            }
            Origin = new Vector2(DestRect.X + DestRect.Height / 2, DestRect.Y + DestRect.Width / 2);
            if (isIce)
            {
                FirePoint = new Vector2(Origin.X + 50, Origin.Y - 50);
                shots = 10;
            }
            else
            {
                FirePoint = new Vector2(Origin.X, Origin.Y - 10);
                shots = 5;
            }
            if (isIce)
            {
                CircleRectangle = new Rectangle(DestRect.X - 140, DestRect.Y - 140, DestRect.Width + 250, DestRect.Height + 250);
            }      
            else
            {
                CircleRectangle = new Rectangle(DestRect.X - 125, DestRect.Y - 125, DestRect.Width + 250, DestRect.Height + 250);
            }
            RotOrigin = new Vector2(CircleRectangle.Width/2,CircleRectangle.Height/2);
            Rotation = - 2;
            HasFocus = false;
            Size = new Vector2(image.Width, image.Height);
            base.Type = "MageTower";
            
            
  
        }

        public void IsElementCheck()
        {
            if (isIce)
            {
                if (level == 1)
                {
                    Cost = 200;
                    Damage = 50;
                    lingerDamage = 5;
                    IsFireDamage = false;
                }
                else if (level == 2)
                {
                    Cost = 400;
                    Damage = 100;
                    lingerDamage = 25;
                    IsFireDamage = false;
                }
                else if (level == 3)
                {
                    Cost = 500;
                    Damage = 150;
                    lingerDamage = 50;
                    IsFireDamage = false;
                }
            }

            else if (!isIce)
            {
                if (level == 1)
                {
                    Cost = 100;
                    Damage = 40;
                    lingerDamage = 15;
                    IsFireDamage = false;
                }
                else if (level == 2)
                {
                    Cost = 200;
                    Damage = 80;
                    lingerDamage = 30;
                    IsFireDamage = false;
                }
                else if (level == 3)
                {
                    Cost = 350;
                    Damage = 120;
                    lingerDamage = 60;
                    IsFireDamage = false;
                }
            }
        }

        void MageTower_obj_Leave(object sender, EventArgs e)
        {
            HasFocus = false;
            MouseOver = false;
        }

        void MageTower_obj_MouseOver(object sender, EventArgs e)
        {
            HasFocus = true;
            MouseOver = true;
        }

        #region Abstract Method Region
        public override void Update(GameTime gameTime)
        {
            if (shots == 0)
            {
                Color = Color.Gray;
                shotTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                Color = Color.White;
            }
            if (Rotation < 2)
                Rotation += 0.1f;
            else
            {
                Rotation = -2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F10) && !btnDown)
            {
                btnDown = true;
                if (!developmentMode)
                    developmentMode = true;
                else
                    developmentMode = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F10) && btnDown)
            {
                btnDown = false;
            }
            
                
            //animation for tower
            if (!developmentMode)
            {

                time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (time > AnimationTime)
                {
                   
                    if (CoolDown != 0)
                    {
                        CoolDown--;
                    }
                    

                    if (time > AnimationTime)
                    {
                        if (iSelected < imageSource.Count - 1)
                        {
                            iSelected++;
                            Origin = new Vector2(DestRect.X + DestRect.Height / 2, DestRect.Y + DestRect.Width / 2);
                        }
                        else
                            iSelected = 0;

                    }
                    time = 0;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down)&&!btnDown)
                {
                    btnDown = true;
                    if (iSelected < imageSource.Count - 1)
                    {
                        iSelected++;
                    }
                    else
                        iSelected = 0;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Down) && btnDown)
                {
                    btnDown = false;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !btnDown)
                {
                    this.OnFire(null);
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released && btnDown)
                {
                    btnDown = false;
                }
            }
            if (CoolDown < FireTime)
            {
                coolDown += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (shotTime > 5000)
            {
                if (isIce)
                {
                    
                    shots = 10;
                }
                else
                {
                   
                    shots = 5;
                }
                shotTime = 0;
                Color = Color.White;
            }

       }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HasFocus && CircleImage != null)
            {

  
                spriteBatch.Draw(CircleImage, CircleRectangle, CircleSource[circleSelection], Color.White);



            }
 
               spriteBatch.Draw(image, destRect, imageSource[iSelected], Color);
            
            
        }

        #endregion


        #endregion
    }
}
