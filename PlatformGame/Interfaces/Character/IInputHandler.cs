using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    // Converteert hardware-specifieke input naar abstracte commando's
    // DIP De Character class is niet afhankelijk van Keyboard.GetState().
    // Hierdoor kun je later een GamepadInputHandler of zelfs een AIInputHandler maken zonder de Character aan te passen.
    // OCP Wil je touchscreen-controls toevoegen.
    // Maak een TouchInputHandler die IInputHandler implementeert.
    // Geen enkele bestaande class hoeft aangepast te worden.
    public interface IInputHandler
    {
        float GetMoveDirectionX();
        bool IsJumpPressed();
        bool IsAttackPressed();
        bool IsCrouchPressed();
    }
}
