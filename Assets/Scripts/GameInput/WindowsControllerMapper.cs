using UnityEngine;

namespace GameInput {
    public class WindowsControllerMapper : InputMapper {
        public WindowsControllerMapper(string playerID) {
            playerId = playerID;
        }

        override public bool InteractPressed() {
            //Any face buttons interact
            if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "0")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "1")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "2")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "3")) || KeyboardInteractPressed()) {
                return true;
            }

            return false;
        }

        override public bool RunPressed() {
            //Any bumper buttons pressed
            if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "4")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "5")) || KeyboardRunPressed()) {
                return true;
            }

            return false;
        }

        public override bool InteractDown() {
            //Any face buttons interact
            if (Input.GetKey(string.Format("joystick {0} button {1}", playerId, "0")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "1")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "2")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "3")) || KeyboardInteractDown()) {
                return true;
            }

            return false;
        }

        public override bool RunDown() {
            //Any bumper buttons pressed
            if (Input.GetKey(string.Format("joystick {0} button {1}", playerId, "4")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "5")) || KeyboardRunDown()) {
                return true;
            }

            return false;
        }
    }
}