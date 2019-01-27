using UnityEngine;

public class TimeFreezer : MonoBehaviour
{
    public bool FreezeTime;

    private void Update()
    {
        if (FreezeTime)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}