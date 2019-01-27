using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("I'm a teleporter");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider player) {
        Debug.Log("collide!");
        
        if (player.GetComponents<Teleportable>().Length > 0) {
            Debug.Log("let's go bitches!");
        }

    }
}
