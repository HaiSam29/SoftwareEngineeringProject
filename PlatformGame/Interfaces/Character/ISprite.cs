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
        // SRP Doet alleen animatie. Weet niets van physics of input.
        // ISP De renderer (in PlayingState.Draw()) hoeft alleen CurrentFrame en CurrentTexture te kennen. N
        // iet hoe animaties geregistreerd worden.
        Rectangle CurrentFrame { get; }
        Texture2D CurrentTexture { get; }
        // Bereken welk frame van de animatie nu getoond moet worden.
        void Update(CharacterState state, float deltaTime);
        // Koppel een texture aan een state
        void RegisterAnimation(CharacterState state, Texture2D texture, int frameCount, float frameDuration, int? customFrameHeight = null, int? customFrameWidth = null);
        // Wat de renderer nodig heeft om te tekenen.
        Vector2 CalculateDrawOffset(int baseFrameSize, float scale);
    }
}
