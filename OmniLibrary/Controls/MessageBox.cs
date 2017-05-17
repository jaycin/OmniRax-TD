using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OmniLibrary;
using OmniLibrary.Map;

namespace OmniLibrary.Controls
{
    public class MessageBox:GameObject
    {
        float LifeTime;
        float TotalTime;
        SpriteFont Font;
        string Text;
        string Splitter;
        List<Container> LstContainer;
        Texture2D Image;
        Rectangle DrawRectangle;
        ObjectManager ObjMan;
        bool isText;
        int counter = 0;
        bool spawn;
        public MessageBox(Texture2D image,SpriteFont font, string text,Vector2 pos ,float lifeTime)
        {
            Image = image;
            isText = true;
            AnimationTime = 250;
            Font = font;
            Text = text;
            DrawRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)text.Length * 13, 100);
            LifeTime = lifeTime;
        }
        public MessageBox(Texture2D image, SpriteFont font, List<Container> listContainer, Vector2 position,ObjectManager objMan)
        {
            Text = "100  -  200";
            AnimationTime = 250;
            isText = false;
            IsLocked = true;
            Image = image;
            LstContainer = listContainer;
            ObjMan = objMan;
            Font = font;
            spawn = true;
            DrawRectangle = new Rectangle((int)position.X, (int)position.Y, LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
            if (!ObjMan.GameRectangle().Contains(new Point((int)position.X + DrawRectangle.Width,(int)position.Y + DrawRectangle.Height)))
            {
                DrawRectangle = new Rectangle((int)position.X - DrawRectangle.Width/2, (int)position.Y - DrawRectangle.Height/2,
                    LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
                spawn = false;
            }
            else if (!ObjMan.GameRectangle().Contains(new Point((int)position.X - DrawRectangle.Width,(int)Position.Y - DrawRectangle.Height)))
            {
                DrawRectangle = new Rectangle((int)position.X + DrawRectangle.Width / 2, (int)position.Y + DrawRectangle.Height / 2,
                    LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
                spawn = false;
            }
            else if (!ObjMan.GameRectangle().Contains(new Point((int)position.X + DrawRectangle.Width,(int)Position.Y - DrawRectangle.Height)))
            {
                DrawRectangle = new Rectangle((int)position.X - DrawRectangle.Width / 2, (int)position.Y + DrawRectangle.Height / 2,

                    LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
                spawn = false;
            }
            else if  (!ObjMan.GameRectangle().Contains(new Point((int)position.X - DrawRectangle.Width,(int)Position.Y + DrawRectangle.Height)))
            {
                DrawRectangle = new Rectangle((int)position.X + DrawRectangle.Width / 2, (int)position.Y - DrawRectangle.Height / 2,
                    LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
                spawn = false;
            }

            if(spawn)
            {
                DrawRectangle = new Rectangle((int)position.X, (int)position.Y,
                    LstContainer[0].DestRectangle.Width * LstContainer.Count, LstContainer[0].DestRectangle.Height * LstContainer.Count);
                spawn = false;
            }
           
          
            
            spawn = true;
            Location = DrawRectangle;

            obj_MouseOver += new EventHandler(MessageBox_obj_MouseOver);
            obj_Leave += new EventHandler(MessageBox_obj_Leave);
            base.Type = "MessageBox";
          
        }

       

        public override void Update(GameTime gametime)
        {
            Time += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            TotalTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (Time>AnimationTime)
            {
                if (!isText)
                {
                    while (spawn)
                    {
                        foreach (Container c in LstContainer)
                        {
                            c.DestRectangle = new Rectangle(DrawRectangle.X + counter * c.DestRectangle.Height, DrawRectangle.Y, c.DestRectangle.Width, c.DestRectangle.Height);
                            ObjMan.AddLst.Add(c);
                            counter++;
                        }
                        Location = DrawRectangle;
                        counter = 0;
                        spawn = false;
                    }
                }
                else
                {
                    if (LifeTime < TotalTime)
                    {
                        this.IsDead = true;
                    }
                } 
            }
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, DrawRectangle, Color.White);
            if (isText)
            {
                spriteBatch.DrawString(Font, Text, new Vector2(DrawRectangle.X + 15, DrawRectangle.Y + 15), Color);
            }
            else
            {
                spriteBatch.DrawString(Font, Text, new Vector2(DrawRectangle.X + 15, DrawRectangle.Y+LstContainer[0].DestRectangle.Height), Color.Gold);
            }

        }
        void MessageBox_obj_Leave(object sender, EventArgs e)
        {
            MouseOver = false;
        }

        void MessageBox_obj_MouseOver(object sender, EventArgs e)
        {
            this.MouseOver = true;
        }
    }
}
