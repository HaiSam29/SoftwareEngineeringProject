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
    // Beheert de visuele weergave. Het weet welke texture bij welke state hoort en welk plaatje frame er getoond moet worden.
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

            // Gebruik custom width en height per animatie
            CurrentFrame = new Rectangle(_currentFrame * anim.FrameWidth, 0, anim.FrameWidth, anim.FrameHeight);
        }

        // Soms is een aanval animatie plaatje groter dan een idle plaatje.
        // Als je die gewoon tekent, lijkt het alsof de speler in de grond zakt.
        // Deze methode berekent hoeveel pixels we het plaatje omhoog of opzij moeten schuiven zodat de voeten op dezelfde plek blijven staan.
        public Vector2 CalculateDrawOffset(int baseFrameSize, float scale)
        {
            Vector2 offset = Vector2.Zero;

            // Offset voor hogere sprites (voeten op grond)
            if (CurrentFrame.Height > baseFrameSize)
            {
                offset.Y -= (CurrentFrame.Height - baseFrameSize) * scale;
            }

            // Offset voor bredere sprites (center character)
            if (CurrentFrame.Width > baseFrameSize)
            {
                offset.X -= (CurrentFrame.Width - baseFrameSize) * scale / 2f;
            }

            return offset;
        }
    }
}
