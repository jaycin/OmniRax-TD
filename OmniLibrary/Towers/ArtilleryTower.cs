using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Map
{
    public class ArtilleryTower : GameObject
    {
        #region fields
        Texture2D image;
        Texture2D selectedImage;
        
        Rectangle sourceRect;
        Rectangle destRect;
        #endregion

        #region Properties
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public Texture2D SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; }
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
        public ArtilleryTower(Texture2D image,Texture2D selectedImage, Rectangle destination, float scale)
        {
            Image = image;
            destRect = destination;
            SourceRect = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
            Scale = scale;
            SelectedImage = selectedImage;
            HasFocus = false;
            Size = new Vector2(image.Width, image.Height);
            base.Type = "ArtilleryTower";
            Position = new Vector2(destRect.X, destRect.Y);

        }
        public ArtilleryTower(Texture2D image, Texture2D selectedImage, Rectangle destination, Rectangle source, float scale)
        {
            Image = image;
            destRect = destination;
            SourceRect = source;
            Color = Color.White;
            Scale = scale;
            SelectedImage = selectedImage;
            HasFocus = false;
            base.Type = "ArtilleryTower";
        }
        #endregion

        #region Abstract Method Region
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!HasFocus)
            spriteBatch.Draw(image,destRect, sourceRect, Color);
            else
            spriteBatch.Draw(selectedImage, destRect, sourceRect, Color);
            
        }

        #endregion




    }
}
