using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace OmniLibrary.Map
{
    public class WayPointManager:List<WayPoints>
    {
        Vector2 offsetVector;


        public WayPointManager()
        {
            
            
        }
        public Queue<Vector2> GetArray(int wayPointNumber)
        {
            
            int counter = 0;
            foreach (WayPoints wp in this)
            {
                offsetVector = new Vector2(10, 10);
                if (counter == wayPointNumber)
                {
                    return wp.waypointQ();
                }
            }
            return null;
        }
    }
}
