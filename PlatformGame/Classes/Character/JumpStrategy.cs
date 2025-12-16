using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private bool _wasJumpPressed;

        public JumpStrategy(float jumpForce = 400f)
        {
            _jumpForce = jumpForce;
        }

        public void Execute(IPhysicsComponent physics, IInputHandler input, bool isGrounded, float moveSpeed)
        {
            var keys = Keyboard.GetState();
            bool isJumpPressed = keys.IsKeyDown(Keys.Space);

            // Jump alleen als:
            // - Op de grond staat
            // - Space wordt ingedrukt
            // - Space was niet al ingedrukt (voorkomt infinite jump)
            if (isGrounded && isJumpPressed && !_wasJumpPressed)
            {
                physics.Velocity = new Vector2(physics.Velocity.X, -_jumpForce);
            }

            _wasJumpPressed = isJumpPressed;
        }
    }
}
