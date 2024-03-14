using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // �÷��̾� ������(����)
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>(); // Ű(ID), �÷��̾� ������
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>(); // Ű(ID), ��
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // Ű(ID), ����

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
}