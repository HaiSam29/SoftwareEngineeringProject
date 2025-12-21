using Microsoft.Xna.Framework.Graphics;
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
    public class Sprite: ISprite
    {
        private readonly Dictionary<CharacterState, AnimationData> _animations = new();
        private readonly int _frameWidth, _frameHeight;
        private float _frameTimer;
        private int _currentFrame;
        private CharacterState _lastState;

        public Rectangle CurrentFrame { get; private set; }
        public Texture2D CurrentTexture { get; private set; }

        public Sprite(int frameWidth, int frameHeight)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
        }

        public void RegisterAnimation(CharacterState state, Texture2D texture, int frameCount, float frameDuration, int? customFrameHeight = null, int? customFrameWidth = null)
        {
            _animations[state] = new AnimationData
            {
                Texture = texture,
                FrameCount = frameCount,
                FrameDuration = frameDuration,
                FrameHeight = customFrameHeight ?? _frameHeight,
                FrameWidth = customFrameWidth ?? _frameWidth
            };
        }

        public void Update(CharacterState state, float deltaTime)
        {
            if (!_animations.TryGetValue(state, out var anim))
            {
                if (!_animations.TryGetValue(CharacterState.Idle, out anim))
                    return;
            }

            if (state != _lastState)
            {
                _currentFrame = 0;
                _frameTimer = 0;
                _lastState = state;
            }

            CurrentTexture = anim.Texture;
            _frameTimer += deltaTime;

            if (_frameTimer >= anim.FrameDuration)
            {
                _frameTimer -= anim.FrameDuration;
                _currentFrame = (_currentFrame + 1) % anim.FrameCount;
            }

            // Gebruik custom width én height per animatie
            CurrentFrame = new Rectangle(_currentFrame * anim.FrameWidth, 0, anim.FrameWidth, anim.FrameHeight);
        }

        private class AnimationData
        {
            public Texture2D Texture { get; set; }
            public int FrameCount { get; set; }
            public float FrameDuration { get; set; }
            public int FrameHeight { get; set; }
            public int FrameWidth { get; set; }
        }
    }
}
