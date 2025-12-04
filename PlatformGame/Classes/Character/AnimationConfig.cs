using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class AnimationConfig
    {
        public Texture2D Texture { get; set; }
        public int FrameCount { get; set; }
        public float FrameDuration { get; set; } // Duration of each frame in seconds

        public AnimationConfig(Texture2D texture, int framecount, float frameDuration= 0.1f)
        {
            Texture = texture;
            FrameCount = framecount;
            FrameDuration = frameDuration;
        }
    }
}
