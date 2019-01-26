using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsControllerMapper : InputMapper {

    public WindowsControllerMapper(string playerID)
    {
        playerId = playerID;
    }

    override public bool InteractPressed()
    {
        //Any face buttons interact
        if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "0")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "1")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "2")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "3")))
        {
            return true;
        }

        return false;
    }

    override public bool RunPressed()
    {
        //Any bumper buttons pressed
        if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "4")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "5")))
        {
            return true;
        }

        return false;
    }
}
