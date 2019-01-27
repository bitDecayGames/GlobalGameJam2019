using GameInput;
using UnityEngine;

namespace Interactables {
    public abstract class PressAndHoldInteractable : AbstractInteractable {
        private float _timeToComplete = 3f;
        private float time = 0;
        public float TimeToComplete {
            get { return _timeToComplete; }
            set { _timeToComplete = value; }
        }

        public override void Interact(InputController interactee) {
            if (!_isInteracting) {
                _isInteracting = true;
                time = _timeToComplete;
                _interactee = interactee;
                OnInteract();
            }
        }

        private void Update() {
            if (_isInteracting) {
                if (IsInteractButtonBeingHeld()) {
                    time -= Time.deltaTime;
                    if (time < 0) {
                        _isInteracting = false;
                        time = 0;
                        Trigger();
                    }
                } else Disconnect();
            }
        }

        public override void Disconnect() {
            if (_isInteracting) {
                _isInteracting = false;
                time = 0;
                _interactee = null;
                OnDisconnect();
            }
        }

        protected bool IsInteractButtonBeingHeld() {
            return _interactee.ControllerMapper.InteractDown();
        }
    }
}