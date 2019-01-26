using Interactables;
using UnityEngine;

namespace DebugScripts {
    public class DebugPressAndHoldInteractable : MonoBehaviour {

        private Door door;
        
        private void Start() {
            door = FindObjectOfType<Door>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.I)) {
                door.Interact(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                door.Disconnect();
            }

            if (Input.GetKeyDown(KeyCode.T)) {
                door.Trigger();
            }
        }
    }
}