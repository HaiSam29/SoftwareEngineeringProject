using PlatformGame.Enums;
using PlatformGame.Interfaces.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Enemy
{
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
