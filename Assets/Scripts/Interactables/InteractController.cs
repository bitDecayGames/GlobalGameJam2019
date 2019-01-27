using System;
using GameInput;
using Interactables;
using PolishElements;
using UnityEngine;

public class InteractController : MonoBehaviour {
    private InputController input;
    private AbstractInteractable interactable;
    private Shaker shaker;

    private int interactableLayer = -1;

    private void Start() {
        input = GetComponentInChildren<InputController>();
        if (!input) throw new Exception("Missing InputController on the InteractController object");

        interactableLayer = LayerMask.NameToLayer("Interactable");
    }

    private void OnTriggerEnter2D(Collider2D potentialInteractable) {
        interactable = potentialInteractable.gameObject.GetComponentInChildren<AbstractInteractable>();
        if (!interactable) interactable = potentialInteractable.gameObject.GetComponentInParent<AbstractInteractable>();

        ShakeShakers(potentialInteractable.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D potentialInteractable) {
        ShakeShakers(potentialInteractable.gameObject);
    }

    private void OnTriggerExit2D(Collider2D potentialInteractable) {
        if (interactable) {
            interactable.Disconnect();
            interactable = null;
        }
    }

    void Update() {
        if (interactable) {
            if (input.ControllerMapper.InteractPressed()) {
                interactable.Interact(input);
            }
        }
    }
    
    private void ShakeShakers(GameObject potentialInteractable) {
        shaker = FindShaker(potentialInteractable.transform, interactableLayer);
        if (shaker) shaker.Shake();
    }

    private Shaker FindShaker(Transform go, int layer) {
        if (!go) return null;
        if (go.gameObject.layer == layer && (!go.parent || go.parent.gameObject.layer != layer)) {
            return go.GetComponentInChildren<Shaker>();
        }
        return FindShaker(go.parent, layer);
    }
}