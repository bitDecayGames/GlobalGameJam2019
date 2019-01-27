using System;
using UnityEngine;

namespace GameInput {
    public class InputController : MonoBehaviour {
        public InputMapper ControllerMapper { get; private set; }
        public float deadzone = 0.2f;

        void Start () {
            PlayerIdComponent playerIdComp = GetComponent<PlayerIdComponent>();
            if (playerIdComp == null) throw new Exception("Could not find PlayerIdComponent.");
            string playerId = playerIdComp.playerId;

            ControllerMapper = InputMapperFactory.BuildInputMapper(playerId);
        }
    }
}
