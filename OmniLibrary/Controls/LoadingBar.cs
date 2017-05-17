using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Controls
{
    public class LoadingBar : GameObject
    {
        int currentLoad;

        public int CurrentLoad
        {
            get { return currentLoad; }

            set { 

                currentLoad = value;
                loadChange(this, null);
                }
        }
        int maxLoad;
        
        List<Rectangle> sourcelist;
        Rectangle baseRect;
        Rectangle destRect;
        Texture2D Image;
        int Selection;
        public event EventHandler loadChange;
        public LoadingBar(Texture2D image, int rows, int collums,Vector2 position,int selection)
        {
            Selection = selection;
            loadChange +=new EventHandler(LoadingBar_loadChange);
            sourcelist = new List<Rectangle>();
            maxLoad = 100;
            Image = image;
            Position = position;
            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < collums; x++)
                {
                    sourcelist.Add(new Rectangle(image.Width * x, image.Height/rows * i, image.Width / collums, image.Height / rows));
                }
            }
            baseRect = new Rectangle((int)Position.X, (int)Position.Y, image.Width, image.Height / 3);
            destRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width / 100 * currentLoad, Image.Height / 3);
        }
        public override void Update(GameTime gameTime)
        {
            
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            if (Visable)
            {
                spriteBatch.Draw(Image, baseRect, sourcelist[0], Color.White);
                spriteBatch.Draw(Image, destRect, sourcelist[2], Color.White);
            }
            //spriteBatch.End();
        }
        public void setload(int load)
        {
            currentLoad = load;
        }
        public void LoadingBar_loadChange(object sender, EventArgs e)
        {
            sourcelist[2] = new Rectangle(0, Image.Height / 3 * 2, Image.Width / 100 * currentLoad, Image.Height / 3);
            destRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width / 100 * currentLoad, Image.Height / 3);
        }
    }
}
