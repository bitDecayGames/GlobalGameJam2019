using System;
using UnityEngine;

namespace PlayerScripts {
    public class InvisibilityCloak : MonoBehaviour {
        private SpriteRenderer sprite;

        private float _fadeOutSpeed = 1f;

        public float FadeOutSpeed {
            get { return _fadeOutSpeed; }
            set { _fadeOutSpeed = value; }
        }

        private bool _isActive = false;
        public bool IsActive {
            get { return _isActive; }
            set {
                if (value && !_isActive) {
                    time = _fadeOutSpeed;
                    SetAlpha(initialAlpha);
                } else if(!value) {
                    time = 0;
                    SetAlpha(initialAlpha);
                }
                _isActive = value;
            }
        }
        
        private float time = 0;
        private float timeRatio = 0;
        private float initialAlpha = 1;

        private void Start() {
            sprite = GetComponentInChildren<SpriteRenderer>();
            if (!sprite) throw new Exception("SpriteRenderer is missing from the InvisibilityCloak object");

            initialAlpha = sprite.color.a;
        }

        private void Update() {
            if (time > 0) {
                time -= Time.deltaTime;
                if (time < 0) time = 0;
                timeRatio = time / _fadeOutSpeed;

                SetAlpha(initialAlpha * timeRatio);                
            }
        }

        private void SetAlpha(float alpha) {
            if (sprite) {
                var c = sprite.color;
                c.a = alpha;
                sprite.color = c;
            }
        }

        private void OnDestroy() {
            SetAlpha(initialAlpha);
        }

        public void Ping() {
            if (_isActive) {
                IsActive = false;
                IsActive = true;
            }
        }
    }
}