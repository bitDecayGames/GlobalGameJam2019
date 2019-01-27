using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitAndChange : MonoBehaviour
{

	public string SceneName;

	public float waitTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		waitTime -= Time.deltaTime;
		if (waitTime <= 0)
		{
			SceneManager.LoadScene(SceneName);
		}
	}
}
