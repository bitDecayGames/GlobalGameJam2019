using System;
using GameInput;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

namespace Interactables {
	public class RedDoor : PrecisionButtonsInteractable {

		private bool _isOpen;
		public bool IsOpen {
			get { return _isOpen; }
		}
	
		private Collider2D body;
		private SpriteRenderer sprite;
		private Blinker blinker;
		private int seekerLayer = -1;
		private int intruderLayer = -1;
	
		void Start () {
			body = GetComponentInChildren<Collider2D>();
			if (!body) throw new Exception("Collider2D is missing on this object");
			var passthrough = body.gameObject.AddComponent<ColliderPassthrough>();
			passthrough.OnCollisionEnter2DEvent.AddListener(OnCollisionEnter2D);
			passthrough.OnCollisionExit2DEvent.AddListener(OnCollisionExit2D);
			passthrough.OnTriggerEnter2DEvent.AddListener(OnTriggerEnter2D);
			passthrough.OnTriggerExit2DEvent.AddListener(OnTriggerExit2D);
		
			sprite = GetComponentInChildren<SpriteRenderer>();
			if (!sprite) throw new Exception("Sprite is missing on this object");

			seekerLayer = LayerMask.NameToLayer("Seeker");
			intruderLayer = LayerMask.NameToLayer("Intruder");
		}

		protected new void Update() {
			base.Update();
			// TODO: MW this is a debug hack
			if (Input.GetKeyDown(KeyCode.I)) {
				Trigger();
			}
		}

		// TODO: MW this method is extremely hard-coded, messing with the doors at all will require this logic to be redone
		public void SetDoorType(Door.DoorType doorType) {
			var rot = 0f;
			Vector3 pos;
			switch (doorType) {
				case Door.DoorType.HORIZONTAL_LEFT_HINGE:
					pos = transform.localPosition;
					pos.y += 0.05f;
					transform.localPosition = pos;
					
					break; 
				case Door.DoorType.HORIZONTAL_RIGHT_HINGE:
					rot = 180f;
					pos = transform.localPosition;
					pos.x += 0.32f;
					pos.y += 0.05f;
					transform.localPosition = pos;
					break;
				case Door.DoorType.VERTICAL_BOTTOM_HINGE:
					rot = 90;
					pos = transform.localPosition;
					pos.x += 0.1f;
					transform.localPosition = pos;
					break;
				case Door.DoorType.VERTICAL_TOP_HINGE:
					rot = -90;
					pos = transform.localPosition;
					pos.y += 0.32f;
					pos.x += 0.05f;
					transform.localPosition = pos;
					break;
			}
			transform.rotation = Quaternion.Euler(0, 0, rot);
		}

		public void Open() {
			if (!_isOpen) {
				sprite.transform.parent.localRotation = Quaternion.Euler(0, 0, -80);
				body.isTrigger = true;
				_isOpen = true;
			}
		}

		public void Close() {
			if (_isOpen)
			{
				_isUnlocked = false;
				sprite.transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
				body.isTrigger = false;
				_isOpen = false;
			}
		}

		public override void Trigger() {
			base.Trigger();
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
			if (other.gameObject.layer == seekerLayer) {
				Open();
			}
		}

		private void OnCollisionExit2D(Collision2D other) {
			if (other.gameObject.layer == seekerLayer) {
				Close();
			}
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.layer == seekerLayer) {
				Open();
			}
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (other.gameObject.layer == seekerLayer) {
				Close();
			}
		}
	}
}
