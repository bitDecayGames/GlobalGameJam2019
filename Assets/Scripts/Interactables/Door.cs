using System;
using UnityEngine;

namespace Interactables {
	public class Door : MonoBehaviour {

		private bool _isOpen;
		public bool IsOpen {
			get { return _isOpen; }
		}
	
		private Rigidbody2D body;
		private SpriteRenderer sprite;
	
		void Start () {
			body = GetComponentInChildren<Rigidbody2D>();
			if (!body) throw new Exception("RigidBody2D is missing on this object");
		
			sprite = GetComponentInChildren<SpriteRenderer>();
			if (!sprite) throw new Exception("Sprite is missing on this object");
		}

		public void SetDoorType(DoorType doorType) {
			var rot = 0f;
			switch (doorType) {
				case DoorType.HORIZONTAL_LEFT_HINGE:
					// do nothing
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
				sprite.transform.parent.localRotation = Quaternion.Euler(0, 0, 90);
				body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
				_isOpen = true;
			}
		}

		public void Close() {
			if (_isOpen) {
				sprite.transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
				body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
				_isOpen = false;
			}
		}

		public enum DoorType {
			HORIZONTAL_LEFT_HINGE,
			HORIZONTAL_RIGHT_HINGE,
			VERTICAL_TOP_HINGE,
			VERTICAL_BOTTOM_HINGE
		}
	}
}
