using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

public class ObjectiveComponent : AbstractInteractable {
    public bool isComplete = false;

	void Start () {
        GameStateManager.getLocalReference().Register(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Interact(GameObject interactee)
    {
        //todo: check to see if this is the defender or attacker
        isComplete = true;
    }

    public override void Disconnect()
    {
        throw new System.NotImplementedException();
    }

    public override void Trigger()
    {
        throw new System.NotImplementedException();
    }
}
