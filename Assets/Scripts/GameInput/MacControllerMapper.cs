﻿using UnityEngine;

namespace GameInput {
    public class MacControllerMapper : InputMapper {
        public MacControllerMapper(string playerID) {
            playerId = playerID;
        }

        override public bool InteractPressed() {
            //Any face buttons interact
            if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "16")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "17")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "18")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "19")) || KeyboardInteractPressed()) {
                return true;
            }

            return false;
        }

        public override bool APressed()
        {
            return Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "16"));
        }

        public override bool XPressed()
        {
            return Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "18"));
        }

        public override bool YPressed()
        {
            return Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "19"));
        }

        public override bool BPressed()
        {
            return Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "17"));
        }

        override public bool RunPressed() {
            //Any bumper buttons pressed
            if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "13")) ||
                Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "14")) || KeyboardRunPressed()) {
                return true;
            }

            return false;
        }

        public override bool InteractDown() {
            //Any face buttons interact
            if (Input.GetKey(string.Format("joystick {0} button {1}", playerId, "16")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "17")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "18")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "19")) || KeyboardInteractDown()) {
                return true;
            }

            return false;
        }

        public override bool RunDown() {
            //Any bumper buttons pressed
            if (Input.GetKey(string.Format("joystick {0} button {1}", playerId, "13")) ||
                Input.GetKey(string.Format("joystick {0} button {1}", playerId, "14")) || KeyboardRunDown()) {
                return true;
            }

            return false;
        }
    }
}