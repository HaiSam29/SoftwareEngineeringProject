using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Classes.Game;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using PlatformGame.Interfaces.Map;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformGame.Interfaces.Game;
using PlatformGame.Interfaces.Character.States;
using PlatformGame.Classes.Character.States;

namespace PlatformGame.Classes.Character
{
    // Verantwoordelijk voor het aanmaken van een Character. Het laadt textures, leest de config uit, en zet alle dependencies aan elkaar
    // SRP Creatie is gescheiden van Gebruik. Als je een ander soort character wilt (bijv. met andere start-stats), pas je alleen deze factory aan, niet de Character class zelf.
    public class CharacterFactory
    {
        private ContentManager _content;
        private IGameConfig _config; // Config opslaan

        // Constructor injectie
        public CharacterFactory(ContentManager content, IGameConfig config)
        {
            _content = content;
            _config = config;
        }

        public (ICharacter, ISprite) CreateCharacter(Vector2 startPosition, Rectangle screenBounds, ITileCollisionProvider tileCollision, int tileSize)
        {
            // Textures laden
            var idleTexture = _content.Load<Texture2D>("idle");
            var runningTexture = _content.Load<Texture2D>("Running");
            var jumpingTexture = _content.Load<Texture2D>("Jumping");
            var fallingTexture = _content.Load<Texture2D>("Falling");
            var landingTexture = _content.Load<Texture2D>("Landing");
            var attackingTexture = _content.Load<Texture2D>("Attacking");
            var crouchingTexture = _content.Load<Texture2D>("Crouching");

            // Maak Componenten aan
            var collision = new CollisionSystem();
            collision.SetTileCollisionProvider(tileCollision, tileSize);

            // Gebruik _config.JumpForce 
            var strategies = new List<IMovementStrategy>
            {
                new GroundedMovementStrategy(),
                new JumpStrategy(_config.JumpForce)
            };

            // Gebruik _config.Gravity
            var physics = new PhysicsComponent(_config.Gravity);
            var input = new KeyboardInputHandler();

            // Bereken afmetingen met config
            int scaledSize = (int)(_config.CharacterFrameSize * _config.CharacterScale);

            IStateFactory stateFactory = new CharacterStateFactory();

            // Maak de Character met config waarden
            var character = new Character(
                startPosition,
                physics,
                input,
                collision,
                strategies,
                screenBounds,
                stateFactory,
                scaledSize,
                scaledSize,
                _config.CharacterMoveSpeed // Config
            );

            // Maak de Sprite met config waarden
            var sprite = new Sprite(_config.CharacterFrameSize, _config.CharacterFrameSize);

            // Registraties
            sprite.RegisterAnimation(CharacterState.Idle, idleTexture, 4, 0.2f);
            sprite.RegisterAnimation(CharacterState.Running, runningTexture, 6, 0.12f);
            sprite.RegisterAnimation(CharacterState.Jumping, jumpingTexture, 1, 0.1f, 64);
            sprite.RegisterAnimation(CharacterState.Falling, fallingTexture, 1, 0.1f, 64);
            sprite.RegisterAnimation(CharacterState.Landing, landingTexture, 1, 0.1f, 64);
            sprite.RegisterAnimation(CharacterState.Attacking, attackingTexture, 8, 0.5f, null, 80);
            sprite.RegisterAnimation(CharacterState.Crouching, crouchingTexture, 1, 0.1f);

            sprite.Update(CharacterState.Idle, 0);

            return (character, sprite);
        }
    }
}
