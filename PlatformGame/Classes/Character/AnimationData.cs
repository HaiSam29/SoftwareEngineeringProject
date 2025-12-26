using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class AnimationData
    {
        public Texture2D Texture { get; set; }
        public int FrameCount { get; set; }
        public float FrameDuration { get; set; }
        public int FrameHeight { get; set; }
        public int FrameWidth { get; set; }
    }
}
