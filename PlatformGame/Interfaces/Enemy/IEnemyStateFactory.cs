using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Enemy
{
    // Maakt states op basis van een enum
    // SRP Enemy hoeft niet te weten hoe states gemaakt worden
    // Enemy hangt af van de interface, niet van concrete factory
    public interface IEnemyStateFactory
    {
        IEnemyState Create(EnemyStateType stateType);
    }
}
