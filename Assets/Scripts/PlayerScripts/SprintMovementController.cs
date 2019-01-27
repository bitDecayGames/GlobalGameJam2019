using System;
using GameInput;
using UnityEngine;

namespace PlayerScripts {
    public class SprintMovementController : MonoBehaviour {
        private GamePadMovementController mover;
        private InputController input;
        private InvisibilityCloak cloak;
        public float SprintSpeed = 30f;
        private float initialMovementSpeed;

        private void Start() {
            mover = GetComponentInChildren<GamePadMovementController>();
            if (!mover) throw new Exception("GamePadMovementController is missing from SprintMovementController object");
            input = GetComponentInChildren<InputController>();
            if (!input) throw new Exception("InputController is missing from SprintMovementController object");
            cloak = GetComponentInChildren<InvisibilityCloak>();
            if (!cloak) throw new Exception("InvisibilityCloak is missing from SprintMovementController object");

            initialMovementSpeed = mover.MovementSpeed;
        }

        private void Update() {
            if (input.ControllerMapper.RunDown()) {
                mover.MovementSpeed = SprintSpeed;
                if (cloak.IsActive) cloak.IsActive = false;
            } else {
                mover.MovementSpeed = initialMovementSpeed;
                if (!cloak.IsActive) cloak.IsActive = true;
            }
        }
    }
}