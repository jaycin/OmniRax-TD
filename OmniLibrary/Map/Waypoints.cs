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
public class WayPoints : GameObject
    {
        Queue<Vector2> WaypointQ;
        public WayPoints(Vector2[] Points)
        {
            Queue<Vector2> WaypointQ = new Queue<Vector2>();
            foreach (Vector2 v in Points)
            {
                WaypointQ.Enqueue(v); 
            }

        }
        public Queue<Vector2> waypointQ()
        {
            return WaypointQ;
        }
        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
