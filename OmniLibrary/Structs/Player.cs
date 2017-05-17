using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmniLibrary.Structs
{
    public struct Player
    {
        public string Name;
        public int Score;
        public int gold;
        //will be 0 for no 1 for yes
        public int[] challengesCompleted;
        public int[] achievementsCompleted;
        public int[] TowersUnlocked;
        public int[] emraldsCollected;
        //enemies killed
        public int[] enemiesKilled;
        

    }
}
