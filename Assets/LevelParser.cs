﻿using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;
using SuperTiled2Unity;
using UnityEngine;

public class LevelParser : MonoBehaviour
{

  public GameObject playerTemplate;

  // Use this for initialization
  void Start ()
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
    }
  }

  void parseLevelObjects(SuperObjectLayer objLayer)
  {
    Debug.Log("We are parsing an object layer");
    var objects = objLayer.gameObject.GetComponentsInChildren<SuperObject>();
        int playerId = 1;
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
              //break;
            case "player2Spawn":
              //break;
            case "player3Spawn":
              //break;
            case "player4Spawn":
              //break;
            case "button":
              Debug.Log("We maky da player frum button");
              SpawnPlayer(superObject, 3);
              break;
            default:
              throw new RuntimeException("Unrecognized 'ObjectName' " + prop.m_Name + " found");
          }
        }
      }
    }
  }

  void SpawnPlayer(SuperObject superObject, int playerNumber)
  {
    var newPlayer = Instantiate(playerTemplate, superObject.transform.position, Quaternion.identity);
    newPlayer.GetComponent<PlayerSpriteSetter>().SetPlayerNumber(playerNumber);
  }

  // Update is called once per frame
  void Update () {

  }
}
