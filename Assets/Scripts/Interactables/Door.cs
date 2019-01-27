using System;
using GameInput;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

namespace Interactables {
	public class Door : ButtonMashInteractable {

		private bool _isOpen;
		public bool IsOpen {
			get { return _isOpen; }
		}
	
		private Collider2D body;
		private SpriteRenderer sprite;
		private Blinker blinker;
		private int seekerLayer = -1;
		
	
		void Start () {
			body = GetComponentInChildren<Collider2D>();
			if (!body) throw new Exception("Collider2D is missing on this object");
			var passthrough = body.gameObject.AddComponent<ColliderPassthrough>();
			passthrough.OnCollisionEnter2DEvent.AddListener(OnCollisionEnter2D);
			passthrough.OnCollisionExit2DEvent.AddListener(OnCollisionExit2D);
		
			sprite = GetComponentInChildren<SpriteRenderer>();
			if (!sprite) throw new Exception("Sprite is missing on this object");

			seekerLayer = LayerMask.NameToLayer("Seeker");
		}

		protected new void Update() {
			base.Update();
			// TODO: MW this is a debug hack
			if (Input.GetKeyDown(KeyCode.I)) {
				Trigger();
			}
		}

		public void SetDoorType(DoorType doorType) {
			var rot = 0f;
			switch (doorType) {
				case DoorType.HORIZONTAL_LEFT_HINGE:
					// This is to move the door sprite into the correct position for rotation stuff
//					var pos = sprite.transform.parent.localPosition;
//					pos.y = 0;
//					sprite.transform.parent.localPosition = pos;
					
					break; 
				case DoorType.HORIZONTAL_RIGHT_HINGE:
					rot = 180f;
					break;
				case DoorType.VERTICAL_BOTTOM_HINGE:
					rot = 90;
					break;
				case DoorType.VERTICAL_TOP_HINGE:
					rot = -90;
					break;
			}
			transform.rotation = Quaternion.Euler(0, 0, rot);
		}

		public void Open() {
			if (!_isOpen) {
				sprite.transform.parent.parent.localRotation = Quaternion.Euler(0, 0, 90);
				body.isTrigger = true;
				_isOpen = true;
			}
		}

		public void Close() {
			if (_isOpen) {
				sprite.transform.parent.parent.localRotation = Quaternion.Euler(0, 0, 0);
				body.isTrigger = false;
				_isOpen = false;
			}
		}

		public enum DoorType {
			HORIZONTAL_LEFT_HINGE,
			HORIZONTAL_RIGHT_HINGE,
			VERTICAL_TOP_HINGE,
			VERTICAL_BOTTOM_HINGE
		}

		public override void Trigger() {
			if (_isOpen) Close();
			else Open();
			OnTrigger();
		}

		protected override void OnInteract() {
			blinker = gameObject.AddComponent<Blinker>();
		}

		protected override void OnDisconnect() {
			if (blinker) Destroy(blinker);
		}

		protected override void OnTrigger() {
			base.OnTrigger();
			if (blinker) Destroy(blinker);
		}

		private void OnCollisionEnter2D(Collision2D other) {
			Debug.Log("Door entered collision: " + other.gameObject.layer);
			if (other.gameObject.layer == seekerLayer) {
				Open();
			}
		}

		private void OnCollisionExit2D(Collision2D other) {
			Debug.Log("Door exit collision: " + other.gameObject.layer);
			if (other.gameObject.layer == seekerLayer) {
				Close();
			}
		}
	}
}
