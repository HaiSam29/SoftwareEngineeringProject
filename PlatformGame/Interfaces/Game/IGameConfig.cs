using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Game
{
    public interface IGameConfig
    {
        // Screen
        int ScreenWidth { get; }
        int ScreenHeight { get; }

        // World
        int GroundY { get; }
        int GroundHeight { get; }

        // Character
        int CharacterFrameSize { get; }
        float CharacterScale { get; }
        float CharacterMoveSpeed { get; }

        // Physics
        float Gravity { get; }
        float JumpForce { get; }
    }
}
