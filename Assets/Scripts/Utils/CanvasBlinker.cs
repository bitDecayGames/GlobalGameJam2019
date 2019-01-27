using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CanvasBlinker : MonoBehaviour {

   private CanvasGroup canvasGroup;

        private float _frequency = 1f;

        public float Frequency {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private float time = 0;
        private float timeRatio = 0;

        private float initialAlpha = 1;

        private void Start() {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
            if (!canvasGroup) throw new Exception("CanvasGroup is missing from the Blinker object");

            initialAlpha = canvasGroup.alpha;
        }

        private void Update() {
            time -= Time.deltaTime;
            if (time < 0) {
                time = _frequency;
            }
            timeRatio = time / _frequency;
            SetAlpha(Math.Abs(timeRatio - 0.5f) / 0.5f);
        }

        private void SetAlpha(float alpha) {
            canvasGroup.alpha = alpha;
        }

        private void OnDestroy() {
            SetAlpha(initialAlpha);
        }
}
