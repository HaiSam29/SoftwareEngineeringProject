using PlatformGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformGame.Interfaces.Character
{
    // ICharacterContext: Voor binnenstaanders (states, strategies)
    // Dit is een uitgebreide interface speciaal voor de State classes.
    // Het geeft toegang tot alles wat een state nodig heeft om zijn werk te doen: physics, input, collision, timers, etc.
    // IReadOnlyList<IMovementStrategy> Strategies: States kunnen de lijst lezen en strategieën uitvoeren, maar niet aanpassen.
    // Dit voorkomt bugs waarbij een state per ongeluk de lijst leegmaakt
    // Position { get; set; }: States mogen de positie aanpassen (bijvoorbeeld om de speler op de grond te snappen na een landing).
    // TransitionTo(CharacterState state): States kunnen naar andere states overschakelen.
    // ISP  States krijgen precies de tools die ze nodig hebben, niet meer, niet minder.
    // SRP De Character class zelf hoeft niet te weten hoe states werken;
    // states krijgen een "context" en doen hun ding.
    public interface ICharacterContext
    {
        // Components
        IPhysicsComponent Physics { get; }
        IInputHandler Input { get; }
        ICollisionSystem Collision { get; }

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
