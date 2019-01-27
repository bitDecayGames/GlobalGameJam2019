using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;
using SuperTiled2Unity;
using UnityEngine;
using PolishElements;

public class LevelParser : MonoBehaviour
{

    public GameObject playerTemplate;
    public GameObject ladderTemplate;

    // Use this for initialization
    void Start()
    {
        var mapScript = FindObjectOfType<SuperMap>();
        if (mapScript == null)
        {
            throw new RuntimeException("There is no map in the scene");
        }

        var objLayers = mapScript.gameObject.GetComponentsInChildren<SuperObjectLayer>();
        if (objLayers == null || objLayers.Length < 1)
        {
            throw new RuntimeException("No object layers found in map object");
        }

        foreach (var ol in objLayers)
        {
            if (ol.m_TiledName == "Object Layer 1")
            {
                parseLevelObjects(ol);
            }
            if (ol.m_TiledName == "Game Objects")
            {
                parseGameObjects(ol);
            }
        }
    }

    void parseLevelObjects(SuperObjectLayer objLayer)
    {
        Debug.Log("We are parsing an object layer");
        var objects = objLayer.gameObject.GetComponentsInChildren<SuperObject>();
        foreach (var superObject in objects)
        {
            var custProps = superObject.gameObject.GetComponent<SuperCustomProperties>();

            foreach (var prop in custProps.m_Properties)
            {
                if (prop.m_Name == "ObjectName")
                {
                    switch (prop.m_Value)
                    {
                        case "player1Spawn":
                            SpawnPlayer(superObject, 1, false);
                            break;
                        case "player2Spawn":
                            SpawnPlayer(superObject, 2, false);
                            break;
                        case "player3Spawn":
                            SpawnPlayer(superObject, 3, false);
                            break;
                        case "player4Spawn":
                            SpawnPlayer(superObject, 4, true);
                            break;
                        case "ladder":
                            var newLadder = Instantiate(ladderTemplate, superObject.transform.position, Quaternion.identity);
                            newLadder.GetComponent<SpriteRenderer>().sprite = superObject.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
                            Destroy(superObject.gameObject);
                            break;  
                        case "button":
                            break;
                        default:
                            throw new RuntimeException("Unrecognized 'ObjectName' " + prop.m_Value + " found");
                    }
                }
            }
        }
    }

    void parseGameObjects(SuperObjectLayer objectLayer)
    {
        Debug.Log("We are parsing an object layer");
        var objects = objectLayer.gameObject.GetComponentsInChildren<SuperObject>();
        foreach (var superObject in objects)
        {
            superObject.gameObject.AddComponent<Shaker>();
        }

    }

    void SpawnPlayer(SuperObject superObject, int playerNumber, bool isOwner)
    {
        var newPlayer = Instantiate(playerTemplate, superObject.transform.position, Quaternion.identity);
        newPlayer.GetComponent<PlayerSpriteSetter>().SetPlayerNumber(playerNumber);
        newPlayer.GetComponent<PlayerIdComponent>().setPlayerId(playerNumber);
        Destroy(superObject.gameObject);
        if (isOwner) 
        {
            newPlayer.AddComponent<Teleportable>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
