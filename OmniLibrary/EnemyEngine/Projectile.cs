using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OmniLibrary;

namespace OmniLibrary.EnemyEngine
{
    public class Projectile : GameObject
    {
        MovementGrid Grid;
        aStar aStar;
        int damage;
        int imageFrame;
        int explosionFrame;
        int columns;
        int Frames;
        int current;
        int facing;
        bool reverse;
        double time;
        bool visable;
        float direction;
        double RotationTime;
        double RotationCounter;
        bool moving;
        public float Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        Vector2 EndPosition;

       

        int TargetId;
        int SquareTarget;
        int SquareId;
        int type;


        bool explosion;
        public bool Explosion
        {
            get { return explosion; }
            set { explosion = value; }
        }

        Rectangle destRect;
        Rectangle destExplosion;
        ObjectManager ObjMan;
        List<Rectangle> sourceList;
        List<Rectangle> explosionList;

        Texture2D image;
        Texture2D explosionImage;
        Queue<int> FirePath;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }


        
        public Projectile(Texture2D image, int frames, Texture2D explosionImage, int explosionRows, int explosionColumns,
            Vector2 start, int targetID, ObjectManager objman, MovementGrid grid,float speed,int damage,int typePro)
        {
            type = typePro;
            Grid = grid;
            aStar = new EnemyEngine.aStar(Grid);
            Position = start;
            moving = true;
            float scale = 0.8f;
            Speed = speed;
            Damage = damage;
            this.image = image;
            this.explosionImage = explosionImage;
            ObjMan = objman;
            Visable = true;
            Frames = frames;
            updateTarget(targetID);
            AnimationTime = 150;
            reverse = false;
            Position = start;
            destRect = new Rectangle ((int)Position.X,(int)Position.Y, (int)((image.Width / Frames)*scale),(int)((image.Height)*scale));
            Origin = new Vector2(destRect.X + destRect.Width / 2, destRect.Y + destRect.Height / 2);
            TargetId = targetID;
            sourceList = new List<Rectangle> ();
            explosionList = new List<Rectangle> ();
            RotationTime = 50;


            explosion = false;
            Type = "Projectile";
            facing = 0;
            imageFrame = 0;
            explosionFrame = 0;
            time = 0;
            current = 0;



            for (int i = 0; i < frames; i++)
            {
                
                    sourceList.Add(new Rectangle(image.Width/frames *i,0,image.Width/frames,image.Height));
                
            }
            if (type  == 0)
            {
                for (int i = 0; i < explosionColumns; i++)
                {
                    for (int j = 0; j < explosionRows; j++)
                    {
                        explosionList.Add(new Rectangle(explosionImage.Width / explosionColumns * i, explosionImage.Height / explosionRows * j,
                            explosionImage.Width / explosionColumns, explosionImage.Height / explosionRows));
                    }
                } 
            }
            else if(type == 1)
            {
                for (int j = 0; j < explosionRows; j++)
                {
                    for (int i = 0; i < explosionColumns; i++)
                    {
                        explosionList.Add(new Rectangle(explosionImage.Width / explosionColumns * i, explosionImage.Height / explosionRows * j,
                            explosionImage.Width / explosionColumns, explosionImage.Height / explosionRows));
                    }
                } 
            }
             
             

             
             SquareId = getId();
             
             EndPosition = Grid.SquareList[SquareTarget].Origin;
             //EndPosition = Grid.SquareList[FirePath.Peek()].Origin;
             
             
             Rotation = (float)Math.Atan2(EndPosition.Y - Position.Y, EndPosition.X - Position.X);
            RotOrigin = new Vector2(destRect.Width/2,destRect.Height/2);
             Velocity = new Vector2((float)Math.Cos(Rotation) * Speed, (float)Math.Sin(Rotation) * Speed);

        }

        public override void Update(GameTime gametime)
        {
            if (moving)
            {
                Move();
            }
            if (Explosion)
            {
                AnimationTime = 50;
            }
            Origin = new Vector2(destRect.X+ destRect.Height/2,destRect.Y+ destRect.Width/2);
            RotationCounter += gametime.ElapsedGameTime.TotalMilliseconds;
            Location = destRect;
            time += gametime.ElapsedGameTime.TotalMilliseconds;
            //AnimationTime += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
            
            


            if (AnimationTime < time)
            {
                if (explosion)
                {
                    
                    if (explosionFrame < explosionList.Count() - 1)
                    {

                        explosionFrame++;
                    }
                    else
                    {
                        explosion = false;
                        this.IsDead = true;
                    }
                   
                }
                else
                {
                   
                        SquareId = getId();
                        if (Grid.SourceList[SquareTarget].Intersects(destRect))
                        {

                            Explosion = true;
                            explosionFrame = 0;
                            destExplosion = new Rectangle(destRect.X - 100, destRect.Y - 100, destRect.Width + 150, destRect.Height + 150);
                            moving = false;
                            Location = destExplosion;
                            Rotation = (float)Math.Atan2(EndPosition.Y - Origin.Y, EndPosition.X - Origin.X);


                        }
                        else
                        {


                            Velocity = new Vector2((float)Math.Cos(Rotation) * Speed, (float)Math.Sin(Rotation) * Speed);

                        } 
                    
                    if (current < Frames - 1)
                    {
                        current++;
                    }
                    else
                    {
                        current = 0;
                    }
                   
                }

               

               

                    
                    time = 0;
                
                
               
              
            }
            if (RotationTime < RotationCounter)
            {

                Direction = (float)Math.Atan2(EndPosition.Y - Origin.Y, EndPosition.X - Origin.X);
                Velocity = new Vector2((float)Math.Cos(Direction) * Speed, (float)Math.Sin(Direction) * Speed);
                RotationCounter = 0;
            }
            
            //if (!fire())
            //{
            //    explosion = false;
            //}
            //else
            //{
            //    explosion = true;
            //}
            imageFrame = current;
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            
            if (explosion)
            {
                spritebatch.Draw(explosionImage, destExplosion, explosionList[explosionFrame], Color.White);
            }
            else
                spritebatch.Draw(image, destRect, sourceList[imageFrame], Color.White, Rotation, RotOrigin, SpriteEffects.None, 0);

        }

        public bool fire()
        {
            if (!destRect.Contains(new Point((int)EndPosition.X,(int)EndPosition.Y)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
          
      
        public int getId()
        {
            int Id = 0;
            foreach (Square s in Grid.SquareList)
            {
                if (s.Location.Contains(new Point((int)Origin.X, (int)Origin.Y)))
                {
                    if (s.Id < 1024)
                        Id = s.Id;
                    else
                    {
                    }
                }
            }
            return Id;
        }
        public void Move()
        {
            Rotation = (float)Math.Atan2(EndPosition.Y - Origin.Y, EndPosition.X - Origin.X);
            //Velocity = new Vector2((float)Math.Cos(Rotation) * Speed, (float)Math.Sin(Rotation) * Speed);
            Position += Velocity;
           
            destRect = new Rectangle((int)Position.X, (int)Position.Y, (int)destRect.Width, (int)destRect.Height);

            
        }
        public void checkDirection()
        {

        }
        public void updateTarget(int id)
        {
            SquareTarget = ObjMan.FindTarget(id);
        }
    }
}
