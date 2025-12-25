using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class Character : ICharacter
    {
        private Vector2 _position;
        private readonly IPhysicsComponent _physics;
        private readonly IInputHandler _input;
        private readonly ICollisionSystem _collision;
        private readonly List<IMovementStrategy> _strategies;
        private readonly int _frameWidth;
        private readonly int _frameHeight;
        private readonly float _moveSpeed;
        private const float movementThreshold = 0.1f;
        private const float facingThreshold = 0.1f;
        private const float landingDuration = 0.2f;
        private float _landingTimer;
        private bool _wasInAir;
        private const float attackDuration = 0.4f;
        private float _attackTimer;
        private readonly Rectangle _screenBounds;

        public Vector2 Position => _position;
        public CharacterState CurrentState { get; private set; } = CharacterState.Idle;
        public bool FacingLeft { get; private set; }

        public Character(Vector2 startPosition, IPhysicsComponent physics, IInputHandler input,
                ICollisionSystem collision, List<IMovementStrategy> strategies, Rectangle screenBounds,
                int frameWidth = 48, int frameHeight = 48, float moveSpeed = 150f)
        {
            _position = startPosition;
            _physics = physics;
            _input = input;
            _collision = collision;
            _strategies = strategies;
            _frameWidth = frameWidth;   
            _frameHeight = frameHeight; 
            _moveSpeed = moveSpeed;
            _screenBounds = screenBounds;
        }

        public void Update(float deltaTime)
        {
            bool wasGrounded = _collision.IsGrounded(GetHitbox(), out float groundY);

            // Check attack input
            if (_input.IsAttackPressed() && _attackTimer <= 0 && wasGrounded)
            {
                _attackTimer = attackDuration;
            }

            // Movement (alleen als niet aan het attacken)
            if (_attackTimer <= 0)
            {
                foreach (var strategy in _strategies)
                    strategy.Execute(_physics, _input, wasGrounded, _moveSpeed);
            }
            else
            {
                _physics.Velocity = new Vector2(0, _physics.Velocity.Y);
            }

            _physics.ApplyGravity(deltaTime);

            _physics.Velocity = _collision.ResolveCollision(GetHitbox(), _physics.Velocity, deltaTime);

            _physics.ApplyVelocity(ref _position, deltaTime);

            ClampToScreenBounds();

            // Check ground AFTER movement
            bool isGroundedNow = _collision.IsGrounded(GetHitbox(), out float newGroundY);

            if (isGroundedNow && _physics.Velocity.Y >= 0)
            {
                float targetY = newGroundY - _frameHeight;

                // Als we dicht genoeg bij de grond zijn, snap exact
                if (Math.Abs(_position.Y - targetY) < 10f)
                {
                    _position.Y = targetY;
                    _physics.Velocity = new Vector2(_physics.Velocity.X, 0);

                    if (_wasInAir)
                    {
                        _landingTimer = landingDuration;
                    }
                }
            }

            _wasInAir = !isGroundedNow;

            // Update timers
            if (_landingTimer > 0)
                _landingTimer -= deltaTime;

            if (_attackTimer > 0)
                _attackTimer -= deltaTime;

            UpdateState(isGroundedNow);
            UpdateFacing();
        }

        private void ClampToScreenBounds()
        {
            // Clamp X (links en rechts)
            _position.X = MathHelper.Clamp(
                _position.X,
                _screenBounds.Left,                      // Linker rand
                _screenBounds.Right - _frameWidth        // Rechter rand minus character breedte
            );

            // Optioneel: Clamp Y (boven en onder)
            // _position.Y = MathHelper.Clamp(
            //     _position.Y,
            //     _screenBounds.Top,
            //     _screenBounds.Bottom - _frameHeight
            // );
        }

        private void UpdateState(bool isGrounded)
        {
            // Attack heeft hoogste prioriteit
            if (_attackTimer > 0)
            {
                CurrentState = CharacterState.Attacking;
                return;
            }

            // In de lucht = Jumping
            if (!isGrounded)
            {
                CurrentState = CharacterState.Jumping;
                return;
            }

            // Net geland = Landing
            if (_landingTimer > 0)
            {
                CurrentState = CharacterState.Landing;
                return;
            }

            // Op de grond: Idle of Running
            CurrentState = Math.Abs(_physics.Velocity.X) > movementThreshold
                ? CharacterState.Running
                : CharacterState.Idle;
        }

        private void UpdateFacing()
        {
            const float threshold = 0.1f;

            if (_physics.Velocity.X < -threshold)
                FacingLeft = true;
            else if (_physics.Velocity.X > threshold)
                FacingLeft = false;
        }

        public Rectangle GetHitbox() => new((int)_position.X, (int)_position.Y, _frameWidth, _frameHeight);
    }

}
