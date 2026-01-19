using PlatformGame.Enums;
using PlatformGame.Interfaces.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Enemy
{
    // Converteert een EnemyStateType enum naar de bijbehorende IEnemyState class.
    // Het gebruikt state pooling (hergebruik van dezelfde state objecten) in plaats van elke keer new PatrolState() aan te maken.
    // SRP Doet alleen state creatie. De Enemy hoeft niet te weten hoe states gemaakt worden
    // DIP Enemy hangt af van IEnemyStateFactory, niet van concrete state classes
    // OCP Nieuwe states toevoegen is simpel: voeg een entry toe aan de dictionary
    public class EnemyStateFactory : IEnemyStateFactory
    {
        private readonly Dictionary<EnemyStateType, IEnemyState> _states;

        public EnemyStateFactory()
        {
            // State pooling (hergebruiken van state objecten)
            _states = new Dictionary<EnemyStateType, IEnemyState>
            {
                { EnemyStateType.Patrol, new PatrolState() },
                { EnemyStateType.Attack, new AttackState() }
            };
        }

        public IEnemyState Create(EnemyStateType stateType)
        {
            if (_states.TryGetValue(stateType, out var state))
                return state;

            throw new ArgumentOutOfRangeException(nameof(stateType), stateType, "Unknown enemy state");
        }
    }
}
