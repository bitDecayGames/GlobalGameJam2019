using UnityEngine;

namespace PolishElements {
	public class Shaker : MonoBehaviour {
		
		private bool isJiggling = false;
		private float time = 0;
		private Vector3 curPos;
		private float jitterAmount;
		private Random rnd = new Random();

		void Update() {
			if (time > 0) {
				time -= Time.deltaTime;
				var pos = Random.insideUnitCircle;
				pos *= jitterAmount;
				transform.position = curPos + new Vector3(pos.x, pos.y);
			} else if (isJiggling) {
				isJiggling = false;
				transform.position = curPos;
			}
		}
		
		
		public void Shake(float seconds = 0.1f, float intensity = 0.1f) {
			curPos = transform.position;
			isJiggling = true;
			time = seconds;
			jitterAmount = intensity;
		}
	}
}
