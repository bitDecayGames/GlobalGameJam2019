using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Knockoutable : MonoBehaviour {
        private const float KNOCKOUT_SPIN = 500;
        
        public float KnockoutTime = 5f;
        
        private InvisibilityCloak cloak;
        private GamePadMovementController mover;

        private float time = 0;
        
        private float rotation = 0;
        
        void Start()
        {
            cloak = GetComponentInChildren<InvisibilityCloak>();
            if (!cloak) throw new Exception("Missing InvisibilityCloak on the Knockoutable object");
            mover = GetComponentInChildren<GamePadMovementController>();
            if (!mover) throw new Exception("Missing GamePadMovementController on the Knockoutable object");
        }

        void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                rotation += Time.deltaTime * KNOCKOUT_SPIN;
                SetRotation();
                if (time < 0)
                {
                    time = 0;
                    cloak.IsActive = true;
                    mover.IsFrozen = false;
                }
            }
        }

        public void KnockOut() {
            rotation = transform.localRotation.eulerAngles.z;
            time = KnockoutTime;
            cloak.IsActive = false;
            mover.IsFrozen = true;
        }

        private void SetRotation() {
            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}