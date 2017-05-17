using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.EnemyEngine
{
    public class HeathBar: GameObject
    {
        int health;

        public int Health
        {
            get { return Health; }
            set
            {

                health = value;
                loadChange(this, null);
            }
        }
        int maxHealth;

        public int MaxHealth
        {
          get { return maxHealth; }
          set { maxHealth = value; }
        }
        List<Rectangle> sourcelist;
        Rectangle baseRect;

        public Rectangle BaseRect
        {
            get { return baseRect; }
            set { baseRect = value; }
        }
        Rectangle destRect;

        public Rectangle DestRect
        {
            get { return destRect; }
            set { destRect = value; }
        }
        Texture2D Image;
        public event EventHandler loadChange;
        public HeathBar(Texture2D image, int rows, int collums)
        {
            
            loadChange += new EventHandler(LoadingBar_loadChange);
            sourcelist = new List<Rectangle>();
            Image = image;
            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < collums; x++)
                {
                    sourcelist.Add(new Rectangle(image.Width * x, image.Height / rows * i, image.Width / collums, image.Height / rows));
                }
            }
        }
        public override void Update(GameTime gameTime)
        {


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, baseRect, sourcelist[3], Color.White);
            spriteBatch.Draw(Image, destRect, sourcelist[0], Color.White);
        }
        public void LoadingBar_loadChange(object sender, EventArgs e)
        {
           if (health> 0)
            {
                sourcelist[0] = new Rectangle(sourcelist[0].X, sourcelist[0].Y, (int)(((double)Image.Width / 100) * ((double)health / maxHealth * 100)), Image.Height/4);

                DestRect = new Rectangle(baseRect.X, baseRect.Y, (int)(((double)baseRect.Width / 100) * ((double)health / maxHealth * 100)), baseRect.Height);
            }
        }
    }
}
