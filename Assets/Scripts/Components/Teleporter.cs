using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public bool Active = true;

    public Transform TeleporterIndicatorPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!this.Active)
        {
            return;
        }

        var player = col.gameObject;
        if (player.GetComponents<Teleportable>().Length > 0) 
        {
            var allTeleporters = GameObject.FindGameObjectsWithTag("teleporter");
            if (allTeleporters.Length % 2 != 0) {
                throw new System.Exception("There must be an even number of transporters");
            }
            int i = 0;
            GameObject nextTeleporter = null;
            foreach(var otherTeleporter in allTeleporters)
            {       
                if (otherTeleporter == this.gameObject)
                {
                    nextTeleporter = GetNextTeleporter(allTeleporters, i);
                    break;
                }         
                i++;
            }
            nextTeleporter.GetComponent<Teleporter>().Active = false;
            var offset = nextTeleporter.GetComponent<CircleCollider2D>().offset;
            var newOffset = new Vector3(offset.x, offset.y, player.transform.position.z);
            player.transform.position = nextTeleporter.transform.position + newOffset;
            var indicator = Instantiate(TeleporterIndicatorPrefab);
            indicator.position = player.transform.position;
        } 
    }

    private GameObject GetNextTeleporter(GameObject[] allTeleporters, int i)
    {
        var mapTeleporters = new Dictionary<int, int> {
            { 0, 4 },
            { 4, 0 },
            { 3, 2 },
            { 2, 3 },
            { 1, 5 },
            { 5, 1 }
        };

        int nextTeleporterIndex;
        if (mapTeleporters.ContainsKey(i)) {
            nextTeleporterIndex = mapTeleporters[i];
        }
        else {
            throw new System.Exception("I don't know where this teleporter goes");
        }

        var nextTeleporter = allTeleporters[nextTeleporterIndex];
        return nextTeleporter;
    }
    void OnTriggerExit2D(Collider2D col) {
        var player = col.gameObject;
        if (player.GetComponents<Teleportable>().Length > 0) 
        {
            this.Active = true;
        }
    }



}
