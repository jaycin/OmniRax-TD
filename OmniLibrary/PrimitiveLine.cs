﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OmniLibrary.Towers
{
    /// <summary>
    /// A class to make primitive 2D objects out of lines.
    /// </summary>
    public class PrimitiveLine
    {
        Texture2D pixel;
        List<Vector2> vectors;
        List<Rectangle> LstGrid;
        float[] distance = new float[2];
        /// <summary>
        /// Gets/sets the colour of the primitive line object.
        /// </summary>
        public Color Colour;
        
        /// <summary>
        /// Gets/sets the position of the primitive line object.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Gets/sets the render depth of the primitive line object (0 = front, 1 = back)
        /// </summary>
        public float Depth;
        float[] Angle = new float[2];
        /// <summary>
        /// Gets the number of vectors which make up the primtive line object.
        /// </summary>
        public int CountVectors
        {
            get
            {
                return vectors.Count;
            }
        }

        /// <summary>
        /// Creates a new primitive line object.
        /// </summary>
        /// <param name="graphicsDevice">The Graphics Device object to use.</param>
        public PrimitiveLine(GraphicsDevice graphicsDevice)
        {
            // create pixels
            pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] pixels = new Color[1];
            pixels[0] = Color.White;
            pixel.SetData<Color>(pixels);

            Colour = Color.White;
            Position = new Vector2(0, 0);
            Depth = 0;

            vectors = new List<Vector2>();
        }




        /// <summary>
        /// Adds a vector to the primive live object.
        /// </summary>
        /// <param name="vector">The vector to add.</param>
        public void AddVector(Vector2 vector)
        {
            vectors.Add(vector);
        }

        /// <summary>
        /// Insers a vector into the primitive line object.
        /// </summary>
        /// <param name="index">The index to insert it at.</param>
        /// <param name="vector">The vector to insert.</param>
        public void InsertVector(int index, Vector2 vector)
        {
            vectors.Insert(index, vector);
        }

        /// <summary>
        /// Removes a vector from the primitive line object.
        /// </summary>
        /// <param name="vector">The vector to remove.</param>
        public void RemoveVector(Vector2 vector)
        {
            vectors.Remove(vector);
        }

        /// <summary>
        /// Removes a vector from the primitive line object.
        /// </summary>
        /// <param name="index">The index of the vector to remove.</param>
        public void RemoveVector(int index)
        {
            vectors.RemoveAt(index);
        }

        /// <summary>
        /// Clears all vectors from the primitive line object.
        /// </summary>
        public void ClearVectors()
        {
            vectors.Clear();
        }

        /// <summary>
        /// Renders the primtive line object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use to render the primitive line object.</param>
        public void Render(SpriteBatch spriteBatch)
        {
            if (vectors.Count < 2)
                return;

            for (int i = 1; i < vectors.Count; i++)
            {
                Vector2 vector1 = (Vector2)vectors[i - 1];
                Vector2 vector2 = (Vector2)vectors[i];

                // calculate the distance between the two vectors
                float distance = Vector2.Distance(vector1, vector2);

                // calculate the angle between the two vectors
                float angle = (float)Math.Atan2((double)(vector2.Y - vector1.Y),
                    (double)(vector2.X - vector1.X));

                // stretch the pixel between the two vectors
                spriteBatch.Draw(pixel,
                    Position + vector1,
                    null,
                    Colour,
                    angle,
                    Vector2.Zero,
                    new Vector2(distance, 1),
                    SpriteEffects.None,
                    Depth);
            }
        }
        public void DrawGrid(SpriteBatch spriteBatch,Texture2D block)
        {
            
            foreach (Rectangle r in LstGrid)
            {
                spriteBatch.Draw(pixel, new Vector2(r.X, r.Y), null, Colour, Angle[1], Vector2.Zero, new Vector2(distance[1], 1), SpriteEffects.None, Depth);
                spriteBatch.Draw(pixel, new Vector2(r.X+ r.Width, r.Y), null, Colour, Angle[1], Vector2.Zero, new Vector2(distance[1], 1), SpriteEffects.None, Depth);
                spriteBatch.Draw(pixel, new Vector2(r.X, r.Y), null, Colour, Angle[0], Vector2.Zero, new Vector2(distance[0], 1), SpriteEffects.None, Depth);
                spriteBatch.Draw(pixel, new Vector2(r.X,r.Y+ r.Height), null, Colour, Angle[0], Vector2.Zero, new Vector2(distance[0], 1), SpriteEffects.None, Depth);
                
                
                
            }
        }

        /// <summary>
        /// Creates a circle starting from 0, 0.
        /// </summary>
        /// <param name="radius">The radius (half the width) of the circle.</param>
        /// <param name="sides">The number of sides on the circle (the more the detailed).</param>
        public void CreateCircle(float radius, int sides)
        {
            vectors.Clear();

            float max = 2 * (float)Math.PI;
            float step = max / (float)sides;

            for (float theta = 0; theta < max; theta += step)
            {
                vectors.Add(new Vector2(radius * (float)Math.Cos((double)theta),
                    radius * (float)Math.Sin((double)theta)));
            }

            // then add the first vector again so it's a complete loop
            vectors.Add(new Vector2(radius * (float)Math.Cos(0),
                    radius * (float)Math.Sin(0)));
        }
        public void CreateGrid(List<Rectangle> lstGrid)
        {
            float[] angle = new float[2];
            LstGrid = lstGrid;

            angle[0] = (float)Math.Atan2((double)LstGrid[0].Y, (double)LstGrid[0].Width - (double)LstGrid[0].X);
            angle[1] = (float)Math.Atan2((double)LstGrid[0].Height, (double)LstGrid[0].Width - (double)LstGrid[0].X);
            angle[1] = (float)Math.Atan2((double)LstGrid[0].Height - (double)LstGrid[0].Y,0);
            Angle = angle;

            distance[0] = Vector2.Distance(new Vector2(LstGrid[0].X, LstGrid[0].Y), new Vector2(LstGrid[0].Width, LstGrid[0].Y));
            distance[1] = Vector2.Distance(new Vector2(LstGrid[0].X, LstGrid[0].Y), new Vector2(LstGrid[0].X, LstGrid[0].Height));
                
        }
        public void renderGrid(SpriteBatch spriteBatch)
        {
            foreach (Vector2 c in vectors)
            {
                spriteBatch.Draw(pixel,
                    c,
                    null,
                    Colour,
                    0,
                    Vector2.Zero,
                    new Vector2(1,1),
                    SpriteEffects.None,
                    Depth);
            }
        }


    }
}
