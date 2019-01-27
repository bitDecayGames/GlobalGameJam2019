using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public class PrefabHolder : MonoBehaviour {

        public List<PrefabItem> items = new List<PrefabItem>();

        public PrefabItem Get(string name) {
            return items.Find(p => p.name == name);
        }
        
        [Serializable]
        public struct PrefabItem {
            public String name;
            public Transform prefab;
        }
    }
}