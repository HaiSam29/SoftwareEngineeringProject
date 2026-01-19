using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Enemy
{
    // Voor beweging
    // OCP Nieuwe bewegingen toevoegen zonder bestaande code aan te passen
    // Strategy Pattern: Beweging is uitwisselbaar
    public interface IEnemyMovementStrategy
    {
        void Move(IEnemyContext context, float deltaTime);
    }
}
