using Interactables;
using UnityEngine;

namespace DebugScripts {
	public class DebugDoor : MonoBehaviour {

		private Door door;
	
		// Use this for initialization
		void Start () {
			door = FindObjectOfType<Door>();
			
			door.SetDoorType(Door.DoorType.VERTICAL_TOP_HINGE);
		}
	
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (door.IsOpen) door.Close();
				else door.Open();
			}
		}
	}
}
