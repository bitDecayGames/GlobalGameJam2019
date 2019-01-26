using System;
using UnityEngine;

namespace Utils {
    public class Blinker : MonoBehaviour {
        private SpriteRenderer sprite;

        private float _frequency = 0.5f;

        public float Frequency {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private float time = 0;
        private float timeRatio = 0;

        private float initialAlpha = 1;

        private void Start() {
            sprite = GetComponentInChildren<SpriteRenderer>();
            if (!sprite) throw new Exception("SpriteRenderer is missing from the Blinker object");

            initialAlpha = sprite.color.a;
        }

        private void Update() {
            time -= Time.deltaTime;
            if (time < 0) time = _frequency;
            timeRatio = time / _frequency;

            SetAlpha(Math.Abs(timeRatio - 0.5f) / 0.5f);
        }

        private void SetAlpha(float alpha) {
            var c = sprite.color;
            c.a = alpha;
            sprite.color = c;
        }

        private void OnDestroy() {
            SetAlpha(initialAlpha);
        }
    }
}