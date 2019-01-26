using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacControllerMapper : InputMapper
{
    public MacControllerMapper(string playerID)
    {
        playerId = playerID;
    }

    override public bool InteractPressed()
    {
        //Any face buttons interact
        if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "16")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "17")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "18")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "19")))
        {
            return true;
        }

        return false;
    }

    override public bool RunPressed()
    {
        //Any bumper buttons pressed
        if (Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "13")) ||
            Input.GetKeyDown(string.Format("joystick {0} button {1}", playerId, "14")))
        {
            return true;
        }

        return false;
    }
}
