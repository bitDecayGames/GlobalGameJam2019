using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;
using UnityEngine;

public class PlayerSpriteSetter : MonoBehaviour
{
    public List<Sprite> playerSpriteChoices;

    public void SetPlayerNumber(int n)
    {
        if (n < 1 || n > playerSpriteChoices.Count)
        {
            throw new RuntimeException("Player number " + n + " is invalid");
        }
        
        GetComponent<SpriteRenderer>().sprite = playerSpriteChoices[n-1];
    }

    private void Start()
    {
        SetPlayerNumber(3);
    }
}
