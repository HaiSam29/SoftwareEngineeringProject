using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface IMovementStrategy
    {
        // Mag deze strategy nu uitgevoerd worden? (bv. "sta ik op de grond?")
        bool CanExecute(ICharacterContext context);

        // Voer de beweging uit
        void Execute(ICharacterContext context);
    }
}
