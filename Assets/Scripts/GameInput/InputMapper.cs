﻿using UnityEngine;

namespace GameInput {
    public abstract class InputMapper  {
        
        public string playerId;

        abstract public bool InteractPressed();
        
        abstract public bool APressed();
        abstract public bool XPressed();
        abstract public bool YPressed();
        abstract public bool BPressed();

        abstract public bool RunPressed();
        
        abstract public bool InteractDown();

        abstract public bool RunDown();

        protected bool KeyboardInteractPressed() {
            return Input.GetKeyDown(KeyCode.Space);
        }
        
        protected bool KeyboardRunPressed() {
            return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
        }
        
        protected bool KeyboardInteractDown() {
            return Input.GetKey(KeyCode.Space);
        }
        
        protected bool KeyboardRunDown() {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }

        string BuildHorizontalAxisString()
        {
            return "Horizontal_" + playerId;
        }

        string BuildVerticalAxisString()
        {
            return "Vertical_" + playerId;
        }

        public virtual float GetHorizontalMovement()
        {
//            if(playerId == 1.ToString())
//                Debug.Log(BuildHorizontalAxisString());

            return Input.GetAxis(BuildHorizontalAxisString()) + (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
        }

        public virtual float GetVerticalMovement()
        {
//            if (playerId == 1.ToString())
//                Debug.Log(BuildVerticalAxisString());
            return Input.GetAxis(BuildVerticalAxisString()) + (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? -1 : 0) + (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 : 0);
        }
    }
}
