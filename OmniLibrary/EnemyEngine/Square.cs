using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OmniLibrary;

namespace OmniLibrary.EnemyEngine
{
    class Square
    {
        #region Fields and encapsulation
        float endDistance;

        public float EndDistance
        {
            get { return endDistance; }
            set { endDistance = value; }
        }
        bool isWalkable;

        public bool IsWalkable
        {
            get { return isWalkable; }
            set { isWalkable = value; }
        }

        bool endPoint;

        public bool EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
        bool isPath;

        public bool IsPath
        {
            get { return isPath; }
            set { isPath = value; }
        }
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        Vector2 origin;

        Texture2D image;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        Rectangle location;
        int selection;

        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
        List<Rectangle> sourceRectange;

        public List<Rectangle> SourceRectange
        {
            get { return sourceRectange; }
            set { sourceRectange = value; }
        }

        public int Damage;
        
        #endregion
        #region Constructors
        public Square(Rectangle destRect, int id, bool walkable, bool isPath, bool isEnd,Texture2D squreTexture,int select)
        {
            Damage = 0;
            selection = select;
            Location = destRect;
            Origin = new Vector2(destRect.X , destRect.Y + destRect.Height / 2);
            Id = id;
            IsWalkable = walkable;
            IsPath = isPath;
            EndPoint = isEnd;
            SourceRectange = new List<Rectangle>();
            image = squreTexture;
            if (EndPoint == true || isWalkable == false)
            {
                EndDistance = 0;
                //ya dont say
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    SourceRectange.Add(new Rectangle(image.Width / 2 * j, image.Height / 2 * i, image.Width /2, image.Height /2));
                }
                
            }
            
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, location, sourceRectange[selection], Color.White);
        }
        public void Update(GameTime gameTime)
        {

        }
        #endregion
        #region Methods

        #endregion
    }
}
