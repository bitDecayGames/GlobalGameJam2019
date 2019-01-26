using Player;
using UnityEngine;

namespace DebugScripts {
    public class DebugDasher : MonoBehaviour {

        private Dasher dasher;
        private Rigidbody2D body;
        private Vector3 initialPosition;
        
        void Start() {
            dasher = GetComponent<Dasher>();
            body = GetComponent<Rigidbody2D>();

            initialPosition = transform.position;
        }
        
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) dasher.Dash();

            if (Input.GetKeyDown(KeyCode.R)) {
                transform.position = initialPosition;
                body.velocity = new Vector2(50f, 0);
            }
        }
    }
}