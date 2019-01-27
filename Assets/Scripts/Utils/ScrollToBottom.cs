using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utils {
    public class ScrollToBottom : MonoBehaviour {
        public float Speed = 0.01f;
        public UnityEvent OnScrolledToBottom = new UnityEvent();

        private ScrollRect scroller;
        private bool atBottom;
        private bool ready = false;

        void Start() {
            scroller = GetComponentInChildren<ScrollRect>();
            scroller.normalizedPosition = new Vector2(1, 1);
            StartCoroutine(ReadyUp());
        }

        void Update() {
            if (scroller != null && !atBottom && ready) {
                var offset = scroller.normalizedPosition;
                offset.y -= Speed * Time.deltaTime;
                scroller.normalizedPosition = offset;
                if (offset.y <= 0) {
                    atBottom = true;
                    OnScrolledToBottom.Invoke();
                }
            }
        }

        private IEnumerator ReadyUp() {
            yield return new WaitForSeconds(0.1f);
            ready = true;
        }
    }
}