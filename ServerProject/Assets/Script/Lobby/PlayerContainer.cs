using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // �÷��̾� ������(����)
    public Dictionary<string, GameObject> PlayerData = new Dictionary<string, GameObject>(); // Ű(ID), �÷��̾� ������

    public void AddPlayerData(string playerID, GameObject player)
    {
        PlayerData[playerID] = player;
    }

    public GameObject ReturnPlayerData(string playerID)
    {
        return PlayerData[playerID];
    }
}