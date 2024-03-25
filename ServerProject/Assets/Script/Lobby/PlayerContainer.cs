using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // 플레이어 프리팹(원본)
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>(); // 키(ID), 플레이어 프리팹
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>(); // 키(ID), 색
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // 키(ID), 점수

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
        {
            return playerColor[playerID];
        }
        else
        {
            return Color.white;
        }
    }

    public Color ReturnPlayerColorArray(int i)
    {
        Color[] scoresArray = new Color[playerColor.Count];
        int index = 0;

        foreach (var color in playerColor.Values)
        {
            scoresArray[index] = color;
            index++;
        }

        if (i >= 0 && i < scoresArray.Length)
        {
            return scoresArray[i];
        }
        else
        {
            return Color.white;
        }
    }

    public void AddPlayerScore(string playerID, int score)
    {
        playerScore[playerID] = score;
    }

    public int ReturnPlayerScore(string playerID)
    {
        if (playerScore.ContainsKey(playerID))
        {
            return playerScore[playerID];
        }
        else
        {
            return -1;
        }
    }

    public int ReturnPlayerScoreArray(int i)
    {
        int[] scoresArray = new int[playerScore.Count];
        int index = 0;

        foreach (var score in playerScore.Values)
        {
            scoresArray[index] = score;
            index++;
        }

        if (i >= 0 && i < scoresArray.Length)
        {
            return scoresArray[i];
        }
        else
        {
            return 0;
        }
    }
}