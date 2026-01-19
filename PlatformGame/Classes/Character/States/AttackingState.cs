using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;

namespace PlatformGame.Classes.Character.States
{
    // Speler slaat
    // Negeert input HandleInput is leeg, zodat je niet kunt lopen tijdens het slaan
    // Gaat terug naar Idle als de animatie klaar is
    public class AttackingState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Attacking;
            context.AttackTimer = 0.4f; 
            context.Physics.Velocity = Vector2.Zero; // Stilstand
        }

        public override void HandleInput(ICharacterContext context)
        {
            // Geen input tijdens aanval
        }

        public override void Update(ICharacterContext context, float deltaTime)
        {
            base.Update(context, deltaTime);

            if (context.AttackTimer <= 0)
            {
                context.TransitionTo(CharacterState.Idle);
            }
        }
    }
}
