using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Interfaces.Character
{
    public interface ISprite
    {
        Rectangle CurrentFrame { get; }
        Texture2D CurrentTexture { get; }
        void Update(CharacterState state, float deltaTime);
    }
}
