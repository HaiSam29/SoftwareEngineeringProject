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
    // Het past zwaartekracht toe en telt snelheid op bij de positie.
    // SRP Doet alleen wiskunde. Weet niets van inputs of sprites.
    public class PhysicsComponent : IPhysicsComponent
    {
        private readonly float _gravity;
        public Vector2 Velocity { get; set; }

        public PhysicsComponent(float gravity = 800f)
        {
            _gravity = gravity;
            Velocity = Vector2.Zero;
        }

        public void ApplyGravity(float deltaTime)
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + _gravity * deltaTime);
        }

        public void ApplyVelocity(ref Vector2 position, float deltaTime)
        {
            position += Velocity * deltaTime;
        }
    }
}
