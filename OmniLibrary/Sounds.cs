using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace OmniLibrary
{
    public class Sounds:Game
    {
        public  SoundEffect ClickEffect1;
        public SoundEffect ClickEffect2;
        public SoundEffect ClickEffect3;
        public SoundEffect LogoStop;
        public SoundEffect ThemeAppear;

        public Sounds()
        {
            Content.RootDirectory = "Content";
            //ThemeAppear = Content.Load<SoundEffect>(@"Sounds\sword-unsheathe5");
            //ClickEffect1 = Content.Load<SoundEffect>(@"Sounds\metal-small1");
            //ClickEffect2 = Content.Load<SoundEffect>(@"Sounds\metal-small2");
            //ClickEffect3 = Content.Load<SoundEffect>(@"Sounds\metal-small3");
            //LogoStop = Content.Load<SoundEffect>(@"Sounds\sword-unsheathe5");
        }

    }
}
