using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class JumpStrategy: IMovementStrategy
    {
        private readonly float _jumpForce;

        public JumpStrategy(float jumpForce = 400f)
        {
            _jumpForce = jumpForce;
        }

        public void Execute(IPhysicsComponent physics, IInputHandler input, bool isGrounded, float moveSpeed)
        {
            if (isGrounded && input.IsJumpPressed())
            {
                physics.Velocity = new System.Numerics.Vector2(physics.Velocity.X, -_jumpForce);
            }   
        }
    }
}
