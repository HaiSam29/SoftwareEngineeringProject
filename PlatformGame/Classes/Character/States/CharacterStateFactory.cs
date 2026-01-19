using PlatformGame.Enums;
using PlatformGame.Interfaces.Character.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character.States
{
    public class CharacterStateFactory : IStateFactory
    {
        private readonly Dictionary<CharacterState, ICharacterState> _states;

        public CharacterStateFactory()
        {
            // Let op: dit werkt alleen veilig als je states geen “eigen” state bewaren.
            // (In jouw code is dat zo: timers zitten op Character, niet op de state.)
            _states = new Dictionary<CharacterState, ICharacterState>
            {
                { CharacterState.Idle, new IdleState() },
                { CharacterState.Running, new RunningState() },
                { CharacterState.Jumping, new JumpingState() },
                { CharacterState.Falling, new FallingState() },
                { CharacterState.Landing, new LandingState() },
                { CharacterState.Attacking, new AttackingState() },
                { CharacterState.Crouching, new CrouchingState() }
            };
        }

        public ICharacterState Create(CharacterState state)
        {
            if (_states.TryGetValue(state, out var s))
                return s;

            throw new ArgumentOutOfRangeException(nameof(state), state, "Unknown state");
        }
    }
}
