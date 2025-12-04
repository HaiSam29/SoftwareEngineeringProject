using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface ICharacter
    {
        Vector2 Position { get; }
        CharacterState CurrentState { get; }
        bool FacingLeft { get; }
        void Update(float deltaTime);
        Rectangle GetHitbox();
    }
}
