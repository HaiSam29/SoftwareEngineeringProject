using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Character.States;
using PlatformGame.Classes.Character.States;

namespace PlatformGame.Classes.Character
{
    // Centrale hub bewaart alle data maar bevat geen gedragslogica
    // Delegeert dat naar states en Strategies
    // SRP puur een data container en coördinator berekent zelf geen physics of collision
    // ISP implementeert ICharacterContext(voor de states) en ICharacter (voor de game loop) zodat buitenstaanders alleen zien wat ze moeten zien.
    // DIP Alle hulpmiddelen(Input, Physics, Collision) komen binnen via constructor 

    public class Character : ICharacter, ICharacterContext
    {
        // Configuratie
        public const float DefaultMovementThreshold = 0.1f;
        public const float DefaultLandingDuration = 0.2f;
        public const float DefaultAttackDuration = 0.4f;
        public const float DefaultInvulnerabilityDuration = 1.5f;
        public const float DefaultKnockbackForce = -200f;

        // ICharacterContext Properties
        public Vector2 Position { get; set; }
        public IPhysicsComponent Physics { get; private set; }
        public IInputHandler Input { get; private set; }
        public ICollisionSystem Collision { get; private set; }

        // IReadOnlyList voor betere encapsulation
        // We gebruiken een private setter zodat alleen Character de lijst kan vervangen,
        // en de buitenwereld (inclusief States) alleen kan lezen.
        public IReadOnlyList<IMovementStrategy> Strategies { get; private set; }

        public Rectangle ScreenBounds { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public float MoveSpeed { get; private set; }

        // Timers 
        public float LandingTimer { get; set; }
        public float AttackTimer { get; set; }
        public float InvulnerabilityTimer { get; set; }

        // State Machine
        private readonly IStateFactory _stateFactory;
        private ICharacterState _currentState;

        public CharacterState CurrentStateEnum { get; set; }
        public bool FacingLeft { get; set; }
        public int Health { get; private set; } = 3;

        // Derived properties
        public bool IsInvulnerable => InvulnerabilityTimer > 0;
        CharacterState ICharacter.CurrentState => CurrentStateEnum;
        Vector2 ICharacter.Position => Position;

        public Character(
            Vector2 startPosition,
            IPhysicsComponent physics,
            IInputHandler input,
            ICollisionSystem collision,
            List<IMovementStrategy> strategies, // Constructor accepteert nog wel een List
            Rectangle screenBounds,
            IStateFactory stateFactory,
            int frameWidth = 48,
            int frameHeight = 48,
            float moveSpeed = 150f)
        {
            Position = startPosition;
            Physics = physics;
            Input = input;
            Collision = collision;
            Strategies = strategies; // List implementeert IReadOnlyList
            ScreenBounds = screenBounds;
            _stateFactory = stateFactory ?? throw new ArgumentNullException(nameof(stateFactory));

            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            MoveSpeed = moveSpeed;

            TransitionTo(CharacterState.Idle);
        }

        public void TransitionTo(CharacterState state)
        {
            _currentState = _stateFactory.Create(state);
            _currentState.Enter(this);
        }

        public void Update(float deltaTime)
        {
            _currentState.HandleInput(this);
            _currentState.Update(this, deltaTime);

            ClampToScreenBounds();
            UpdateFacing();
        }

        public void UpdateTimers(float deltaTime)
        {
            if (InvulnerabilityTimer > 0) InvulnerabilityTimer -= deltaTime;
            if (LandingTimer > 0) LandingTimer -= deltaTime;
            if (AttackTimer > 0) AttackTimer -= deltaTime;
        }

        private void ClampToScreenBounds()
        {
            Position = new Vector2(
                MathHelper.Clamp(Position.X, ScreenBounds.Left, ScreenBounds.Right - FrameWidth),
                Position.Y
            );
        }

        private void UpdateFacing()
        {
            if (Physics.Velocity.X < -DefaultMovementThreshold) FacingLeft = true;
            else if (Physics.Velocity.X > DefaultMovementThreshold) FacingLeft = false;
        }

        public bool TakeDamage()
        {
            if (IsInvulnerable) return false;
            Health--;
            InvulnerabilityTimer = DefaultInvulnerabilityDuration;
            Physics.Velocity = new Vector2(Physics.Velocity.X, DefaultKnockbackForce);
            return true;
        }

        public Rectangle GetHitbox() => new((int)Position.X, (int)Position.Y, FrameWidth, FrameHeight);
    }
}

