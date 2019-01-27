﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public bool Active = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!this.Active)
        {
            return;
        }

        var player = col.gameObject;
        Debug.Log("Oh! Don't touch me there");
        if (player.GetComponents<Teleportable>().Length > 0) 
        {
            Debug.Log("Get your hands off my PENIS!");
            var allTeleporters = GameObject.FindGameObjectsWithTag("teleporter");
            if (allTeleporters.Length % 2 != 0) {
                throw new System.Exception("There must be an even number of transporters");
            }
            int i = 0;
            GameObject nextTeleporter = null;
            foreach(var otherTeleporter in allTeleporters)
            {       
                if (otherTeleporter == this.gameObject)
                {
                    Debug.Log("I found myself!");
                    int nextTeleporterIndex = allTeleporters.Length - i - 1;
                    nextTeleporter = allTeleporters[nextTeleporterIndex];
                }         
                i++;
            }
            nextTeleporter.GetComponent<Teleporter>().Active = false;
            var offset = nextTeleporter.GetComponent<CircleCollider2D>().offset;
            var newOffset = new Vector3(offset.x, offset.y, player.transform.position.z);
            player.transform.position = nextTeleporter.transform.position + newOffset; 
        } 
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("exit!");
        var player = col.gameObject;
        if (player.GetComponents<Teleportable>().Length > 0) 
        {
            this.Active = true;
        }
    }



}
