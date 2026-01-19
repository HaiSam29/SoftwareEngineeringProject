using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    // Beheert de visuele weergave. Het weet welke texture bij welke state hoort en welk plaatje frame er getoond moet worden.
    public class AnimationData
    {
        public Texture2D Texture { get; set; }
        public int FrameCount { get; set; }
        public float FrameDuration { get; set; }
        public int FrameHeight { get; set; }
        public int FrameWidth { get; set; }
    }
}
