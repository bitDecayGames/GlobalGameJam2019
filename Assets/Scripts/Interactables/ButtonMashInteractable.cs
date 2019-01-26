using UnityEngine;

namespace Interactables {
    public abstract class ButtonMashInteractable : AbstractInteractable {
        private const float HITPOINT_DECAY_SCALAR = 1f;
        private const float HITPOINT_PER_BUTTON_PRESS = 0.3f;
        
        private float _buttonMashHitpoints = 3f;
        private float currentHitpoints = 0;

        public float ButtonMashHitpoints {
            get { return _buttonMashHitpoints; }
            set { _buttonMashHitpoints = value; }
        }

        private float _cooldown = 1f;
        private float _time = 0;

        public float Cooldown {
            get { return _cooldown; }
            set { _cooldown = value; }
        }

        public override void Interact(GameObject interactee) {
            if (!_isInteracting && _time <= 0) {
                _isInteracting = true;
                currentHitpoints = _buttonMashHitpoints * .1f;
                _interactee = interactee;
                OnInteract();
            }
        }

        private void Update() {
            if (_isInteracting) {
                currentHitpoints -= Time.deltaTime * HITPOINT_DECAY_SCALAR;
                if (IsInteractButtonPressed()) currentHitpoints += HITPOINT_PER_BUTTON_PRESS;
                if (currentHitpoints < 0) Disconnect();
                else if (currentHitpoints >= _buttonMashHitpoints) Trigger();
            } else if (_time > 0) _time -= Time.deltaTime;
        }

        public override void Disconnect() {
            if (_isInteracting) {
                _isInteracting = false;
                currentHitpoints = 0;
                _interactee = null;
                OnDisconnect();
            }
        }

        protected bool IsInteractButtonPressed() {
            // TODO: check if button has been pressed on this frame
            return Input.GetKeyDown(KeyCode.I);
        }

        protected override void OnTrigger() {
            _isInteracting = false;
            currentHitpoints = 0;
            _time = _cooldown;
        }
    }
}