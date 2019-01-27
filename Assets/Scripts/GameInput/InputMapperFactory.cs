using System;
using UnityEngine;

namespace GameInput {
    public static class InputMapperFactory {
        public static InputMapper BuildInputMapper(string playerId) {
            var operatingSystem = SystemInfo.operatingSystem;
            if (operatingSystem.Contains("Windows")) return new WindowsControllerMapper(playerId);
            if (operatingSystem.Contains("Mac")) return new MacControllerMapper(playerId);
            throw new Exception("Failed to figure out what operating system this is: " + operatingSystem);
        }
    }
}