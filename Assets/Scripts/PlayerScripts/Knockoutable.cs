using System;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace PlayerScripts
{
    public class Knockoutable : MonoBehaviour {
        private const float KNOCKOUT_SPIN = 500;
        
        public float KnockoutTime = 5f;
        
        private InvisibilityCloak cloak;
        private GamePadMovementController mover;
        private Transform KnockoutIndicatorPrefab;

        private float time = 0;
        
        private float rotation = 0;

        private Random rnd = new Random();
        
        void Start()
        {
            cloak = GetComponentInChildren<InvisibilityCloak>();
            if (!cloak) throw new Exception("Missing InvisibilityCloak on the Knockoutable object");
            mover = GetComponentInChildren<GamePadMovementController>();
            if (!mover) throw new Exception("Missing GamePadMovementController on the Knockoutable object");

            var prefabHolder = FindObjectOfType<PrefabHolder>();
            if (!prefabHolder) throw new Exception("You are missing a PrefabHolder object in your scene, sorry");
            KnockoutIndicatorPrefab = prefabHolder.Get("KnockoutIndicator").prefab;
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

        public void KnockOut()
        {
            FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Smack);
            TimeFreezer.GetLocalReference().TriggerHitStun();
            rotation = transform.localRotation.eulerAngles.z;
            time = KnockoutTime;
            cloak.IsActive = false;
            mover.IsFrozen = true;
            var indicator = Instantiate(KnockoutIndicatorPrefab);
            indicator.position = transform.position;
            Respawn();
        }

        private void SetRotation() {
            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }

        private void Respawn() {
            var respawns = GameObject.FindGameObjectsWithTag(Tags.Respawn);
            if (respawns.Length > 0) {
                transform.position = respawns[rnd.Next(0, respawns.Length)].transform.position;
                var indicator = Instantiate(KnockoutIndicatorPrefab);
                indicator.position = transform.position;
            } else Debug.LogError("Failed to find any objects with the Respawn tag");
        }
    }
}