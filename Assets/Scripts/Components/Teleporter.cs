using System.Collections;
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
            int i = 0;
            GameObject nextTeleporter = null;
            foreach(var otherTeleporter in allTeleporters)
            {       
                if (otherTeleporter == this.gameObject)
                {
                    Debug.Log("I found myself!");
                    int nextTeleporterIndex = i + 1;
                    if (nextTeleporterIndex > allTeleporters.Length - 1) 
                    {
                        nextTeleporterIndex = 0;
                    }
                    nextTeleporter = allTeleporters[nextTeleporterIndex];
                }         
                i++;
            }
            nextTeleporter.GetComponent<Teleporter>().Active = false;
            player.transform.position = nextTeleporter.transform.position;
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
