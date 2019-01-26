using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    InputMapper ControllerMapper;
    public float speed = 0.2f;
    public float turnSpeed = 1.0f;
    public float deadzone = 0.2f;

    void Start () {

        string OS = SystemInfo.operatingSystem;
        PlayerIdComponent playerIdComp = GetComponent<PlayerIdComponent>();
        string playerId;

        if (playerIdComp == null)
            throw new System.Exception("Could not find PlayerIdComponent.");

        playerId = playerIdComp.playerId;
        //todo find player ID
        if (OS.Contains("Windows"))
        {
            ControllerMapper = new WindowsControllerMapper(playerId);
        }else if (OS.Contains("Mac"))
        {
            ControllerMapper = new MacControllerMapper(playerId);
        }
    }
	
	void Update () {
        if (ControllerMapper.InteractPressed())
        {
            //Interact with thing in front of you
            Debug.Log("InteractPressed");
        }

        if (ControllerMapper.RunPressed())
        {
            //RUN!
            Debug.Log("RunPressed");
        }
        
        float moveLR = ControllerMapper.GetHorizontalMovement();
        float moveUD = ControllerMapper.GetVerticalMovement();

        float newFacing = FindDegree(moveLR, moveUD);
        var newPosition = new Vector3(moveLR, moveUD);
        if(newPosition.magnitude > deadzone)
        {
            Debug.Log("magnitude: " + newPosition);
            transform.rotation = Quaternion.AngleAxis(newFacing, Vector3.forward);
            transform.Translate(newPosition.magnitude * speed * Time.deltaTime, 0, 0);
        }

    }

    public static float FindDegree(float x, float y)
    {
        float value = (Mathf.Atan2(y,x) / (float)Math.PI) * 180f;
        if (value < 0) value += 360f;

        return value;
    }
}
