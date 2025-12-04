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
    public class KeyboardInputHandler : IInputHandler
    {
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
    }
}
