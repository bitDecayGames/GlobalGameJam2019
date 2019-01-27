using System;
using UnityEngine;

namespace GameInput {
    public class InputController : MonoBehaviour {
        public InputMapper ControllerMapper { get; private set; }
        public float deadzone = 0.2f;

        void Start () {

            string OS = SystemInfo.operatingSystem;
            PlayerIdComponent playerIdComp = GetComponent<PlayerIdComponent>();
            string playerId;

            if (playerIdComp == null)
                throw new Exception("Could not find PlayerIdComponent.");

            playerId = playerIdComp.playerId;
            if (OS.Contains("Windows"))
            {
                ControllerMapper = new WindowsControllerMapper(playerId);
            }else if (OS.Contains("Mac"))
            {
                ControllerMapper = new MacControllerMapper(playerId);
            }
        }
    }
}
