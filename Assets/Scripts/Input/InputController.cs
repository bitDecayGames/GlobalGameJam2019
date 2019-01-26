﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    InputMapper ControllerMapper;

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
    }   
}
