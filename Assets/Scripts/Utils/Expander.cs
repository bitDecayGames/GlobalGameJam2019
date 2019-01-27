using UnityEngine;

public class Expander : MonoBehaviour {

	public float TimeToExpand = 1.5f;
	public float SizeToExpandTo = 10;
	
	private float time;
	private float timeRatio;
	
	// Use this for initialization
	void Start () {
		time = TimeToExpand;
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 0) {
			time -= Time.deltaTime;
			timeRatio = time / TimeToExpand;
			var size = SizeToExpandTo * (1 - timeRatio);
			transform.localScale = new Vector3(size, size);
			if (time < 0) {
				Destroy(gameObject);
			}
		}
	}
}
