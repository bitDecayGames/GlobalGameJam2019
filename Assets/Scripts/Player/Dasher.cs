using System;
using UnityEngine;

namespace Player {
	public class Dasher : MonoBehaviour {

		private float intensity = 50f;
		private float refreshRate = 2f;
		private int maxNumberOfDashes = 3;

		private int CurrentDashes = 0;
		private float time = 0;
	
		private Rigidbody2D body;

		private LineRenderer lineRenderer;

		public float Intensity {
			get { return intensity; }
			set { intensity = value; }
		}

		public float RefreshRate {
			get { return refreshRate; }
			set { refreshRate = value; }
		}

		public int MaxNumberOfDashes {
			get { return maxNumberOfDashes; }
			set { maxNumberOfDashes = value; }
		}

		// Use this for initialization
		void Start () {
			body = GetComponent<Rigidbody2D>();
			if (!body) throw new Exception("RigidBody2D could not be found on the Dasher object");

//			lineRenderer = gameObject.AddComponent<LineRenderer>();

			CurrentDashes = maxNumberOfDashes;
		}
	
		// Update is called once per frame
		void Update () {
			if (time > 0) {
				time -= Time.deltaTime;
				if (time < 0) Refresh();
			}
		
//			DrawDebugLine();
		}

		public void Refresh() {
			CurrentDashes = maxNumberOfDashes;
			time = 0;
		}

		public void Dash() {
			if (CurrentDashes > 0) {
				var vel = body.velocity;
				var velMag = vel.magnitude;
				if (velMag < 0.01f && velMag > -0.01f) return; // if the player isn't moving, they can't dash
				var dir = vel.normalized;
				dir *= intensity;
				body.AddForce(dir, ForceMode2D.Impulse);

				CurrentDashes--;
				time = refreshRate;
			}
		}

		private void DrawDebugLine() {
			if (lineRenderer != null) {
				var pos = transform.position;
				var vel = body.velocity;
				lineRenderer.SetPositions(new[] {pos, pos + new Vector3(vel.x, vel.y)});
			}
		}
	}
}
