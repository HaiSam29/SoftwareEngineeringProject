using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Enums;
using PlatformGame.Interfaces.Character;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Classes.Character
{
    public class Sprite: ISprite
    {
        private readonly Dictionary<CharacterState, AnimationConfig> _animations;

        private readonly int _frameWidth;
        private readonly int _frameHeight;

        private float _frameTimer = 0f;
        private int _currentFrameIndex = 0;
        private CharacterState _lastState;

        public Rectangle CurrentFrame { get; private set; }
        public Texture2D CurrentTexture { get; private set; }

        public Sprite(int frameWidth, int frameHeight)
        {
            _frameHeight = frameHeight;
            _frameWidth = frameWidth;
            _animations = new Dictionary<CharacterState, AnimationConfig>();
        }

        // Configuratie from the outside
        public void RegisterAnimation(CharacterState state, AnimationConfig config)
        {
            _animations[state] = config;
        }

        // Convenience method
        public void RegisterAnimation(CharacterState state, Texture2D texture, int frameCount, float frameDuration= 0.1f)
        {
            RegisterAnimation(state, new AnimationConfig(texture, frameCount, frameDuration));
        }

        public void Update(CharacterState state, float deltaTime)
        {
            // State changed? reset animation
            if (state != _lastState)
            {
                _currentFrameIndex = 0;
                _frameTimer = 0f;
                _lastState = state;
            }

            if (!_animations.ContainsKey(state))
            {
                return;
            }

            var config = _animations[state];
            CurrentTexture = config.Texture;

            _frameTimer += deltaTime;

            if (_frameHeight >= config.FrameDuration)
            {
                _frameTimer -= config.FrameDuration;
                _currentFrameIndex++;

                if (_currentFrameIndex >= config.FrameCount)
                    _currentFrameIndex = 0;
            }

            CurrentFrame = new Rectangle(_currentFrameIndex * _frameWidth, 0, _frameWidth, _frameHeight);
        }
    }
}
