using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OmniLibrary.Controls
{
    public class Coin:GameObject
    {
        Texture2D coinTex;
        Rectangle DestRect;
        List<Rectangle> SourceRect;
        int current;
        public Coin(Texture2D image, Rectangle Destination, int rows, int cols)
        {
            IsEnabled = true;
            Visable = true;
            AnimationTime = 50;
            Time = 0;
            SourceRect = new List<Rectangle>();
            DestRect=Destination;
            coinTex = image;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    SourceRect.Add(new Rectangle(coinTex.Width / cols * j, coinTex.Height / rows * i, coinTex.Width / cols, coinTex.Height / rows));
                }
            }

        }
        public override void Update(GameTime gametime)
        {
            Time += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            //if (Time > AnimationTime)
            //{

            //    if (current < SourceRect.Count() - 1)
            //    {
            //        current++;
            //    }
            //    else
            //    {
            //        current = 0;
            //    }
            //    Time = 0;
            //}
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(coinTex, DestRect, SourceRect[current], Color.White);
        }
    }
}
