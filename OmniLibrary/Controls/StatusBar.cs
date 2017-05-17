using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmniLibrary.Controls
{
    public class StatusBar : GameObject
    {
        Texture2D Image;
        List<Rectangle> sourceList;
        Rectangle DestRect;
        SpriteFont font;
        event EventHandler onGoldChange;
        event EventHandler onHealthChange;
        bool goldChange;
        bool healthChange;
        double time;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        int health;

        public int Health
        {
            get { return health; }
            set { health = value; onHealthChange(this, null); }
        }
        int gold;

        public int Gold
        {
            get { return gold; }
            set { gold = value; onGoldChange(this, null); }
        }
        int currentFrame;
        public StatusBar(Texture2D image, int rows, int columns, Rectangle destination)
        {
            
            Image = image;
            DestRect = destination;
            sourceList=new List<Rectangle>();
            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < columns; x++)
                {
                    sourceList.Add(new Rectangle(image.Width/columns*x,image.Height/rows*i,image.Width/columns,image.Height/rows));
                }
            }
            currentFrame = 0;
            onGoldChange += new EventHandler(StatusBar_onGoldChange);
            onHealthChange += new EventHandler(StatusBar_onHealthChange);
            time = 0;
        }

        void StatusBar_onHealthChange(object sender, EventArgs e)
        {
            healthChange = true;
            
        }

        void StatusBar_onGoldChange(object sender, EventArgs e)
        {
            goldChange = true;
        }
        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (time > 200)
            {
                
                if (goldChange)
                {
                    switch (currentFrame)
                    {
                        case 0: currentFrame = 3;
                            break;
                        case 3: currentFrame = 4;
                            break;
                        case 4: currentFrame = 0; goldChange = false;
                            break;
                    }
                }
                else if (healthChange)
                {
                    switch (currentFrame)
                    {
                        case 0: currentFrame = 1;
                            break;
                        case 1: currentFrame = 2;
                            break;
                        case 2: currentFrame = 0; healthChange = false;
                            break;
                    }
                }
                else
                {
                    currentFrame = 0;
                }
                
                time = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, DestRect, sourceList[currentFrame], Color.White);
            spriteBatch.DrawString(font, Gold.ToString(), new Vector2(DestRect.X+250, DestRect.Y+5), Color.Gold);
            spriteBatch.DrawString(font, Health.ToString(), new Vector2(DestRect.X+75, DestRect.Y+5), Color.Red);
        }

    }
}
