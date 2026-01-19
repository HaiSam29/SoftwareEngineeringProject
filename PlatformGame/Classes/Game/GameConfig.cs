using PlatformGame.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Game
{
    // Implementeert IGameConfig en levert alle configuratiewaarden
    // Game1 maakt precies 1 GameConfig aan en geeft die door aan alle states/factories
    // SRP 1 verantwoordelijkheid: configuratiewaarden aanbieden
    // DIP Code hangt af van IGameConfig, niet van GameConfig. Alleen Game1 kent de concrete implementatie
    // OCP Nieuwe config‑waarden toevoegen kan zonder dat bestaande gebruikers aangepast moeten worden
    public class GameConfig : IGameConfig
    {
        public int ScreenWidth => 1920;
        public int ScreenHeight => 1080;

        public int GroundY => 785;
        public int GroundHeight => 130;

        public int CharacterFrameSize => 48;
        public float CharacterScale => 1.2f;
        public float CharacterMoveSpeed => 250f;

        public float Gravity => 800f;
        public float JumpForce => 550f;
    }
}
