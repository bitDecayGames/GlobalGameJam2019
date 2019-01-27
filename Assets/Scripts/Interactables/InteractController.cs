using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using Interactables;

public class InteractController : MonoBehaviour {

    GameObject collidee;

    private void OnTriggerEnter2D(Collider2D other)
    {
        collidee = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidee = null;
    }
    	
	void Update () {
		if(collidee != null)
        {
            if (this.GetComponent<InputController>().ControllerMapper.InteractPressed())
            {
                collidee.GetComponent<AbstractInteractable>().Trigger();
            }
        }
	}
}
