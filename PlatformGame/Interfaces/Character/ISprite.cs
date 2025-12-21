using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Classes.Character;

namespace PlatformGame.Interfaces.Character
{
    public interface ISprite
    {
        Rectangle CurrentFrame { get; }
        Texture2D CurrentTexture { get; }
        void Update(CharacterState state, float deltaTime);
        void RegisterAnimation(CharacterState state, Texture2D texture, int frameCount, float frameDuration, int? customFrameHeight = null, int? customFrameWidth = null);
        Vector2 CalculateDrawOffset(int baseFrameSize, float scale);
    }
}
