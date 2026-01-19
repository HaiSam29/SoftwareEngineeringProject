using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character.States
{
    // Converteert een CharacterState enum naar de bijbehorende ICharacterState class.
    // SRP De Character class hoeft niet te weten hoe states gemaakt worden.
    // DIP Als je later states met parameters wilt pas je alleen de factory aan, niet de Character class.
    public interface IStateFactory
    {
        ICharacterState Create(CharacterState state);
    }
}
