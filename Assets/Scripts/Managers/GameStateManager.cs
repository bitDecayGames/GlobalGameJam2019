﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public int winThreshold = 2;
    public float gameTime = 60.0f;
    private const string gameObjectName = "GameStateManagerGameObject";
    private List<IObjective> winObjectives = new List<IObjective>();
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
            GameOver(); //Seeker Wins
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
            GameOver(); //Hiders Win
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

    private void GameOver()
    {

    }
}
