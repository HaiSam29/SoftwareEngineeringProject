using PlatformGame.Interfaces.Character.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;

namespace PlatformGame.Classes.Character.States
{
    // De basisklasse voor alle states. Hier staat code die in bijna elke state nodig is zoals zwaartekracht toepassen en timers aftellen
    // Zodat je dat niet alles opnieuw hoeft te typen.
    public abstract class CharacterStateBase : ICharacterState
    {
        public virtual void Enter(ICharacterContext context) { }

        public virtual void HandleInput(ICharacterContext context)
        {
            // Default: Check voor attack input
            if (context.Input.IsAttackPressed())
            {
                context.TransitionTo(CharacterState.Attacking);
            }
        }

        // Deze methode Past physics toe en roept ResolveCollision aan.
        // Dit zorgt dat physics in elke state (rennen, springen, stilstaan) consistent werken.
        public virtual void Update(ICharacterContext context, float deltaTime)
        {
            // Physics
            context.Physics.ApplyGravity(deltaTime);
            context.Physics.Velocity = context.Collision.ResolveCollision(context.GetHitbox(), context.Physics.Velocity, deltaTime);

            Vector2 pos = context.Position;
            context.Physics.ApplyVelocity(ref pos, deltaTime);
            context.Position = pos;

            context.UpdateTimers(deltaTime);
        }
    }
}
