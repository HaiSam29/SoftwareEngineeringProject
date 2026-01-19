using PlatformGame.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Game
{
    public class GameConfig : IGameConfig
    {
        public int ScreenWidth => 1920;
        public int ScreenHeight => 1080;

        public int GroundY => 785;
        public int GroundHeight => 130;

        public int CharacterFrameSize => 48;
        public float CharacterScale => 1.2f;
        public float CharacterMoveSpeed => 250f;

        public float Gravity => 800f;
        public float JumpForce => 550f;
    }
}
