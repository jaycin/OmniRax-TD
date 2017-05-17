using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary
{
    public class Camera
    {
        #region Fields
        Vector2 position;
        Rectangle viewpointRectangle;
        float speed;
        float zoom;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }
        public float Speed
        {
            get { return speed; }
            private set { speed = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        #endregion

        #region Constructors
        public Camera(Rectangle viewPointRect)
        {
            speed = 4f;
            zoom = 1f;
            viewpointRectangle = viewPointRect;
        }
        public Camera(Rectangle viewPointRect, Vector2 position)
        {
            speed = 4f;
            zoom = 1f;
            viewpointRectangle = viewPointRect;
            Position = position;

        }
   
        #endregion
        #region Method Region

        public void Update(GameTime gameTime)
        {
            if (Input_Handler.KeyDown(Keys.Left))
                position.X -= speed;
            else if (Input_Handler.KeyDown(Keys.Right))
                position.X += speed;

            if (Input_Handler.KeyDown(Keys.Up))
                position.Y -= speed;
            else if(Input_Handler.KeyDown(Keys.Down))
                position.Y += speed;
            LockCamera();
        }
        private void LockCamera()
        {
            //position.X = MathHelper.Clamp(position.X, 0);
            //position.Y = MathHelper.Clamp(position.Y, 0 );
        }
        #endregion
    }
}
