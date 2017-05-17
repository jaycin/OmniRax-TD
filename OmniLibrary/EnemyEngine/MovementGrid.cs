using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OmniLibrary;

namespace OmniLibrary.EnemyEngine
{
    public class MovementGrid : GameObject
    {
        public List<int> lstSpawn;
        public List<int> lstEnd;
        public List<int> lstStart;
        public List<Rectangle> SourceList;
        int Level;
        List<Square> squareList;

        internal List<Square> SquareList
        {
            get { return squareList; }
            set { squareList = value; }
        }
        int length;
        int height;
        public Texture2D image;
        char[,] holder;
        public MovementGrid(Rectangle ScreenRectangle, int length, int height, Texture2D texture,int level)
        {
            SquareList = new List<Square>();
            lstSpawn = new List<int>();
            lstEnd = new List<int>();
            lstStart = new List<int>();
            holder = new char[length, height];
            image = texture;
            Level = level;

            SourceList = new List<Rectangle>();

            for (int h = 0; h < length; h++)
            {
                for (int w = 0; w < height; w++)
                {
                    Rectangle R = new Rectangle(ScreenRectangle.Width / length * w, ScreenRectangle.Height / height * h, ScreenRectangle.Height / height, ScreenRectangle.Width / length);
                    SourceList.Add(R);
                }
            }
            readFromFile();

        }
        public override void Update(GameTime gametime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Square r in squareList)
            {
                r.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Writes the current map grid to file 
        /// </summary>
        public void writeTofFile()
        {
            //try
            //{
            string[] GridString = new string[holder.GetUpperBound(0)];

            using (var sr = new StreamWriter(@"MovementGrid.txt", false))
            {

                for (int i = 0; i < holder.GetUpperBound(0); i++)
                {
                    GridString[i] = "";
                    for (int j = 0; j < holder.GetUpperBound(1); j++)
                    {
                        holder[i, j] = 'X';
                        GridString[i] += "X";
                    }
                    sr.WriteLine(GridString[i]);

                }




            }
          

        }
        /// <summary>
        /// Reads movement grid from file
        /// </summary>
        public void readFromFile()
        {
            int counter = 0;
            char[,] MapChars = new char[32, 32];
            string Routeline = @"MovementGrid";
            if (Level == 0)
            {
                Routeline += ".txt";
            }
            else if (Level == 1)
            {
                Routeline += "1.txt";
            }
            else if (Level == 2)
            {
                Routeline += "2.txt";

            }
            else if (Level == 3)
            {
                Routeline += "3.txt";
            }
            using (var Stream = TitleContainer.OpenStream(Routeline))
            {
                using (var sr = new StreamReader(Stream))
                {

                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();
                        char[] Array; Array = line.ToCharArray();
                        for (int i = 0; i < Array.Length; i++)
                        {
                            if (Array[i] == 'X')
                            {
                                SquareList.Add(new Square(SourceList[i + counter * 32], i + counter * 32, false, false, false, image, 0));
                                
                            }
                            else if (Array[i] == 'O')
                            {

                                lstSpawn.Add(i + counter * 32);
                                SquareList.Add(new Square(SourceList[i + counter * 32], i + counter * 32, true, true, false, image, 1));
                                
                            }
                            else if (Array[i] == 'S')
                            {
                                lstStart.Add(i + counter * 32);
                                lstSpawn.Add(i + counter * 32);
                                SquareList.Add(new Square(SourceList[i + counter * 32], i+ counter*32, true, true, false, image, 2));
                               
                            }
                            else if (Array[i] == 'E')
                            {

                                lstEnd.Add(i + counter * 32);
                                lstSpawn.Add(i + counter * 32);
                                SquareList.Add(new Square(SourceList[i + counter * 32], i+ counter*32, true, true, true, image, 3));
                                
                            }

                            //MapChars[counter, i] = Array[i];
                            

                        }
                        counter++;

                    } 
                }

            }
        }

    }
}
