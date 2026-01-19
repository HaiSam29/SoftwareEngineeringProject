using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    // Dit is het Strategy Pattern. Elke strategy is een klein, uitwisselbaar stukje gedrag
    // OCP Wil je een DashStrategy of WallJumpStrategy toevoegen.
    // Maak een nieuwe class en voeg hem toe aan de lijst in CharacterFactory.
    // De Character class blijft onveranderd
    // SRP Elke strategy doet één ding
    public interface IMovementStrategy
    {
        // Mag deze strategy nu uitgevoerd worden? (bv. "sta ik op de grond?")
        bool CanExecute(ICharacterContext context);

        // Voer de beweging uit
        void Execute(ICharacterContext context);
    }
}
