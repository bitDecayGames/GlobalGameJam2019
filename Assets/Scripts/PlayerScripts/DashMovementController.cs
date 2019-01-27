using System;
using GameInput;
using UnityEngine;

namespace PlayerScripts {
    public class DashMovementController : MonoBehaviour {
        private GamePadMovementController mover;
        private InputController input;
        private InvisibilityCloak cloak;
        private Rigidbody2D body;


        public float Intensity = 50f;
        public float RefreshRate = 2f;
        public int MaxNumberOfDashes = 3;

        private int currentDashes = 0;
        private float time = 0;

        private void Start() {
            mover = GetComponentInChildren<GamePadMovementController>();
            if (!mover) throw new Exception("GamePadMovementController is missing from DashMovementController object");
            input = GetComponentInChildren<InputController>();
            if (!input) throw new Exception("InputController is missing from DashMovementController object");
            cloak = GetComponentInChildren<InvisibilityCloak>();
            if (!cloak) throw new Exception("InivisibilityCloak is missing from DashMovementController object");
            body = GetComponentInChildren<Rigidbody2D>();
            if (!body) throw new Exception("RigidBody2D could not be found on the DashMovementController object");

            currentDashes = MaxNumberOfDashes;
        }

        private void Update() {
            if (input.ControllerMapper.RunPressed()) {
                if (Dash()) {
                    cloak.Ping();
                }
            }

            if (time > 0) {
                time -= Time.deltaTime;
                if (time < 0) ResetDashes();
            }
        }

        public void ResetDashes() {
            currentDashes = MaxNumberOfDashes;
            time = 0;
        }

        public bool Dash() {
            Debug.Log("Dash");
            if (currentDashes > 0) {
                var vel = body.velocity;
                var velMag = vel.magnitude;
                if (velMag < 0.01f && velMag > -0.01f) {
                    Debug.Log("Velocity was too low to dash: " + vel);
                    return false; // if the player isn't moving, they can't dash
                }

                var dir = vel.normalized;
                dir *= Intensity;
                body.AddForce(dir, ForceMode2D.Impulse);

                currentDashes--;
                time = RefreshRate;
                return true;
            }

            return false;
        }
    }
}