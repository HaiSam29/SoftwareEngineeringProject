using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    // SRP Doet alleen wiskunde. Weet niets van input of sprites
    // DIP Als je later een andere physics-engine wilt.
    // maak je een Component die deze interface implementeert.
    public interface IPhysicsComponent
    {
        Vector2 Velocity { get; set; }
        // Verhoogt de verticale snelheid
        void ApplyGravity(float deltaTime);
        // Telt de snelheid op bij de positie
        // Zonder ref zou de methode een kopie aanpassen in plaats van het origineel.
        // Met ref passen we het echte position-veld aan.
        void ApplyVelocity(ref Vector2 position, float deltaTime);
    }
}
