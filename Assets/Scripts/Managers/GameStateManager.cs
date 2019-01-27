﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using Utils;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    public int winThreshold = 4;
    public bool isGameOver = false;
    public float gameTime = 30.0f;
    private const string gameObjectName = "GameStateManagerGameObject";
    private List<IObjective> winObjectives = new List<IObjective>();
    public HudController HUD;
    [SerializeField]
    private int registeredGameObjects = 0;
    [SerializeField]
    private int activatedGameObjects = 0;
        
    void Update () {        
        int completedObjectives = 0;
        activatedGameObjects = 0;
        gameTime -= Time.deltaTime;

        if (gameTime < 0)
        {
            GameOver(true); //Seeker Wins
        }

        foreach(IObjective obj in winObjectives)
        {
            if (obj.isComplete())
            {
                completedObjectives++;
                activatedGameObjects++;
            }
        }

        if(completedObjectives >= winThreshold)
        {
            GameOver(false); //Hiders Win
        }

        HUD.SetLabel(completedObjectives,winThreshold);
        if (!isGameOver) {
            HUD.SetTimer(gameTime);
        }
        else {
            if (gameTime < -5) {
                SceneManager.LoadScene("Logan");
            }
        }
    }

    public void Register(IObjective comp)
    {
        winObjectives.Add(comp);
        registeredGameObjects = winObjectives.Count;
    }

    public static GameStateManager getLocalReference()
    {
        GameObject gameObj = GameObject.Find(gameObjectName);
        if (gameObj == null)
            throw new System.Exception("Game State Manager game object reference not found.");
            
        return gameObj.GetComponent<GameStateManager>();
    }
    
    private void GameOver(bool isOwnerTheWinner)
    {
        if (isGameOver) {
            return;
        }

        //HUD.SetTimer(0);
        var players = GameObject.FindGameObjectsWithTag("player");
        foreach (var player in players) {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponent<GamePadMovementController>().IsFrozen = true;
        }
        this.isGameOver = true;
        var blinkers = GameObject.FindGameObjectsWithTag("blinker");
        foreach (var blinker in blinkers) {
            blinker.AddComponent<CanvasBlinker>();
        }

    }
}
