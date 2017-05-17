using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


using OmniLibrary;
using OmniLibrary.Controls;
using OmniLibrary.Map;
using OmniLibrary.EnemyEngine;
using OmniLibrary.Towers;


namespace OmniRax.Game_Screens
{
    class Options:BaseGameState
    {
        public Options(Game game, GameStateManager manager) 
            : base(game, manager) 
        {
            base.Initialize();
        }
    }
}
