using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputMapper  {
    public string playerId;

    abstract public bool InteractPressed();

    abstract public bool RunPressed();

    string BuildHorizontalAxisString()
    {
        return "Horizontal_" + playerId;
    }

    string BuildVerticalAxisString()
    {
        return "Vertical_" + playerId;
    }

    public float GetHorizontalMovement()
    {
        if(playerId == 1.ToString())
            Debug.Log(BuildHorizontalAxisString());

        return Input.GetAxis(BuildHorizontalAxisString());
    }

    public float GetVerticalMovement()
    {
        if (playerId == 1.ToString())
            Debug.Log(BuildVerticalAxisString());
        return Input.GetAxis(BuildVerticalAxisString());
    }
}
