using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    // SRP  Doet alleen collision. Weet niets van rendering of input.
    // DIP Character gebruikt ICollisionSystem, niet de concrete CollisionSystem. Als je later een andere collision-engine wilt gebruiken.
    // Maak je een nieuwe implementatie zonder de rest van de code aan te passen.
    public interface ICollisionSystem
    {
        // Voeg een statisch platform toe 
        void AddCollider(Rectangle collider);
        // Controleert of de character op iets staat.
        // De out parameter geeft de exacte Y-positie van de grond terug 
        bool IsGrounded(Rectangle hitbox, out float groundY);
        // Past de snelheid aan als de speler tegen een muur of plafond botst
        Vector2 ResolveCollision(Rectangle hitbox, Vector2 velocity, float deltaTime);
    }
}
