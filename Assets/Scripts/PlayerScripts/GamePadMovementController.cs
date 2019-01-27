using System;
using GameInput;
using UnityEngine;

namespace PlayerScripts {
    public class GamePadMovementController : MonoBehaviour {
        private InputController input;

        public float MovementSpeed = 0.2f;

        private void Start() {
            input = GetComponent<InputController>();
            if (!input) throw new Exception("InputController is missing from GamePadMovementController object");
        }

        private void Update() {
            float moveLR = input.ControllerMapper.GetHorizontalMovement();
            float moveUD = input.ControllerMapper.GetVerticalMovement();

            float newFacing = FindDegree(moveLR, moveUD);
            var newPosition = new Vector3(moveLR, moveUD);
            if(newPosition.magnitude > input.deadzone) {
                transform.rotation = Quaternion.AngleAxis(newFacing, Vector3.forward);
                transform.Translate(newPosition.magnitude * MovementSpeed * Time.deltaTime, 0, 0);
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