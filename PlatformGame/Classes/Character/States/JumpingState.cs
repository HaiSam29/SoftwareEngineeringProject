using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Classes.Character.States
{
    // Speler gaat omhoog.
    // Zodra de snelheid > 0 omhoog verandert in < 0 omlaag, door zwaartekracht, schakelt hij over naar FallingState.
    public class JumpingState : CharacterStateBase
    {
        public override void Enter(ICharacterContext context)
        {
            context.CurrentStateEnum = CharacterState.Jumping;
        }

        public override void HandleInput(ICharacterContext context)
        {
            // Air Control
            foreach (var strat in context.Strategies)
            {
                if (strat.CanExecute(context))
                    strat.Execute(context);
            }
        }

        public override void Update(ICharacterContext context, float deltaTime)
        {
            base.Update(context, deltaTime);

            if (context.Physics.Velocity.Y > 0)
            {
                context.TransitionTo(CharacterState.Falling);
            }
        }
    }
}
