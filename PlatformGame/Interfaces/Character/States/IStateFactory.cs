using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character.States
{
    public interface IStateFactory
    {
        ICharacterState Create(CharacterState state);
    }
}
