using UnityEngine;

namespace Interactables {
    public abstract class AbstractInteractable : MonoBehaviour {
        

        protected bool _isInteracting;

        public bool IsInteracting {
            get { return _isInteracting; }
        }

        protected GameObject _interactee;

        public GameObject Interactee {
            get { return _interactee; }
        }

        public abstract void Interact(GameObject interactee);
        public abstract void Disconnect();
        public abstract void Trigger();
        
        

        protected virtual void OnInteract() {
            
        }
        
        protected virtual void OnDisconnect() {
            
        } 
        
        protected virtual void OnTrigger() {
            
        }
    }
}