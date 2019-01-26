using PolishElements;
using UnityEngine;

namespace DebugScripts {
    public class DebugJiggle : MonoBehaviour {
        public float time = 0.1f;
        public float intensity = 0.1f;
        
        private Shaker shaker; 
        
        private void Start() {
            shaker = GetComponent<Shaker>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                shaker.Shake(time, intensity);
            }
        }
    }
}