using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OmniLibrary.Controls;
using OmniLibrary.Towers;

namespace OmniLibrary.Map
{
    public class StageBox : GameObject
    {
        Texture2D boxImage;
        int Level;
        Dictionary<int ,Texture2D> towerImage;
        Dictionary<int, Texture2D> mapImage;
        
        SpriteFont Font;
        Rectangle boxSource;
        List<MageTower> towerList;
        Rectangle mapSource;
        Rectangle towerDest;
        Rectangle mapDest;
        Rectangle DestRect;
        ObjectManager ObjMan;
        double time;


        int currentFrame;
        public StageBox(Texture2D image,Rectangle destination,Dictionary<int,Texture2D> MapPreview,Dictionary<int,Texture2D> towerPreview,string LevelDescription,SpriteFont font,int level,ObjectManager objMan)
        {
            Level = level;
            ObjMan = objMan;
            time = 0;
            Font = font;
            boxImage = image;
            towerImage = towerPreview;
            mapImage = MapPreview;
            DestRect = destination;
            Description = LevelDescription;
            mapSource = new Rectangle(0, 0, MapPreview[level].Width, MapPreview[level].Height);
            mapDest = new Rectangle(destination.X+50,destination.Y+50,destination.Width/2,destination.Height/2);
            towerDest = new Rectangle(destination.X+boxImage.Width+50, destination.Y + 80,50,100 );
            towerList=new List<MageTower>();
            int Rows;
            int Colmns;
            for (int y = 0; y < towerImage.Count; y++)
			{
			 if(y ==0)
             {
                
                 MageTower m = new MageTower(towerPreview[y],2,18,0.5f,new Vector2((int)destination.X+boxImage.Width+50, destination.Y + 80),null,false);
                 towerList.Add(m);
                 ObjMan.AddLst.Add(m);

             }
             if (y == 1)
             {
                 MageTower m = new MageTower(towerPreview[y], 2, 18, 0.5f, new Vector2((int)destination.X + boxImage.Width + 200, destination.Y + 80), null, false);
                 towerList.Add(m);
                 objMan.AddLst.Add(m);
             }

            }
            currentFrame = 0;
        }
        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (time > 150)
            {

                if (currentFrame == towerList.Count() - 3)
                {
                    currentFrame = 0;
                }
                else
                    currentFrame++;
                time = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boxImage, DestRect, boxSource, Color.White);
           
            spriteBatch.DrawString(Font,"Availible Towers:",new Vector2(towerDest.X +20,towerDest.Y-20),Color.White);

            spriteBatch.DrawString(Font, Description, new Vector2(mapDest.X+mapImage[Level].Width, mapDest.Y+mapImage[Level].Height+10), Color.Red);
           
        }
        public void RemoveTowers()
        {
            foreach (GameObject obj  in ObjMan)
            {
                if (obj.IsTower)
                {
                    obj.IsDead = true;
                }
            }
        }

    }
}
