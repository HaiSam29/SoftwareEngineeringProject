using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Interfaces.Character
{
    public interface ICharacterContext
    {
        // Components
        IPhysicsComponent Physics { get; }
        IInputHandler Input { get; }
        ICollisionSystem Collision { get; }

        // AANGEPAST: Van List naar IReadOnlyList
        IReadOnlyList<IMovementStrategy> Strategies { get; }

        // Data & Properties
        Vector2 Position { get; set; }
        int FrameWidth { get; }
        int FrameHeight { get; }
        float MoveSpeed { get; }

        // Timers & State data
        float LandingTimer { get; set; }
        float AttackTimer { get; set; }
        float InvulnerabilityTimer { get; set; }
        CharacterState CurrentStateEnum { get; set; }

        // Methods
        void TransitionTo(CharacterState state);
        void UpdateTimers(float deltaTime);
        Rectangle GetHitbox();
    }
}
