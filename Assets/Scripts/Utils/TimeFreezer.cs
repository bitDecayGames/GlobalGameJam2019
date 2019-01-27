using System;
using System.Collections;
using UnityEngine;

public class TimeFreezer : MonoBehaviour
{
    private static string timeFreezerGameObjectName = "TimeFreezerManager";

    public static TimeFreezer GetLocalReference()
    {
        GameObject timeFreezerGameObject = GameObject.Find(timeFreezerGameObjectName);
        if (timeFreezerGameObject == null)
        {
            throw new Exception(String.Format("Unable to find {0}", timeFreezerGameObjectName));
        }

        return timeFreezerGameObject.GetComponent<TimeFreezer>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PauseBriefly());
        }
    }

    public void TriggerHitStun()
    {
        StartCoroutine(PauseBriefly());
    }
    
    IEnumerator PauseBriefly() 
    {
        Time.timeScale = 0;   
        yield return new WaitForSecondsRealtime(.15f);
        Time.timeScale = 1;     
    }
}