using Microsoft.Xna.Framework.Input;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class KeyboardInputHandler : IInputHandler
    {
        private bool _jumpWasPressed = false;

        public Vector2 GetMovementDirection()
        {
            var keys = Keyboard.GetState();
            float x = 0;

            if (keys.IsKeyDown(Keys.Left) || keys.IsKeyDown(Keys.A))
            {
                x = -1;
            }
            if (keys.IsKeyDown(Keys.Right) || keys.IsKeyDown(Keys.D))
            {
                x = 1;
            }

            return new Vector2(x, 0);
        }

        public bool IsJumpPressed()
        {
            var keys = Keyboard.GetState();
            bool jumpNow = keys.IsKeyDown(Keys.Space) || keys.IsKeyDown(Keys.W);

            if (jumpNow && !_jumpWasPressed)
            {
                _jumpWasPressed = true;
                return true;
            }

            if (!jumpNow)
            {
                _jumpWasPressed = false;
            }

            return false;
        }
    }
}
