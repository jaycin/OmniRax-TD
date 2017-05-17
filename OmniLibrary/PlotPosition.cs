using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System.IO;

namespace OmniLibrary.Map
{
    [Serializable]
   public struct PlotPosition
    {

        public List<Vector2> Position;
    }
}
