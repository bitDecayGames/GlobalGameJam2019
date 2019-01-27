using System;
using GameInput;
using Interactables.Progress;
using UnityEngine;

namespace Interactables {
    public abstract class PressAndHoldInteractable : AbstractInteractable {
        private float _timeToComplete = 3f;
        private float time = 0;
        private int soundId;
        public float TimeToComplete {
            get { return _timeToComplete; }
            set { _timeToComplete = value; }
        }

        private void Awake()
        {
            SuccessThreshold = _timeToComplete;
        }

        public override void Interact(InputController interactee) {
            if (!_isInteracting) {
                soundId = FMODSoundEffectsPlayer.Instance.PlaySustainedSoundEffect(SFX.PickLock);
                _isInteracting = true;
                time = _timeToComplete;
                _interactee = interactee;
                OnInteract();
            }
        }
        
        protected void Update() {
            if (_isInteracting) {
                if (IsInteractButtonBeingHeld()) {
                    time -= Time.deltaTime;
                    ProgressBarController.AddToSuccesses(Time.deltaTime);
                    if (time < 0) {
                        _isInteracting = false;
                        time = 0;
                        FMODSoundEffectsPlayer.Instance.StopSustainedSoundEffect(soundId);
                        Trigger();
                    }
                }
                else
                {
                    FMODSoundEffectsPlayer.Instance.StopSustainedSoundEffect(soundId);
                    Disconnect();
                }
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

        private void OnDestroy()
        {
            if (soundId == 0)
            {
                FMODSoundEffectsPlayer.Instance.StopSustainedSoundEffect(soundId);   
            }
        }
    }
}