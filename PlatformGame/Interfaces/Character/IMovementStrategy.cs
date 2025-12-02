using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface IMovementStrategy
    {
        void Execute(IPhysicsComponent physics, IInputHandler input, bool isGrounded, float moveSpeed);
    }
}
