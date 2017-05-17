using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace OmniLibrary.Map
{
    public class MapLandscape : GameObject
    {
        #region Fields And Properties
        Texture2D image;
        Texture2D OverLayImage;
        Rectangle destRect;
        Rectangle sourceRect;
        Vector2[] spawnPoints;
        Queue<Vector2>[] wayPoints;

        public Queue<Vector2>[] WayPoints
        {
            get { return wayPoints; }
            set { wayPoints = value; }
        }

        public Vector2[] SpawnPoints
        {
            get { return spawnPoints; }
            set { spawnPoints = value; }
        }

        float scale;

        public Rectangle DestRect
        {
            get { return destRect; }
            set { destRect = value; }
        }
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        } 
        #endregion

        #region Constructors
        public MapLandscape(Texture2D image, Rectangle Destination, float scale,Vector2[] PointsSpawn , Queue<Vector2>[]wayPoints)
        {
            IsMap = true;
            SpawnPoints = PointsSpawn;
            WayPoints = wayPoints;
            Image = image;
            DestRect = Destination;
            sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            Scale = scale;
        }
        public MapLandscape(Texture2D image,Texture2D overLayImage, Rectangle Destination, float scale, Vector2[] PointsSpawn, Queue<Vector2>[] wayPoints)
        {
            IsMap = true;
            SpawnPoints = PointsSpawn;
            WayPoints = wayPoints;
            Image = image;
            OverLayImage = overLayImage;
            DestRect = Destination;
            sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            Scale = scale;
        }
        #endregion

        #region Xna
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, destRect, sourceRect, Color);
            //if (OverLayImage != null)
            //{
            //    spritebatch.Draw(OverLayImage, destRect, sourceRect, Color);
            //}
        }

        #endregion


    }
}
