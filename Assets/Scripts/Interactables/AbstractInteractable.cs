using System;
using GameInput;
using Interactables.Progress;
using UnityEngine;

namespace Interactables {
    public abstract class AbstractInteractable : MonoBehaviour {

        protected bool _isInteracting;

        public float AdditionalVerticalOffset;
        public GameObject ProgressBarGameObject;
        public ProgressBarController ProgressBarController;
        public float SuccessThreshold;

        public bool IsInteracting {
            get { return _isInteracting; }
        }

        protected InputController _interactee;

        public InputController Interactee {
            get { return _interactee; }
        }

        public abstract void Interact(InputController interactee);
        public abstract void Disconnect();
        public abstract void Trigger();
        
        

        protected virtual void OnInteract()
        {
            if (ProgressBarGameObject == null)
            {
                ProgressBarGameObject = Instantiate(Resources.Load("ProgressBar") as GameObject);
                if (ProgressBarGameObject == null) throw new Exception("Resources.Load failed for progress bar");
                ProgressBarController = ProgressBarGameObject.GetComponent<ProgressBarController>();
                if (ProgressBarController == null) throw new Exception("Could not find progress bar controller");
                ProgressBarController.SetSuccessesRequired(SuccessThreshold);   
            }
            ProgressBarController.Activate(_interactee.transform.position + Vector3.up * AdditionalVerticalOffset);
        }
        
        protected virtual void OnDisconnect() {
            ProgressBarController.Reset();
        } 
        
        protected virtual void OnTrigger() {
            ProgressBarController.Complete();
        }
    }
}