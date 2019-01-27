using UnityEngine;
using UnityEngine.Events;

namespace Utils {
    public class ColliderPassthrough : MonoBehaviour{
        public class CollisionPassthroughEvent : UnityEvent<Collision2D> {}
        public class ColliderPassthroughEvent : UnityEvent<Collider2D> {}

        public CollisionPassthroughEvent OnCollisionEnter2DEvent = new CollisionPassthroughEvent();
        public CollisionPassthroughEvent OnCollisionExit2DEvent = new CollisionPassthroughEvent();
        public ColliderPassthroughEvent OnTriggerEnter2DEvent = new ColliderPassthroughEvent();
        public ColliderPassthroughEvent OnTriggerExit2DEvent = new ColliderPassthroughEvent();

        private void OnCollisionEnter2D(Collision2D other) {
            OnCollisionEnter2DEvent.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other) {
            OnCollisionExit2DEvent.Invoke(other);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            OnTriggerEnter2DEvent.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other) {
            OnTriggerExit2DEvent.Invoke(other);
        }
    }
}