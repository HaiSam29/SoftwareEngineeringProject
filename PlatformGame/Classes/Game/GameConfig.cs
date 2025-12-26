using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Game
{
    public static class GameConfig
    {
        // Screen
        public const int screenWidth = 1920;
        public const int screenHeight = 1080;

        // World
        public const int groundY = 785;
        public const int groundHeight = 130;

        // Character
        public const int characterFrameSize = 48;
        public const float characterScale = 1.25f;
        public const float characterMoveSpeed = 250f;

        // Physics
        public const float gravity = 800f;
        public const float jumpForce = 500f;
    }
}
