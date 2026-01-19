using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Enemy
{
    public interface IEnemyStateFactory
    {
        IEnemyState Create(EnemyStateType stateType);
    }
}
