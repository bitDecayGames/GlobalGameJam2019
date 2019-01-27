using System;
using GameInput;
using UnityEngine;

namespace PlayerScripts {
    public class GamePadMovementController : MonoBehaviour {
        private InputController input;
        private Rigidbody2D body;

        public float MovementSpeed = 0.2f;
        
        public bool IsFrozen { get; set; }

        private void Start() {
            input = GetComponent<InputController>();
            if (!input) throw new Exception("InputController is missing from GamePadMovementController object");
            body = GetComponent<Rigidbody2D>();
            if (!body) throw new Exception("Rigidbody2D is missing from GamePadMovementController object");
            IsFrozen = false;
        }

        private void Update() {
            if (!IsFrozen) {
                float moveLR = input.ControllerMapper.GetHorizontalMovement();
                float moveUD = input.ControllerMapper.GetVerticalMovement();

                float newFacing = FindDegree(moveLR, moveUD);
                var dir = new Vector3(moveLR, moveUD);

                if (dir.magnitude > input.deadzone) {
                    transform.rotation = Quaternion.AngleAxis(newFacing, Vector3.forward);

                    var vel = dir * MovementSpeed * Time.deltaTime;
                    body.AddForce(vel, ForceMode2D.Impulse);
                }
            }
        }

        public static float FindDegree(float x, float y)
        {
            float value = (Mathf.Atan2(y,x) / (float)Math.PI) * 180f;
            if (value < 0) value += 360f;

            return value;
        }
    }
}