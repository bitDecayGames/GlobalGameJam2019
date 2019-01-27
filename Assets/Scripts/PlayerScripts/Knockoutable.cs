using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Knockoutable : MonoBehaviour
    {
        public float KnockoutTime = 5f;
        
        private InvisibilityCloak cloak;
        private GamePadMovementController mover;

        private float time = 0;
        
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
                if (time < 0)
                {
                    time = 0;
                    cloak.IsActive = true;
                    mover.IsFrozen = false;
                }
            }
        }

        public void KnockOut()
        {
            time = KnockoutTime;
            cloak.IsActive = false;
            mover.IsFrozen = true;
        }
    }
}