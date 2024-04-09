using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System;
using System.Reflection;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]

public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab;
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>();
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>();
    public Dictionary<string, bool> playerisDead = new Dictionary<string, bool>();

    public ScoreContainer scoreContainer;
    public isDeadContainer isDeadContainer;

    public void AddPlayerData(string playerID, GameObject player)
    {
        playerObject[playerID] = player;
    }

    public GameObject ReturnPlayerData(string playerID)
    {
        return playerObject[playerID];
    }

    public void AddPlayerColor(string playerID, Color color)
    {
        playerColor[playerID] = color;
    }

    public Color ReturnPlayerColor(string playerID)
    {
        if (playerColor.ContainsKey(playerID))
            return playerColor[playerID];
        else
            return Color.white;
    }


    public Color[] ReturnPlayerColorArray()
    {
        Color[] colorsArray = new Color[playerColor.Count];
        int index = 0;

        foreach (var color in playerColor.Values)
        {
            colorsArray[index] = color;
            index++;
        }

        return colorsArray;
    }

    public void AddScore(int score)
    {
        scoreContainer.AddScore(score);

        Debug.Log("Score: " + scoreContainer.ReturnScore().ToString());
    }

    public int ReturnScore()
    {
        return scoreContainer.ReturnScore();
    }

    public void ResetScore()
    {
        scoreContainer.ResetScore();
    }
    
    public void AddPlayerisDead(string playerID, bool isDead)
    {
        playerisDead[playerID] = isDead;
    }

    public bool ReturnPlayerisDead(string playerID)
    {
        if(playerisDead.ContainsKey(playerID))
            return playerisDead[playerID];
        else
        {
            AddPlayerisDead(playerID, false);
            return false;
        }
    }

    public void ResetisDead(int PlayerCount)
    {
        isDeadContainer.ResetContainer(PlayerCount);
    }

    public bool ReturnPlayerisDeadAll()
    {
        foreach (var kvp in playerisDead)
        {
            if (!kvp.Value)
            {
                return false;
            }
        }
        return true;
    }
    
    public void ResetContainer(string playerID)
    {
        if (playerObject.ContainsKey(playerID))
            playerObject.Remove(playerID);
        if (playerColor.ContainsKey(playerID))
            playerColor.Remove(playerID);
        //  if (playerScore.ContainsKey(playerID))
        //       playerScore.Remove(playerID);
        if (playerisDead.ContainsKey(playerID))
            playerisDead.Remove(playerID);
    }

    // public void PrintPlayerData()
    // {
    //     foreach (var kvp in playerObject)
    //     {
    //         string playerID = kvp.Key;
    //         int score = playerScore.ContainsKey(playerID) ? playerScore[playerID] : 0;
    //         bool isDead = playerisDead.ContainsKey(playerID) ? playerisDead[playerID] : false;
    //         Color color = playerColor.ContainsKey(playerID) ? playerColor[playerID] : Color.white;

    //         Debug.Log("UserID: " + playerID + ": Score " + score + " isDead " + isDead + " Color " + color + "\n");
    //     }
    // }
}