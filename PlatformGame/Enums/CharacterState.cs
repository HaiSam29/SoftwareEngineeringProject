using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Enums
{
    // Een simpele lijst van alle mogelijke toestanden
    // Dit wordt gebruikt om: Animaties te kiezen en States op te vragen uit de factory
    public enum CharacterState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Landing,
        Attacking,
        Crouching
    }
}
