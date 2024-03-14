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
}