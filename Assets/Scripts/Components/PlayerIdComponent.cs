using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdComponent : MonoBehaviour {

    public string playerId;

    public void setPlayerId(int PlayerId)
    {
        this.playerId = PlayerId.ToString();
    }

}
