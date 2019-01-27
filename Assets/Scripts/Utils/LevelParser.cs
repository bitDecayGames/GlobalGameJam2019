using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;
using Interactables;
using PlayerScripts;
using SuperTiled2Unity;
using UnityEngine;
using PolishElements;
using PlayerScripts;

public class LevelParser : MonoBehaviour {
    public GameObject playerTemplate;
    public GameObject buttonTemplate;
    public GameObject ladderTemplate;
    public Door doorTemplate;

    private int intruderLayer = -1;
    private int seekerLayer = -1;

    // Use this for initialization
    void Start() {
        intruderLayer = LayerMask.NameToLayer("Intruder");
        seekerLayer = LayerMask.NameToLayer("Seeker");

        var mapScript = FindObjectOfType<SuperMap>();
        if (mapScript == null) {
            throw new RuntimeException("There is no map in the scene");
        }

        var objLayers = mapScript.gameObject.GetComponentsInChildren<SuperObjectLayer>();
        if (objLayers == null || objLayers.Length < 1) {
            throw new RuntimeException("No object layers found in map object");
        }

        Debug.Log("Parse " + objLayers.Length + " layer(s)");
        foreach (var ol in objLayers) {
            Debug.Log("Got layer: " + ol.m_TiledName + ", attempting to parse");
            if (ol.m_TiledName == "Object Layer 1") {
                parseLevelObjects(ol);
            } else if (ol.m_TiledName == "Game Objects") {
                parseGameObjects(ol);
            } else {
                Debug.Log("Ignored parsing the layer: " + ol.m_TiledName);
            }
        }
    }

    void parseLevelObjects(SuperObjectLayer objLayer) {
        var objects = objLayer.gameObject.GetComponentsInChildren<SuperObject>();
        Debug.Log("Parsing " + objects.Length + " object(s)");
        foreach (var superObject in objects) {
            var custProps = superObject.gameObject.GetComponent<SuperCustomProperties>();
            foreach (var prop in custProps.m_Properties) {
                if (prop.m_Name == "ObjectName") {
//                    Debug.Log("Got: " + prop.m_Value);
                    switch (prop.m_Value) {
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
                            SpawnButton(superObject);
                            break;
                        default:
                            Debug.Log("Unrecognized 'ObjectName' " + prop.m_Value + " found in level map");
                            break;
                    }
                } else {
                    Debug.Log("Didn't understand prop name: " + prop.m_Name);
                }
            }
        }
    }

    void parseGameObjects(SuperObjectLayer objectLayer) {
        var objects = objectLayer.gameObject.GetComponentsInChildren<SuperObject>();
//        Debug.Log("Parsing " + objects.Length + " object(s)");
        foreach (var superObject in objects) {
            superObject.gameObject.AddComponent<Shaker>();
            superObject.gameObject.layer = LayerMask.NameToLayer("Interactable");
            
            var custProps = superObject.gameObject.GetComponent<SuperCustomProperties>();
            foreach (var prop in custProps.m_Properties) {
                if (prop.m_Name == "ObjectName") {
//                    Debug.Log("Got: " + prop.m_Value);
                    switch (prop.m_Value) {
                        case "horizontalHingeLeft":
                            SpawnDoor(superObject, Door.DoorType.HORIZONTAL_LEFT_HINGE);
                            break;
                        case "horizontalHingeRight":
                            SpawnDoor(superObject, Door.DoorType.HORIZONTAL_RIGHT_HINGE);
                            break;
                        case "verticalHingeTop":
                            SpawnDoor(superObject, Door.DoorType.VERTICAL_TOP_HINGE);
                            break;
                        case "verticalHingeBottom":
                            SpawnDoor(superObject, Door.DoorType.VERTICAL_BOTTOM_HINGE);
                            break;
                        default:
                            Debug.Log("Unrecognized 'ObjectName' " + prop.m_Value + " found in level map");
                            break;
                    }
                } else {
                    Debug.Log("Didn't understand prop name: " + prop.m_Name);
                }
            }
        }
    }

    void SpawnButton(SuperObject superObject) {
        var newButton = Instantiate(buttonTemplate, superObject.transform.position, Quaternion.identity);
        newButton.GetComponent<SpriteRenderer>().sprite = superObject.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        Destroy(superObject.gameObject);
    }

    void SpawnPlayer(SuperObject superObject, int playerNumber, bool isOwner) {
        var newPlayer = Instantiate(playerTemplate, superObject.transform.position, Quaternion.identity);
        newPlayer.GetComponent<PlayerSpriteSetter>().SetPlayerNumber(playerNumber);
        newPlayer.GetComponent<PlayerIdComponent>().setPlayerId(playerNumber);
        Destroy(superObject.gameObject);
        if (isOwner) {
            newPlayer.layer = seekerLayer;
            newPlayer.AddComponent<DashMovementController>();
            newPlayer.AddComponent<Teleportable>();
            newPlayer.AddComponent<KnockOutPuncher>();
        } else {
            newPlayer.layer = intruderLayer;
            newPlayer.AddComponent<SprintMovementController>();
            newPlayer.AddComponent<InvisibilityCloak>();
            newPlayer.AddComponent<Knockoutable>();
        }
    }

    void SpawnDoor(SuperObject superObject, Door.DoorType doorType) {
        var door = Instantiate(doorTemplate, superObject.transform.parent);
        door.transform.localScale = superObject.transform.localScale;
        door.transform.localPosition = superObject.transform.localPosition;
        door.transform.localRotation = superObject.transform.localRotation;
        door.SetDoorType(doorType);
        Destroy(superObject.gameObject);
    }

    // Update is called once per frame
    void Update() { }
}