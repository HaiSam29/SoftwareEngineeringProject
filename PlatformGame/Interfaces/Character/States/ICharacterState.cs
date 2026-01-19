using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterEntity = PlatformGame.Classes.Character.Character;

namespace PlatformGame.Interfaces.Character.States
{
    public interface ICharacterState
    {
        void Enter(ICharacterContext context);
        void HandleInput(ICharacterContext context);
        void Update(ICharacterContext context, float deltaTime);
    }
}
