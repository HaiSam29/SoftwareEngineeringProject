using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterEntity = PlatformGame.Classes.Character.Character;

namespace PlatformGame.Interfaces.Character.States
{
    // Elk state implementeert deze interface
    // OCP Wil je een State toevoegen.
    // Maak een nieuwe class die ICharacterState implementeert.
    // De Character class hoeft niet aangepast.
    public interface ICharacterState
    {
        // Wordt aangeroepen bij de overgang naar deze state
        void Enter(ICharacterContext context);
        // Leest input en past velocity aan of schakelt naar andere states.
        void HandleInput(ICharacterContext context);
        // Past physics toe en checkt condities
        void Update(ICharacterContext context, float deltaTime);
    }
}
