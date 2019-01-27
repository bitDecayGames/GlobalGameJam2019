using GameInput;
using Interactables;
using UnityEngine;

namespace DebugScripts {
    public class DebugPressAndHoldInteractable : MonoBehaviour {

        private Door door;
        private InputController input;
        
        private void Start() {
            door = FindObjectOfType<Door>();

            var playerIdComp = gameObject.AddComponent<PlayerIdComponent>();
            playerIdComp.setPlayerId(1);
            
            input = gameObject.AddComponent<InputController>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.I)) {
                door.Interact(input);
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