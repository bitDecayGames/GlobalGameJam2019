using GameInput;
using Interactables;
using UnityEngine;

public class InteractableInteracter : MonoBehaviour
{
    public string InteractableObjectName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject.Find(InteractableObjectName).GetComponent<AbstractInteractable>().Interact(GetComponent<InputController>());
        }
    }
}