using UnityEngine;

public class GameObjectWiggler : MonoBehaviour
{
    private bool moveRight;
    private float timePassed;
    private void Update()
    {
        timePassed += Time.deltaTime;
        
        if (moveRight)
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        
        if (timePassed > 2)
        {
            timePassed = 0;
            moveRight = !moveRight;
        }
    }
}