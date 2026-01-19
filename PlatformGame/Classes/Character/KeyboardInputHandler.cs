using Microsoft.Xna.Framework.Input;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    // Vertaalt toetsenbord input naar abstracte commando's.
    // DIP en ISP 
    // De rest van de game praat tegen IInputHandler.
    // Als je later gamepad-support wilt toevoegen, maak je een GamepadInputHandler en hoef je de Character code niet aan te passen.
    public class KeyboardInputHandler : IInputHandler
    {
        private bool _wasAttackPressed;
        public float GetMoveDirectionX()
        {
            var keys = Keyboard.GetState();
            float x = 0;

            if (keys.IsKeyDown(Keys.Left) || keys.IsKeyDown(Keys.A))
                x = -1;
            if (keys.IsKeyDown(Keys.Right) || keys.IsKeyDown(Keys.D))
                x = 1;

            return x;
        }

        public bool IsJumpPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W);
        }

        public bool IsAttackPressed()
        {
            var keys = Keyboard.GetState();
            bool isPressed = keys.IsKeyDown(Keys.X);

            // Return true alleen bij eerste frame van druk
            if (isPressed && !_wasAttackPressed)
            {
                _wasAttackPressed = true;
                return true;
            }

            if (!isPressed)
                _wasAttackPressed = false;

            return false;
        }

        public bool IsCrouchPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S);
        }
    }
}
