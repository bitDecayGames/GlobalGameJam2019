using System;
using UnityEngine;

public class FadeOuter : MonoBehaviour {

	private SpriteRenderer sprite;
	public float TimeToFadeOut = 1.5f;
	
	private float time;
	private float timeRatio;
	
	// Use this for initialization
	void Start () {
		time = TimeToFadeOut;
		sprite = GetComponentInChildren<SpriteRenderer>();
		if (!sprite) throw new Exception("SpriteRenderer is missing from the FadeOuter");
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 0) {
			time -= Time.deltaTime;
			timeRatio = time / TimeToFadeOut;
			SetAlpha(timeRatio);
			if (time < 0) {
				Destroy(gameObject);
			}
		}
	}

	public void SetAlpha(float alpha) {
		var c = sprite.color;
		c.a = alpha;
		sprite.color = c;
	}
}
