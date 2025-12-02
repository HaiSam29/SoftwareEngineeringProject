using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface IInputHandler
    {
        Vector2 GetMovementDirection();
        bool IsJumpPressed();
    }
}
