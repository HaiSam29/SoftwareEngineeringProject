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
    // ICharacter: Voor buitenstaanders (game loop, enemies)
    // Dit is wat de buitenwereld mag weten over de character.
    // Position, CurrentState, FacingLeft: Voor rendering en AI want vijand moeten weten waar de speler is
    // Health, IsInvulnerable: Voor damage-systemen
    // TakeDamage(): Voor collision met vijanden
    // GetHitbox(): Voor collision detection
    // ISP De gameplay-loop hoeft niet te weten over Physics of Input - die details zijn verborgen.
    // Dit voorkomt dat code buiten Character dingen kan aanpassen die niet mogen
    // DIP Je PlayingState praat tegen ICharacter, niet tegen de concrete Character class.
    // Hierdoor kun je later makkelijk een Enemy class maken die ook ICharacter implementeert.
    public interface ICharacter
    {
        Vector2 Position { get; }
        CharacterState CurrentState { get; }
        bool FacingLeft { get; }
        void Update(float deltaTime);
        Rectangle GetHitbox();
        int Health { get; }
        bool IsInvulnerable { get; } 
        bool TakeDamage();
    }
}
