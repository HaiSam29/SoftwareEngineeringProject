using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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

        public Vector2 Position => _position;
        public CharacterState CurrentState { get; private set; }
        public bool FacingLeft { get; private set; }

        public Character(Vector2 startposition, IPhysicsComponent physics, IInputHandler input, ICollisionSystem collision, List<IMovementStrategy> strategies,
            int frameWidth = 48, int frameHeigt = 48, float movespeed = 150f)
        {
            _position = startposition;
            _physics = physics;
            _input = input;
            _collision = collision;
            _strategies = strategies;
            _frameWidth = frameWidth;
            _frameHeight = frameHeigt;
            _moveSpeed = movespeed;
            CurrentState = CharacterState.Idle;
        }

        public void Update(float deltaTime)
        {
            // Check ground
            bool isGrounded = _collision.CheckGround(GetHitbox(), out float groundY);

            // Execute movement strategies
            foreach (var strategy in _strategies)
            {
                strategy.Execute(_physics, _input, isGrounded, _moveSpeed);
            }

            // Apply physics
            _physics.ApplyGravity(deltaTime);
            _physics.ApplyVelocity(ref _position, deltaTime);

            // Resolve collisions
            ResolveCollisions(isGrounded, groundY);

            // Update character state
            UpdateState(isGrounded);
            UpdateFacingDirection();

        }

        private void ResolveCollisions(bool isGrounded, float groundY)
        {
            Rectangle hitBox = GetHitbox();

            // Ground collision
            if (isGrounded && _physics.Velocity.Y >= 0)
            {
                _position.Y = groundY - _frameHeight;
                _physics.Velocity = new Vector2(_physics.Velocity.X, 0);
            }

            // Ceiling collision
            if (_collision.CheckCeiling(hitBox, out float ceilingY) && _physics.Velocity.Y < 0)
            {
                _position.Y = ceilingY;
                _physics.Velocity = new Vector2(_physics.Velocity.X, 0);
            }

        }

        private void UpdateState(bool isGrounded)
        {
            if (!isGrounded)
            {
                CurrentState = _physics.Velocity.Y < 0 ?
                    CharacterState.Jumping : CharacterState.Falling;
            }
            else if (_physics.Velocity.X != 0)
            {
                CurrentState = CharacterState.Running;
            }
            else
            {
                CurrentState = CharacterState.Idle;
            }
        }

        private void UpdateFacingDirection()
        {
            if (_physics.Velocity.X < 0)
            {
                FacingLeft = true;
            }
            else if (_physics.Velocity.X > 0)
            {
                FacingLeft = false;
            }
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _frameWidth, _frameHeight);
        }
    }

}
